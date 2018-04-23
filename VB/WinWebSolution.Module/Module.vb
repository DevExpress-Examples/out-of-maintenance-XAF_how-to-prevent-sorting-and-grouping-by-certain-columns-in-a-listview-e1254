Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.ExpressApp
Imports System.ComponentModel
Imports DevExpress.Persistent.Base
Imports System.Collections.Generic
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Updating

Namespace WinWebSolution.Module
	Public NotInheritable Partial Class WinWebSolutionModule
		Inherits ModuleBase
		Public Sub New()
			InitializeComponent()
		End Sub
		Public Overrides Sub ExtendModelInterfaces(ByVal extenders As ModelInterfaceExtenders)
			MyBase.ExtendModelInterfaces(extenders)
			extenders.Add(Of IModelCommonMemberViewItem, IModelListViewColumnOptions)()
		End Sub
	End Class
	Public Interface IModelListViewColumnOptions
		<Category("Behavior"), DefaultValue(True)> _
		Property AllowSort() As Boolean
		<Category("Behavior"), DefaultValue(True)> _
		Property AllowGroup() As Boolean
	End Interface
	<AttributeUsage(AttributeTargets.Field Or AttributeTargets.Property, Inherited := True, AllowMultiple := False)> _
	Public NotInheritable Class ListViewColumnOptionsAttribute
		Inherits ModelExportedValuesAttribute
		Public Sub New(ByVal allowSort As Boolean, ByVal allowGroup As Boolean)
			Me.AllowSort = allowSort
			Me.AllowGroup = allowGroup
		End Sub
		Private privateAllowSort As Boolean
		Public Property AllowSort() As Boolean
			Get
				Return privateAllowSort
			End Get
			Set(ByVal value As Boolean)
				privateAllowSort = value
			End Set
		End Property
		Private privateAllowGroup As Boolean
		Public Property AllowGroup() As Boolean
			Get
				Return privateAllowGroup
			End Get
			Set(ByVal value As Boolean)
				privateAllowGroup = value
			End Set
		End Property
		Public Overrides Sub FillValues(ByVal values As Dictionary(Of String, Object))
			values.Add("AllowSort", AllowSort)
			values.Add("AllowGroup", AllowGroup)
		End Sub
	End Class
End Namespace