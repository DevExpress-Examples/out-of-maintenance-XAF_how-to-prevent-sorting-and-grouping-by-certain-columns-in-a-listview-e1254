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
		Implements IObjectModelCustomLoader
		Protected MustOverride Sub UpdateAllowSort()
		Private Sub ListView_InfoChanged(ByVal sender As Object, ByVal e As EventArgs)
			UpdateAllowSort()
		End Sub
		Protected Function GetAllowSortListView() As Boolean
			If View.Model IsNot Nothing Then
				Return View.Model.Node.GetAttributeBoolValue(AllowSortAttribute.AllowSortAttributeName)
			End If
			Return False
		End Function
		Protected Function GetAllowSortColumn(ByVal columnInfo As ColumnInfoNodeWrapper) As Boolean
			If columnInfo IsNot Nothing AndAlso GetAllowSortListView() Then
				Return columnInfo.Node.GetAttributeBoolValue(AllowSortAttribute.AllowSortAttributeName)
			End If
			Return False
		End Function
		Protected Overrides Overloads Sub OnActivated()
			MyBase.OnActivated()
			AddHandler View.InfoChanged, AddressOf ListView_InfoChanged
		End Sub
		Protected Overrides Overloads Sub OnDeactivating()
			RemoveHandler View.InfoChanged, AddressOf ListView_InfoChanged
			MyBase.OnDeactivating()
		End Sub
		Private Sub CustomizeClassNode(ByVal classInfo As ITypeInfo, ByVal classNode As ClassInfoNodeWrapper) Implements IObjectModelCustomLoader.CustomizeClassNode
			Dim attr As AllowSortAttribute
			If classInfo.FindAttribute(Of AllowSortAttribute)() IsNot Nothing Then
				attr = classInfo.FindAttribute(Of AllowSortAttribute)()
			Else
				attr = AllowSortAttribute.Default
			End If
			classNode.Node.SetAttribute(AllowSortAttribute.DefaultListViewAllowSortAttributeName, attr.AllowSort)
		End Sub
		Private Sub CustomizeMemberNode(ByVal memberInfo As IMemberInfo, ByVal memberNode As PropertyInfoNodeWrapper) Implements IObjectModelCustomLoader.CustomizeMemberNode
			Dim attr As AllowSortAttribute
			If memberInfo.FindAttribute(Of AllowSortAttribute)() IsNot Nothing Then
				attr = memberInfo.FindAttribute(Of AllowSortAttribute)()
			Else
				attr = AllowSortAttribute.Default
			End If
			memberNode.Node.SetAttribute(AllowSortAttribute.AllowSortAttributeName, attr.AllowSort)
		End Sub
		Public Overrides Overloads Function GetSchema() As Schema
			Return New Schema(New DictionaryXmlReader().ReadFromString("<Element Name=""Application"">" & ControlChars.CrLf & "                    <Element Name=""BOModel"">" & ControlChars.CrLf & "                        <Element Name=""Class"">" & ControlChars.CrLf & "                            <Attribute Name=""" & AllowSortAttribute.DefaultListViewAllowSortAttributeName & """ Choice=""True,False"" />" & ControlChars.CrLf & "                            <Element Name=""Member"">" & ControlChars.CrLf & "								<Attribute Name=""" & AllowSortAttribute.AllowSortAttributeName & """ Choice=""True,False""/>" & ControlChars.CrLf & "							</Element>" & ControlChars.CrLf & "                        </Element>" & ControlChars.CrLf & "                    </Element>" & ControlChars.CrLf & "                    <Element Name=""Views"">" & ControlChars.CrLf & "		                <Element Name=""ListView"">" & ControlChars.CrLf & "			                <Attribute Name=""" & AllowSortAttribute.AllowSortAttributeName & """" & ControlChars.CrLf & "				                 DefaultValueExpr=""SourceNode=BOModel\Class\@Name=@ClassName; SourceAttribute=@" & AllowSortAttribute.DefaultListViewAllowSortAttributeName & """" & ControlChars.CrLf & "			                     Choice=""True,False""" & ControlChars.CrLf & "				                 />" & ControlChars.CrLf & "                                <Element Name=""Columns"">" & ControlChars.CrLf & "									<Element Name=""ColumnInfo"">" & ControlChars.CrLf & "										<Attribute Name=""" & AllowSortAttribute.AllowSortAttributeName & """ IsNewNode=""True"" " & ControlChars.CrLf & "											DefaultValueExpr=""{DevExpress.ExpressApp.Core.DictionaryHelpers.BOPropertyCalculator}ClassName=..\..\@ClassName""/>" & ControlChars.CrLf & "									</Element>" & ControlChars.CrLf & "								</Element>" & ControlChars.CrLf & ControlChars.CrLf & "		                </Element>" & ControlChars.CrLf & "		            </Element>" & ControlChars.CrLf & "                </Element>"))
		End Function
	End Class
End Namespace