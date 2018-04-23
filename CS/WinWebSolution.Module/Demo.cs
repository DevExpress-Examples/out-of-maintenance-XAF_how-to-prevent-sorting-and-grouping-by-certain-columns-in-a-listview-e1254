using DevExpress.ExpressApp.Model;
using System;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Base;
using DevExpress.Xpo.Metadata;
using DevExpress.ExpressApp;

namespace WinWebSolution.Module {
    [DefaultClassOptions]
    [AllowSort(false)]
    public class DemoIssue : BaseObject {
        public DemoIssue(Session session) : base(session) { }
        public override void AfterConstruction() {
            base.AfterConstruction();
            _CreatedBy = Session.GetObjectByKey<SimpleUser>(SecuritySystem.CurrentUserId);
        }
        [Persistent("ModifiedOn"), ValueConverter(typeof(UtcDateTimeConverter))]
        protected DateTime _ModifiedOn = DateTime.Now;
        [PersistentAlias("_ModifiedOn")]
        [ModelDefault("EditMask", "G")]
        [ModelDefault("DisplayFormat", "{0:G}")]
        public DateTime ModifiedOn {
            get { return _ModifiedOn; }
        }
        internal virtual void UpdateModifiedOn() {
            UpdateModifiedOn(DateTime.Now);
        }
        internal virtual void UpdateModifiedOn(DateTime date) {
            _ModifiedOn = date;
            Save();
        }
        protected override void OnChanged(string propertyName, object oldValue, object newValue) {
            base.OnChanged(propertyName, oldValue, newValue);
            if (propertyName == "Subject" || propertyName == "Description") {
                UpdateModifiedOn();
            }
        }
        private string _Subject;
        public string Subject {
            get { return _Subject; }
            set { SetPropertyValue("Subject", ref _Subject, value); }
        }
        private string _Description;
        public string Description {
            get { return _Description; }
            set { SetPropertyValue("Description", ref _Description, value); }
        }
        [Persistent("CreatedBy")]
        private SimpleUser _CreatedBy;
        [PersistentAlias("_CreatedBy")]
        public SimpleUser CreatedBy {
            get { return _CreatedBy; }
            internal set { _CreatedBy = value; }
        }
    }
    public class DemoUpdater : ModuleUpdater {
        public DemoUpdater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            SimpleUser user = ObjectSpace.FindObject<SimpleUser>(new BinaryOperator("UserName", "Test User"));
            if (user == null) {
                user = ObjectSpace.CreateObject<SimpleUser>();
                user.UserName = "Test User";
                user.Save();
            }
            DemoIssue obj1 = ObjectSpace.FindObject<DemoIssue>(new BinaryOperator("Subject", "Issue 3"));
            if (obj1 == null) {
                obj1 = ObjectSpace.CreateObject<DemoIssue>();
                obj1.Subject = "Issue 3";
                obj1.CreatedBy = user;
                obj1.UpdateModifiedOn();
                obj1.Save();
            }
            DemoIssue obj2 = ObjectSpace.FindObject<DemoIssue>(new BinaryOperator("Subject", "Issue 2"));
            if (obj2 == null) {
                obj2 = ObjectSpace.CreateObject<DemoIssue>();
                obj2.Subject = "Issue 2";
                obj2.CreatedBy = user;
                obj2.UpdateModifiedOn(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1));
                obj2.Save();
            }
            DemoIssue obj3 = ObjectSpace.FindObject<DemoIssue>(new BinaryOperator("Subject", "Issue 1"));
            if (obj3 == null) {
                obj3 = ObjectSpace.CreateObject<DemoIssue>();
                obj3.Subject = "Issue 1";
                obj3.CreatedBy = user;
                obj3.UpdateModifiedOn(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 2));
                obj3.Save();
            }
        }
    }
}
