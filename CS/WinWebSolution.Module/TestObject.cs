using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;

namespace WinWebSolution.Module {
    [DefaultClassOptions]
    public class TestObject : BaseObject {
        public TestObject(Session session) : base(session) { }
        private string _CannotSortAndGroupByThisProperty;
        private string _NonRestrictedProperty;
        [ListViewColumnOptions(false, false)]
        public string CannotSortAndGroupByThisProperty {
            get {
                return _CannotSortAndGroupByThisProperty;
            }
            set {
                SetPropertyValue("CannotSortAndGroupByThisProperty", ref _CannotSortAndGroupByThisProperty, value);
            }
        }
        public string NonRestrictedProperty {
            get {
                return _NonRestrictedProperty;
            }
            set {
                SetPropertyValue("NonRestrictedProperty", ref _NonRestrictedProperty, value);
            }
        }
    }
}
