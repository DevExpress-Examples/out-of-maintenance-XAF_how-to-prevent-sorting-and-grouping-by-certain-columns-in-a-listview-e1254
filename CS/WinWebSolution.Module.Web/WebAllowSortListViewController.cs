using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Web.ASPxGridView;
using DevExpress.ExpressApp.NodeWrappers;
using DevExpress.Web.ASPxClasses;
using DevExpress.Utils;

namespace WinWebSolution.Module.Web {
    public class WebAllowSortListViewController : AllowSortListViewController {
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            UpdateAllowSort();
        }
        protected override void UpdateAllowSort() {
            ASPxGridListEditor gridListEditor = View.Editor as ASPxGridListEditor;
            if (gridListEditor != null) {
                bool allowSortListView = GetAllowSortListView();
                gridListEditor.Grid.SettingsBehavior.AllowSort = allowSortListView;
                if (allowSortListView) {
                    foreach (GridViewColumn column in gridListEditor.Grid.Columns) {
                        GridViewDataColumnWithInfo dataColumn = column as GridViewDataColumnWithInfo;
                        if (dataColumn != null) {
                            dataColumn.Settings.AllowSort = GetAllowSortColumn(dataColumn.Model) ? DefaultBoolean.True : DefaultBoolean.False; ;
                        }
                    }
                }
            }
        }
    }
}