Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Text
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Web.Editors.ASPx
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.ExpressApp.NodeWrappers
Imports DevExpress.Web.ASPxClasses

Namespace WinWebSolution.Module.Web
	Public Class WebAllowSortListViewController
		Inherits AllowSortListViewController
		Protected Overrides Sub OnViewControlsCreated()
			MyBase.OnViewControlsCreated()
			UpdateAllowSort()
		End Sub
		Protected Overrides Sub UpdateAllowSort()
			Dim gridListEditor As ASPxGridListEditor = TryCast(View.Editor, ASPxGridListEditor)
			If gridListEditor IsNot Nothing Then
				Dim allowSortListView As Boolean = GetAllowSortListView()
				gridListEditor.Grid.SettingsBehavior.AllowSort = allowSortListView
				If allowSortListView Then
					For Each column As GridViewColumn In gridListEditor.Grid.Columns
						Dim dataColumn As GridViewDataColumnWithInfo = TryCast(column, GridViewDataColumnWithInfo)
						If dataColumn IsNot Nothing Then
							If dataColumn.Info.Node.GetAttributeBoolValue(AllowSortAttribute.AllowSortAttributeName) Then
								dataColumn.Settings.AllowSort = DefaultBoolean.True
							Else
								dataColumn.Settings.AllowSort = DefaultBoolean.False
							End If
						End If
					Next column
				End If
			End If
		End Sub
	End Class
End Namespace