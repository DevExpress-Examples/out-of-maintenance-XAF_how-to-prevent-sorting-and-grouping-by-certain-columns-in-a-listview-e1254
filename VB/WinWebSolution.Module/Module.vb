Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Imports DevExpress.ExpressApp
Imports System.Reflection
Imports DevExpress.ExpressApp.Model


Namespace WinWebSolution.Module
	Public NotInheritable Partial Class WinWebSolutionModule
		Inherits ModuleBase
		Public Sub New()
			InitializeComponent()
		End Sub
		Public Overrides Sub ExtendModelInterfaces(ByVal extenders As ModelInterfaceExtenders)
			MyBase.ExtendModelInterfaces(extenders)
			extenders.Add(Of IModelClass, IModelClassAllowSort)()
			extenders.Add(Of IModelMember, IModelMemberAllowSort)()
			extenders.Add(Of IModelListView, IModelListViewAllowSort)()
			extenders.Add(Of IModelColumn, IModelColumnAllowSort)()
		End Sub
	End Class
End Namespace
