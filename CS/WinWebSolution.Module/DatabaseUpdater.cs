using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;

namespace WinWebSolution.Module {
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            TestObject obj1 = ObjectSpace.CreateObject<TestObject>();
            obj1.NonRestrictedProperty = "NonRestrictedProperty";
            obj1.CannotSortAndGroupByThisProperty = "CannotSortAndGroupProperty";
            obj1.Save();
            TestObject obj2 = ObjectSpace.CreateObject<TestObject>();
            obj2.NonRestrictedProperty = obj1.NonRestrictedProperty;
            obj2.CannotSortAndGroupByThisProperty = obj1.CannotSortAndGroupByThisProperty;
            obj2.Save();
        }
    }
}
