using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraGrid.Columns;
using DevExpress.ExpressApp.Model.NodeWrappers;
using DevExpress.Utils;

namespace WinWebSolution.Module.Win {
    public class WinAllowSortListViewController : AllowSortListViewController {
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            UpdateAllowSort();
        }
        protected override void UpdateAllowSort() {
            GridListEditor gridListEditor = View.Editor as GridListEditor;
            if (gridListEditor != null) {
                bool allowSortListView = GetAllowSortListView();
                gridListEditor.GridView.OptionsCustomization.AllowSort = allowSortListView;
                if (allowSortListView) {
                    foreach (GridColumn column in gridListEditor.GridView.Columns) {
                        XafGridColumn xafColumn = column as XafGridColumn;
                        if (xafColumn != null) {
                            xafColumn.OptionsColumn.AllowSort = GetAllowSortColumn(xafColumn.Model) ? DefaultBoolean.True : DefaultBoolean.False; ;
                        }
                    }
                }
            }
        }
    }
}
