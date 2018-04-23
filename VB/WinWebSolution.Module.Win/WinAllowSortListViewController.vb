Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Text
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Win.Editors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.ExpressApp.NodeWrappers
Imports DevExpress.Utils

Namespace WinWebSolution.Module.Win
	Public Class WinAllowSortListViewController
		Inherits AllowSortListViewController
		Protected Overrides Sub OnViewControlsCreated()
			MyBase.OnViewControlsCreated()
			UpdateAllowSort()
		End Sub
		Protected Overrides Sub UpdateAllowSort()
			Dim gridListEditor As GridListEditor = TryCast(View.Editor, GridListEditor)
			If gridListEditor IsNot Nothing Then
				Dim allowSortListView As Boolean = GetAllowSortListView()
				gridListEditor.GridView.OptionsCustomization.AllowSort = allowSortListView
				If allowSortListView Then
					For Each column As XafGridColumn In gridListEditor.GridView.Columns
						Dim columnInfo As ColumnInfoNodeWrapper = View.Model.Columns.FindColumnInfo(column.PropertyName)
						If columnInfo IsNot Nothing Then
							If columnInfo.Node.GetAttributeBoolValue(AllowSortAttribute.AllowSortAttributeName) Then
								column.OptionsColumn.AllowSort = DefaultBoolean.True
							Else
								column.OptionsColumn.AllowSort = DefaultBoolean.False
							End If
						End If
					Next column
				End If
			End If
		End Sub
	End Class
End Namespace
