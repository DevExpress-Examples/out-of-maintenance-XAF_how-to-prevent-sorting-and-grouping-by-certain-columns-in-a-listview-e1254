Imports Microsoft.VisualBasic
Imports System.ComponentModel
Imports DevExpress.ExpressApp.DC
Imports DevExpress.ExpressApp.Model

Namespace WinWebSolution.Module

	Public Interface IModelClassAllowSort
		<Category("Behavior")> _
		Property DefaultListViewAllowSort() As Boolean
	End Interface
	Public Interface IModelMemberAllowSort
		<Category("Behavior")> _
		Property AllowSort() As Boolean
	End Interface
	Public Interface IModelListViewAllowSort
		<Category("Behavior")> _
		Property AllowSort() As Boolean
	End Interface
	Public Interface IModelColumnAllowSort
		<Category("Behavior")> _
		Property AllowSort() As Boolean
	End Interface
	<DomainLogic(GetType(IModelClassAllowSort))> _
	Public NotInheritable Class ModelClassAllowSortLogic
		Public Shared Function Get_DefaultListViewAllowSort(ByVal modelClass As IModelClass) As Boolean
			If modelClass IsNot Nothing AndAlso modelClass.TypeInfo IsNot Nothing Then
				Dim attribute As AllowSortAttribute = modelClass.TypeInfo.FindAttribute(Of AllowSortAttribute)()
				If attribute IsNot Nothing Then
					Return attribute.AllowSort
				End If
			End If
			Return True
		End Function
	End Class
	<DomainLogic(GetType(IModelMemberAllowSort))> _
	Public NotInheritable Class ModelMemberAllowSortLogic
		Public Shared Function Get_AllowSort(ByVal modelMember As IModelMember) As Boolean
			If modelMember IsNot Nothing AndAlso modelMember.MemberInfo IsNot Nothing Then
				Dim attribute As AllowSortAttribute = modelMember.MemberInfo.FindAttribute(Of AllowSortAttribute)()
				If attribute IsNot Nothing Then
					Return attribute.AllowSort
				End If
			End If
			Return True
		End Function
	End Class
	<DomainLogic(GetType(IModelListViewAllowSort))> _
	Public NotInheritable Class ModelListViewAllowSortLogic
		Public Shared Function Get_AllowSort(ByVal modelListView As IModelListView) As Boolean
			If modelListView IsNot Nothing AndAlso modelListView.ModelClass IsNot Nothing Then
				Return (CType(modelListView.ModelClass, IModelClassAllowSort)).DefaultListViewAllowSort
			Else
				Return True
			End If
		End Function
	End Class
	<DomainLogic(GetType(IModelColumnAllowSort))> _
	Public NotInheritable Class ModelColumnAllowSortLogic
		Public Shared Function Get_AllowSort(ByVal modelColumn As IModelColumn) As Boolean
			If modelColumn IsNot Nothing AndAlso modelColumn.ModelMember IsNot Nothing Then
				Return (CType(modelColumn.ModelMember, IModelMemberAllowSort)).AllowSort
			Else
				Return True
			End If
		End Function
	End Class
End Namespace