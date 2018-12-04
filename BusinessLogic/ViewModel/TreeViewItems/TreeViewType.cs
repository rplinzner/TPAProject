using BusinessLogic.Model;
using System.Collections.ObjectModel;

namespace BusinessLogic.ViewModel.TreeViewItems
{
    public class TreeViewType : TreeViewItem
    {
        public TypeMetadata TypeData { get; set; }
        public TreeViewType(TypeMetadata type) : base(GetFullName(type))
        {
            TypeData = type;
        }

        public override void Build(ObservableCollection<TreeViewItem> children)
        {
            if (TypeData.BaseType != null)
            {
               children.Add(new TreeViewType(TypeData.BaseType));
            }
            if (TypeData.DeclaringType != null)
            {
                children.Add(new TreeViewType(TypeData.DeclaringType));
            }
            if (TypeData.Properties != null)
            {
                foreach (PropertyMetadata PropertyMetadata in TypeData.Properties)
                {
                    children.Add(new TreeViewProperty(PropertyMetadata));
                }
            }
            if (TypeData.Fields != null)
            {
                foreach (ParameterMetadata ParameterMetadata in TypeData.Fields)
                {
                    children.Add(new TreeViewParameter(ParameterMetadata));
                }
            }
            if (TypeData.GenericArguments != null)
            {
                foreach (TypeMetadata TypeMetadata in TypeData.GenericArguments)
                {
                    children.Add(new TreeViewType(TypeMetadata));
                }
            }
            if (TypeData.ImplementedInterfaces != null)
            {
                foreach (TypeMetadata TypeMetadata in TypeData.ImplementedInterfaces)
                {
                    children.Add(new TreeViewType(TypeMetadata));
                }
            }
            if (TypeData.NestedTypes != null)
            {
                foreach (TypeMetadata TypeMetadata in TypeData.NestedTypes)
                {
                    children.Add(new TreeViewType(TypeMetadata));
                }
            }
            if (TypeData.Methods != null)
            {
                foreach (MethodMetadata MethodMetadata in TypeData.Methods)
                {
                    children.Add(new TreeViewMethod(MethodMetadata));
                }
            }
            if (TypeData.Constructors != null)
            {
                foreach (MethodMetadata MethodMetadata in TypeData.Constructors)
                {
                    children.Add(new TreeViewMethod(MethodMetadata));
                }
            }
        }

        public static string GetFullName(TypeMetadata model)
        {
            if (model.Modifiers == null) return null;

            string fullname = "";
            fullname += model.Modifiers.Item1.ToString().ToLower() + " ";
            fullname += model.Modifiers.Item2 == SealedEnum.Sealed ? SealedEnum.Sealed.ToString().ToLower() + " " : "";
            fullname += model.Modifiers.Item3 == AbstractEnum.Abstract ? AbstractEnum.Abstract.ToString().ToLower() + " " : "";
            fullname += model.Modifiers.Item4 == StaticEnum.Static ? StaticEnum.Static.ToString().ToLower() + " " : "";
            fullname += model.Type.ToString().ToLower() + " ";
            fullname += model.Name;
            return fullname;



        }
    }
}