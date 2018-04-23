Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.Persistent.Base
Imports DevExpress.Xpo
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp
Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.NodeWrappers
Imports DevExpress.Data
Imports DevExpress.ExpressApp.DC
Imports DevExpress.ExpressApp.InfoGenerators
Imports DevExpress.ExpressApp.Model

Namespace WinWebSolution.Module
	<AttributeUsage(AttributeTargets.Interface Or AttributeTargets.Class Or AttributeTargets.Field Or AttributeTargets.Property)> _
	Public Class AllowSortAttribute
		Inherits Attribute
		Public Const DefaultListViewAllowSortAttributeName As String = "DefaultListViewAllowSort"
		Public Const AllowSortAttributeName As String = "AllowSort"
		Public Shared [Default] As New AllowSortAttribute(True)
		Private allowSortCore As Boolean = True
		Public Sub New()
			Me.New(True)
		End Sub
		Public Sub New(ByVal allowSort As Boolean)
			Me.allowSortCore = allowSort
		End Sub
		Public Property AllowSort() As Boolean
			Get
				Return allowSortCore
			End Get
			Set(ByVal value As Boolean)
				allowSortCore = value
			End Set
		End Property
	End Class
	Public MustInherit Class AllowSortListViewController
		Inherits ViewController(Of ListView)
		Protected MustOverride Sub UpdateAllowSort()
		Private Sub ListView_InfoChanged(ByVal sender As Object, ByVal e As EventArgs)
			UpdateAllowSort()
		End Sub
		Protected Function GetAllowSortListView() As Boolean
			If View.Model IsNot Nothing Then
				Return (CType(View.Model, IModelListViewAllowSort)).AllowSort
			End If
			Return False
		End Function
		Protected Function GetAllowSortColumn(ByVal column As IModelColumn) As Boolean
			If column IsNot Nothing AndAlso GetAllowSortListView() Then
				Return (CType(column, IModelColumnAllowSort)).AllowSort
			End If
			Return False
		End Function
		Protected Overrides Overloads Sub OnActivated()
			MyBase.OnActivated()
			AddHandler View.ModelChanged, AddressOf ListView_InfoChanged
		End Sub
		Protected Overrides Overloads Sub OnDeactivated()
			RemoveHandler View.ModelChanged, AddressOf ListView_InfoChanged
			MyBase.OnDeactivated()
		End Sub
		'void IObjectModelCustomLoader.CustomizeClassNode(ITypeInfo classInfo, ClassInfoNodeWrapper classNode) {
		'    AllowSortAttribute attr = classInfo.FindAttribute<AllowSortAttribute>() ?? AllowSortAttribute.Default;
		'    classNode.Node.SetAttribute(AllowSortAttribute.DefaultListViewAllowSortAttributeName, attr.AllowSort);
		'}
		'void IObjectModelCustomLoader.CustomizeMemberNode(IMemberInfo memberInfo, PropertyInfoNodeWrapper memberNode) {
		'    AllowSortAttribute attr = memberInfo.FindAttribute<AllowSortAttribute>() ?? AllowSortAttribute.Default;
		'    memberNode.Node.SetAttribute(AllowSortAttribute.AllowSortAttributeName, attr.AllowSort);
		'}
	   ' public override Schema GetSchema() {
	   '     return new Schema(new DictionaryXmlReader().ReadFromString(
	   '         @"<Element Name=""Application"">
	   '             <Element Name=""BOModel"">
	   '                 <Element Name=""Class"">
	   '                     <Attribute Name=""" + AllowSortAttribute.DefaultListViewAllowSortAttributeName + @""" Choice=""True,False"" />
	   '                     <Element Name=""Member"">
							'	<Attribute Name=""" + AllowSortAttribute.AllowSortAttributeName + @""" Choice=""True,False""/>
							'</Element>
	   '                 </Element>
	   '             </Element>
	   '             <Element Name=""Views"">
			 '           <Element Name=""ListView"">
				'            <Attribute Name=""" + AllowSortAttribute.AllowSortAttributeName + @"""
				   '              DefaultValueExpr=""SourceNode=BOModel\Class\@Name=@ClassName; SourceAttribute=@" + AllowSortAttribute.DefaultListViewAllowSortAttributeName + @"""
				'                 Choice=""True,False""
				   '              />
	   '                         <Element Name=""Columns"">
							'		<Element Name=""ColumnInfo"">
							'			<Attribute Name=""" + AllowSortAttribute.AllowSortAttributeName + @""" IsNewNode=""True"" 
							'				DefaultValueExpr=""{DevExpress.ExpressApp.Core.DictionaryHelpers.BOPropertyCalculator}ClassName=..\..\@ClassName""/>
							'		</Element>
							'	</Element>

			 '           </Element>
			 '       </Element>
	   '         </Element>"));
	   ' }
	End Class
End Namespace