using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using DevExpress.ExpressApp.NodeWrappers;
using DevExpress.Data;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.InfoGenerators;

namespace WinWebSolution.Module {
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property)]
    public class AllowSortAttribute : Attribute {
        public const string DefaultListViewAllowSortAttributeName = "DefaultListViewAllowSort";
        public const string AllowSortAttributeName = "AllowSort";
        public static AllowSortAttribute Default = new AllowSortAttribute(true);
        private bool allowSortCore = true;
        public AllowSortAttribute() : this(true) { }
        public AllowSortAttribute(bool allowSort) {
            this.allowSortCore = allowSort;
        }
        public bool AllowSort {
            get { return allowSortCore; }
            set { allowSortCore = value; }
        }
    }
    public abstract class AllowSortListViewController : ViewController<ListView>, IObjectModelCustomLoader {
        protected abstract void UpdateAllowSort();
        private void ListView_InfoChanged(object sender, EventArgs e) {
            UpdateAllowSort();
        }
        protected bool GetAllowSortListView() {
            if (View.Model != null) {
                return View.Model.Node.GetAttributeBoolValue(AllowSortAttribute.AllowSortAttributeName);
            }
            return false;
        }
        protected bool GetAllowSortColumn(ColumnInfoNodeWrapper columnInfo) {
            if (columnInfo != null && GetAllowSortListView()) {
                return columnInfo.Node.GetAttributeBoolValue(AllowSortAttribute.AllowSortAttributeName);
            }
            return false;
        }
        protected override void OnActivated() {
            base.OnActivated();
            View.InfoChanged += new EventHandler(ListView_InfoChanged);
        }
        protected override void OnDeactivating() {
            View.InfoChanged -= new EventHandler(ListView_InfoChanged);
            base.OnDeactivating();
        }
        void IObjectModelCustomLoader.CustomizeClassNode(ITypeInfo classInfo, ClassInfoNodeWrapper classNode) {
            AllowSortAttribute attr = classInfo.FindAttribute<AllowSortAttribute>() ?? AllowSortAttribute.Default;
            classNode.Node.SetAttribute(AllowSortAttribute.DefaultListViewAllowSortAttributeName, attr.AllowSort);
        }
        void IObjectModelCustomLoader.CustomizeMemberNode(IMemberInfo memberInfo, PropertyInfoNodeWrapper memberNode) {
            AllowSortAttribute attr = memberInfo.FindAttribute<AllowSortAttribute>() ?? AllowSortAttribute.Default;
            memberNode.Node.SetAttribute(AllowSortAttribute.AllowSortAttributeName, attr.AllowSort);
        }
        public override Schema GetSchema() {
            return new Schema(new DictionaryXmlReader().ReadFromString(
                @"<Element Name=""Application"">
                    <Element Name=""BOModel"">
                        <Element Name=""Class"">
                            <Attribute Name=""" + AllowSortAttribute.DefaultListViewAllowSortAttributeName + @""" Choice=""True,False"" />
                            <Element Name=""Member"">
								<Attribute Name=""" + AllowSortAttribute.AllowSortAttributeName + @""" Choice=""True,False""/>
							</Element>
                        </Element>
                    </Element>
                    <Element Name=""Views"">
		                <Element Name=""ListView"">
			                <Attribute Name=""" + AllowSortAttribute.AllowSortAttributeName + @"""
				                 DefaultValueExpr=""SourceNode=BOModel\Class\@Name=@ClassName; SourceAttribute=@" + AllowSortAttribute.DefaultListViewAllowSortAttributeName + @"""
			                     Choice=""True,False""
				                 />
                                <Element Name=""Columns"">
									<Element Name=""ColumnInfo"">
										<Attribute Name=""" + AllowSortAttribute.AllowSortAttributeName + @""" IsNewNode=""True"" 
											DefaultValueExpr=""{DevExpress.ExpressApp.Core.DictionaryHelpers.BOPropertyCalculator}ClassName=..\..\@ClassName""/>
									</Element>
								</Element>

		                </Element>
		            </Element>
                </Element>"));
        }
    }
}