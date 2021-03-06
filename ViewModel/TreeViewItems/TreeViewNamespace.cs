﻿using BusinessLogic.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ViewModel.TreeViewItems
{
    public class TreeViewNamespace : TreeViewItem
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
                children.Add(new TreeViewType(typeModel));
            }
        }
    }
}