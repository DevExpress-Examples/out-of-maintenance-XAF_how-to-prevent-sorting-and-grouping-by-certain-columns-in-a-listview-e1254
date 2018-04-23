using System;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Updating;

namespace WinWebSolution.Module {
    public sealed partial class WinWebSolutionModule : ModuleBase {
        public WinWebSolutionModule() {
            InitializeComponent();
        }
        public override void ExtendModelInterfaces(ModelInterfaceExtenders extenders) {
            base.ExtendModelInterfaces(extenders);
            extenders.Add<IModelCommonMemberViewItem, IModelListViewColumnOptions>();
        }
    }
    public interface IModelListViewColumnOptions {
        [Category("Behavior"), DefaultValue(true)]
        bool AllowSort { get; set; }
        [Category("Behavior"), DefaultValue(true)]
        bool AllowGroup { get; set; }
    }
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class ListViewColumnOptionsAttribute : ModelExportedValuesAttribute {
        public ListViewColumnOptionsAttribute(bool allowSort, bool allowGroup) {
            this.AllowSort = allowSort;
            this.AllowGroup = allowGroup;
        }
        public bool AllowSort { get; set; }
        public bool AllowGroup { get; set; }
        public override void FillValues(Dictionary<string, object> values) {
            values.Add("AllowSort", AllowSort);
            values.Add("AllowGroup", AllowGroup);
        }
    }
}