Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl

Namespace WinWebSolution.Module
	<DefaultClassOptions> _
	Public Class TestObject
		Inherits BaseObject
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		Private _CannotSortAndGroupByThisProperty As String
		Private _NonRestrictedProperty As String
		<ListViewColumnOptions(False, False)> _
		Public Property CannotSortAndGroupByThisProperty() As String
			Get
				Return _CannotSortAndGroupByThisProperty
			End Get
			Set(ByVal value As String)
				SetPropertyValue("CannotSortAndGroupByThisProperty", _CannotSortAndGroupByThisProperty, value)
			End Set
		End Property
		Public Property NonRestrictedProperty() As String
			Get
				Return _NonRestrictedProperty
			End Get
			Set(ByVal value As String)
				SetPropertyValue("NonRestrictedProperty", _NonRestrictedProperty, value)
			End Set
		End Property
	End Class
End Namespace
