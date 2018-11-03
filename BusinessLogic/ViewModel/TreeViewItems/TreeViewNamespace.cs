﻿using BusinessLogic.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BusinessLogic.ViewModel.TreeViewItems
{
    internal class TreeViewNamespace : TreeViewItem
    {
        public List<TypeMetadata> Types { get; set; }

        public TreeViewNamespace(NamespaceMetadata metadata) : base(metadata.Name)
        {
            Types = metadata.Types;
        }

        public override void Build(ObservableCollection<TreeViewItem> children)
        {
            if (Types == null) return;
            foreach (TypeMetadata typeModel in Types)
            {
                children.Add(new TreeViewType(TypeMetadata.TypeDictionary[typeModel.Name]));
            }
        }
    }
}