Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Base
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp

Namespace WinWebSolution.Module
	<DefaultClassOptions, AllowSort(False)> _
	Public Class DemoIssue
		Inherits BaseObject
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		Public Overrides Sub AfterConstruction()
			MyBase.AfterConstruction()
			_CreatedBy = Session.GetObjectByKey(Of SimpleUser)(SecuritySystem.CurrentUserId)
		End Sub
		<Persistent("ModifiedOn"), ValueConverter(GetType(UtcDateTimeConverter))> _
		Protected _ModifiedOn As DateTime = DateTime.Now
		<PersistentAlias("_ModifiedOn"), Custom("EditMask", "G"), Custom("DisplayFormat", "{0:G}")> _
		Public ReadOnly Property ModifiedOn() As DateTime
			Get
				Return _ModifiedOn
			End Get
		End Property
		Friend Overridable Sub UpdateModifiedOn()
			UpdateModifiedOn(DateTime.Now)
		End Sub
		Friend Overridable Sub UpdateModifiedOn(ByVal [date] As DateTime)
			_ModifiedOn = [date]
			Save()
		End Sub
		Protected Overrides Sub OnChanged(ByVal propertyName As String, ByVal oldValue As Object, ByVal newValue As Object)
			MyBase.OnChanged(propertyName, oldValue, newValue)
			If propertyName = "Subject" OrElse propertyName = "Description" Then
				UpdateModifiedOn()
			End If
		End Sub
		Private _Subject As String
		Public Property Subject() As String
			Get
				Return _Subject
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Subject", _Subject, value)
			End Set
		End Property
		Private _Description As String
		Public Property Description() As String
			Get
				Return _Description
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Description", _Description, value)
			End Set
		End Property
		<Persistent("CreatedBy")> _
		Private _CreatedBy As SimpleUser
		<PersistentAlias("_CreatedBy")> _
		Public Property CreatedBy() As SimpleUser
			Get
				Return _CreatedBy
			End Get
			Friend Set(ByVal value As SimpleUser)
				_CreatedBy = value
			End Set
		End Property
	End Class
	Public Class DemoUpdater
		Inherits ModuleUpdater
		Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
			MyBase.New(objectSpace, currentDBVersion)
		End Sub
		Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
			MyBase.UpdateDatabaseAfterUpdateSchema()
			Dim user As SimpleUser = ObjectSpace.FindObject(Of SimpleUser)(New BinaryOperator("UserName", "Test User"))
			If user Is Nothing Then
				user = ObjectSpace.CreateObject(Of SimpleUser)()
				user.UserName = "Test User"
				user.Save()
			End If
			Dim obj1 As DemoIssue = ObjectSpace.FindObject(Of DemoIssue)(New BinaryOperator("Subject", "Issue 3"))
			If obj1 Is Nothing Then
				obj1 = ObjectSpace.CreateObject(Of DemoIssue)()
				obj1.Subject = "Issue 3"
				obj1.CreatedBy = user
				obj1.UpdateModifiedOn()
				obj1.Save()
			End If
			Dim obj2 As DemoIssue = ObjectSpace.FindObject(Of DemoIssue)(New BinaryOperator("Subject", "Issue 2"))
			If obj2 Is Nothing Then
				obj2 = ObjectSpace.CreateObject(Of DemoIssue)()
				obj2.Subject = "Issue 2"
				obj2.CreatedBy = user
				obj2.UpdateModifiedOn(New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1))
				obj2.Save()
			End If
			Dim obj3 As DemoIssue = ObjectSpace.FindObject(Of DemoIssue)(New BinaryOperator("Subject", "Issue 1"))
			If obj3 Is Nothing Then
				obj3 = ObjectSpace.CreateObject(Of DemoIssue)()
				obj3.Subject = "Issue 1"
				obj3.CreatedBy = user
				obj3.UpdateModifiedOn(New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 2))
				obj3.Save()
			End If
		End Sub
	End Class
End Namespace
