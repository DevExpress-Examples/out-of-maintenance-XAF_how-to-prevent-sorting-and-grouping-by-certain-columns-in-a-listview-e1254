Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.ExpressApp
Imports System.Collections.Generic
Imports DevExpress.ExpressApp.Editors

Namespace WinWebSolution.Module
	Public Class ListViewColumnOptionsController
		Inherits ViewController(Of ListView)
		Private Sub ListView_ModelChanged(ByVal sender As Object, ByVal e As EventArgs)
			UpdateListViewColumnOptions()
		End Sub
		Protected Overrides Sub OnActivated()
			MyBase.OnActivated()
			AddHandler View.ModelChanged, AddressOf ListView_ModelChanged
		End Sub
		Protected Overrides Sub OnDeactivated()
			RemoveHandler View.ModelChanged, AddressOf ListView_ModelChanged
			MyBase.OnDeactivated()
		End Sub
		Protected Overrides Sub OnViewControlsCreated()
			MyBase.OnViewControlsCreated()
			UpdateListViewColumnOptions()
		End Sub
		Protected Overridable Sub UpdateListViewColumnOptions()
			Dim columnsListEditor As ColumnsListEditor = TryCast(View.Editor, ColumnsListEditor)
			If columnsListEditor IsNot Nothing Then
				For Each columnWrapper As ColumnWrapper In columnsListEditor.Columns
					Dim options As IModelListViewColumnOptions = TryCast((CType(columnsListEditor.Model.Columns, DevExpress.ExpressApp.Model.IModelColumns)).GetNode(columnWrapper.PropertyName), IModelListViewColumnOptions)
					If options IsNot Nothing Then
						If columnWrapper.AllowSortingChange AndAlso (Not options.AllowSort) Then
							columnWrapper.AllowSortingChange = False
							columnWrapper.SortOrder = DevExpress.Data.ColumnSortOrder.None
							columnWrapper.SortIndex = -1
						End If
						If columnWrapper.AllowGroupingChange AndAlso (Not options.AllowGroup) Then
							columnWrapper.AllowGroupingChange = False
							columnWrapper.GroupIndex = -1
						End If
					End If
				Next columnWrapper
			End If
		End Sub
	End Class
End Namespace
