using System;
using System.Collections.ObjectModel;
using BusinessLogic.Model;

namespace ViewModel.TreeViewItems
{
    public class TreeViewMethod : TreeViewItem
    {
        public MethodMetadata MethodData;

        public TreeViewMethod(MethodMetadata methodMetadata):base(GetFullName(methodMetadata))
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
            return model.GetFullName();
        }
    }
}