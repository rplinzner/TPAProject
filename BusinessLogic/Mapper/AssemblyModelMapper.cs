using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BusinessLogic.Model;
using Data.DataModel;

namespace BusinessLogic.Mapper
{
    public class AssemblyModelMapper 
    {

        public static AssemblyMetadata MapUp(BaseAssemblyMetadata model)
        {
            AssemblyMetadata assemblyModel = new AssemblyMetadata();
            Type type = model.GetType();
            assemblyModel.Name = model.Name;
            PropertyInfo namespaceModelsProperty = type.GetProperty("NamespaceModels",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            List<BaseNamespaceMetadata> namespaceModels= (List<BaseNamespaceMetadata>)HelperClass.ConvertList(typeof(BaseNamespaceMetadata),(IList)namespaceModelsProperty?.GetValue(model));
            if (namespaceModels != null)
                assemblyModel.Namespaces = namespaceModels.Select(n => new NamespaceModelMapper().MapUp(n)).ToList();
            return assemblyModel;
        }

        public static BaseAssemblyMetadata MapDown(AssemblyMetadata model, Type assemblyModelType)
        {
            object assemblyModel = Activator.CreateInstance(assemblyModelType);
            PropertyInfo nameProperty = assemblyModelType.GetProperty("Name");
            PropertyInfo namespaceModelsProperty = assemblyModelType.GetProperty("NamespaceModels",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            nameProperty?.SetValue(assemblyModel,model.Name);
            namespaceModelsProperty?.SetValue(
                assemblyModel,
                HelperClass.ConvertList(namespaceModelsProperty.PropertyType.GetGenericArguments()[0],
                    model.Namespaces.Select(n => new NamespaceModelMapper().MapDown(n,namespaceModelsProperty.PropertyType.GetGenericArguments()[0])).ToList()));
            return (BaseAssemblyMetadata)assemblyModel;
        }

    }
}
