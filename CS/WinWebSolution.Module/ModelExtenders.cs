using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;

namespace WinWebSolution.Module {

    public interface IModelClassAllowSort {
        [Category("Behavior")]
        bool DefaultListViewAllowSort { get; set; }
    }
    public interface IModelMemberAllowSort {
        [Category("Behavior")]
        bool AllowSort { get; set; }
    }
    public interface IModelListViewAllowSort {
        [Category("Behavior")]
        bool AllowSort { get; set; }
    }
    public interface IModelColumnAllowSort {
        [Category("Behavior")]
        bool AllowSort { get; set; }
    }
    [DomainLogic(typeof(IModelClassAllowSort))]
    public static class ModelClassAllowSortLogic {
        public static bool Get_DefaultListViewAllowSort(IModelClass modelClass) {
            if (modelClass != null && modelClass.TypeInfo != null) {
                AllowSortAttribute attribute = modelClass.TypeInfo.FindAttribute<AllowSortAttribute>();
                if (attribute != null)
                    return attribute.AllowSort;
            }
            return true;
        }
    }
    [DomainLogic(typeof(IModelMemberAllowSort))]
    public static class ModelMemberAllowSortLogic {
        public static bool Get_AllowSort(IModelMember modelMember) {
            if (modelMember != null && modelMember.MemberInfo != null) {
                AllowSortAttribute attribute = modelMember.MemberInfo.FindAttribute<AllowSortAttribute>();
                if (attribute != null)
                    return attribute.AllowSort;
            }
            return true;
        }
    }
    [DomainLogic(typeof(IModelListViewAllowSort))]
    public static class ModelListViewAllowSortLogic {
        public static bool Get_AllowSort(IModelListView modelListView) {
            if (modelListView != null && modelListView.ModelClass != null)
                return ((IModelClassAllowSort)modelListView.ModelClass).DefaultListViewAllowSort;
            else
                return true;
        }
    }
    [DomainLogic(typeof(IModelColumnAllowSort))]
    public static class ModelColumnAllowSortLogic {
        public static bool Get_AllowSort(IModelColumn modelColumn) {
            if (modelColumn != null && modelColumn.ModelMember != null)
                return ((IModelMemberAllowSort)modelColumn.ModelMember).AllowSort;
            else
                return true;
        }
    }
}