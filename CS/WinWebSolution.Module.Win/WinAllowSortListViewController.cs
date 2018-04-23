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
using DevExpress.ExpressApp.NodeWrappers;
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
                    foreach (XafGridColumn column in gridListEditor.GridView.Columns) {
                        ColumnInfoNodeWrapper columnInfo = View.Model.Columns.FindColumnInfo(column.PropertyName);
                        if (columnInfo != null) {
                            column.OptionsColumn.AllowSort = columnInfo.Node.GetAttributeBoolValue(AllowSortAttribute.AllowSortAttributeName) ? DefaultBoolean.True : DefaultBoolean.False;
                        }
                    }
                }
            }
        }
    }
}
