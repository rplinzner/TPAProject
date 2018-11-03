using System.Collections.ObjectModel;
using BusinessLogic.Model;

namespace BusinessLogic.ViewModel.TreeViewItems
{
    internal class TreeViewMethod : TreeViewItem
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
                    children.Add(new TreeViewType(TypeMetadata.TypeDictionary[genericArgument.Name]));
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
                children.Add(new TreeViewType(TypeMetadata.TypeDictionary[MethodData.ReturnType.Name]));
            }
        }
    }
}