Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Updating

Namespace WinWebSolution.Module
	Public Class Updater
		Inherits ModuleUpdater
		Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
			MyBase.New(objectSpace, currentDBVersion)
		End Sub
		Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
			MyBase.UpdateDatabaseAfterUpdateSchema()
			Dim obj1 As TestObject = ObjectSpace.CreateObject(Of TestObject)()
			obj1.NonRestrictedProperty = "NonRestrictedProperty"
			obj1.CannotSortAndGroupByThisProperty = "CannotSortAndGroupProperty"
			obj1.Save()
			Dim obj2 As TestObject = ObjectSpace.CreateObject(Of TestObject)()
			obj2.NonRestrictedProperty = obj1.NonRestrictedProperty
			obj2.CannotSortAndGroupByThisProperty = obj1.CannotSortAndGroupByThisProperty
			obj2.Save()
		End Sub
	End Class
End Namespace
