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

    // Logic

    [DomainLogic(typeof(IModelClassAllowSort))]
    public static class ModelClassAllowSortLogic {
        public static bool Get_DefaultListViewAllowSort(IModelClass modelClass) {
            AllowSortAttribute attribute = modelClass.TypeInfo.FindAttribute<AllowSortAttribute>();
            if (attribute != null) {
                return attribute.AllowSort;
            } else {
                return true;
            }
        }
    }
    [DomainLogic(typeof(IModelMemberAllowSort))]
    public static class ModelMemberAllowSortLogic {
        public static bool Get_AllowSort(IModelMember modelMember) {
            AllowSortAttribute attribute = modelMember.MemberInfo.FindAttribute<AllowSortAttribute>();
            if (attribute != null) {
                return attribute.AllowSort;
            } else {
                return true;
            }
        }
    }
    [DomainLogic(typeof(IModelListViewAllowSort))]
    public static class ModelListViewAllowSortLogic {
        public static bool Get_AllowSort(IModelListView modelListView) {
            return ((IModelClassAllowSort)modelListView.ModelClass).DefaultListViewAllowSort;
        }
    }
    [DomainLogic(typeof(IModelColumnAllowSort))]
    public static class ModelColumnAllowSortLogic {
        public static bool Get_AllowSort(IModelColumn modelColumn) {
            return ((IModelMemberAllowSort)modelColumn.ModelMember).AllowSort;
        }
    }
}