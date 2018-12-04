using System;
using System.Collections.ObjectModel;
using BusinessLogic.Model;

namespace BusinessLogic.ViewModel.TreeViewItems
{
    public class TreeViewMethod : TreeViewItem
    {
        public MethodMetadata MethodData;

        public TreeViewMethod(MethodMetadata methodMetadata):base(methodMetadata.Name)
        {
            MethodData= methodMetadata;
        }

        public override void Build(ObservableCollection<TreeViewItem> children)
        {
            if (MethodData.GenericArguments != null)
            {
                foreach (TypeMetadata genericArgument in MethodData.GenericArguments)
                {
                    children.Add(new TreeViewType(genericArgument));
                }
            }

            if (MethodData.Parameters != null)
            {
                foreach (ParameterMetadata parameter in MethodData.Parameters)
                {
                    children.Add(new TreeViewParameter(parameter));
                }
            }

            if (MethodData.ReturnType != null)
            {
                children.Add(new TreeViewType(MethodData.ReturnType));
            }
            
        }

        public static string GetFullName(MethodMetadata model)
        {
            string fullname = "";
            fullname += model.Modifiers.Item1.ToString().ToLower() + " ";
            fullname += model.Modifiers.Item2 == AbstractEnum.Abstract ? AbstractEnum.Abstract.ToString().ToLower() + " " : "";
            fullname += model.Modifiers.Item3 == StaticEnum.Static ? StaticEnum.Static.ToString().ToLower() + " " : "";
            fullname += model.Modifiers.Item4 == VirtualEnum.Virtual ? VirtualEnum.Virtual.ToString().ToLower() + " " : "";
            return fullname;
        }
    }
}