using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BusinessLogic.Model;
using Data.DataModel;

namespace BusinessLogic.Mapper
{
    public class NamespaceModelMapper
    {
        public NamespaceMetadata MapUp(BaseNamespaceMetadata model)
        {
            NamespaceMetadata namespaceModel = new NamespaceMetadata();
            namespaceModel.Name = model.Name;
            Type type = model.GetType();
            PropertyInfo typesProperty = type.GetProperty("Types",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            List<BaseTypeMetadata> types = (List<BaseTypeMetadata>)HelperClass.ConvertList(typeof(BaseTypeMetadata), (IList)typesProperty?.GetValue(model));
            if (types != null)
                namespaceModel.Types = types.Select(n => TypeModelMapper.EmitType(n)).ToList();
            return namespaceModel;
        }

        public BaseNamespaceMetadata MapDown(NamespaceMetadata model, Type namespaceModelType)
        {
            object namespaceModel = Activator.CreateInstance(namespaceModelType);
            PropertyInfo nameProperty = namespaceModelType.GetProperty("Name");
            PropertyInfo namespaceModelsProperty = namespaceModelType.GetProperty("Types",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            nameProperty?.SetValue(namespaceModel, model.Name);
            namespaceModelsProperty?.SetValue(namespaceModel,
                HelperClass.ConvertList(namespaceModelsProperty.PropertyType.GetGenericArguments()[0],
                    model.Types.Select(t => new TypeModelMapper().MapDown(t, namespaceModelsProperty.PropertyType.GetGenericArguments()[0])).ToList()));

            return (BaseNamespaceMetadata)namespaceModel;
        }
    }
}
