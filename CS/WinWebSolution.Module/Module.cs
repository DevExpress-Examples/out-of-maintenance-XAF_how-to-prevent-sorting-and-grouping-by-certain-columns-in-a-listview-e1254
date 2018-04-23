using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using System.Reflection;
using DevExpress.ExpressApp.Model;


namespace WinWebSolution.Module {
    public sealed partial class WinWebSolutionModule : ModuleBase {
        public WinWebSolutionModule() {
            InitializeComponent();
        }
        public override void ExtendModelInterfaces(ModelInterfaceExtenders extenders) {
            base.ExtendModelInterfaces(extenders);
            extenders.Add<IModelClass, IModelClassAllowSort>();
            extenders.Add<IModelMember, IModelMemberAllowSort>();
            extenders.Add<IModelListView, IModelListViewAllowSort>();
            extenders.Add<IModelColumn, IModelColumnAllowSort>();
        }
    }
}
