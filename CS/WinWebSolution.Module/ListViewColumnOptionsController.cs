using System;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.ExpressApp.Editors;

namespace WinWebSolution.Module {
    public class ListViewColumnOptionsController : ViewController<ListView> {
        private void ListView_ModelChanged(object sender, EventArgs e) {
            UpdateListViewColumnOptions();
        }
        protected override void OnActivated() {
            base.OnActivated();
            View.ModelChanged += ListView_ModelChanged;
        }
        protected override void OnDeactivated() {
            View.ModelChanged -= ListView_ModelChanged;
            base.OnDeactivated();
        }
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            UpdateListViewColumnOptions();
        }
        protected virtual void UpdateListViewColumnOptions() {
            ColumnsListEditor columnsListEditor = View.Editor as ColumnsListEditor;
            if(columnsListEditor != null) {
                foreach(ColumnWrapper columnWrapper in columnsListEditor.Columns) {
                    IModelListViewColumnOptions options = columnsListEditor.Model.Columns[columnWrapper.PropertyName] as IModelListViewColumnOptions;
                    if(options != null) {
                        if(columnWrapper.AllowSortingChange && !options.AllowSort) {
                            columnWrapper.AllowSortingChange = false;
                            columnWrapper.SortOrder = DevExpress.Data.ColumnSortOrder.None;
                            columnWrapper.SortIndex = -1;
                        }
                        if(columnWrapper.AllowGroupingChange && !options.AllowGroup) {
                            columnWrapper.AllowGroupingChange = false;
                            columnWrapper.GroupIndex = -1;
                        }
                    }
                }
            }
        }
    }
}
