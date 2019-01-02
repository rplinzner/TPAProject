using System;
using System.Reflection;
using BusinessLogic.Model;
using Data.DataModel;

namespace BusinessLogic.Mapper
{
    public class ParameterModelMapper
    {
        public ParameterMetadata MapUp(BaseParameterMetadata model)
        {
            ParameterMetadata parameterModel = new ParameterMetadata();
            parameterModel.Name = model.Name;
            Type type = model.GetType();
            PropertyInfo typeProperty = type.GetProperty("Type", 
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            BaseTypeMetadata typeModel = (BaseTypeMetadata)typeProperty?.GetValue(model);
            if (typeModel != null)
                parameterModel.Type = TypeModelMapper.EmitType(typeModel);
            return parameterModel;
        }

        public BaseParameterMetadata MapDown(ParameterMetadata model, Type parameterModelType)
        {
            object parameterModel = Activator.CreateInstance(parameterModelType);
            PropertyInfo nameProperty = parameterModelType.GetProperty("Name");
            PropertyInfo typeProperty = parameterModelType.GetProperty("Type",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            nameProperty?.SetValue(parameterModel, model.Name);
            if (model.Type != null)
                typeProperty?.SetValue(parameterModel, 
                    typeProperty.PropertyType.Cast(TypeModelMapper.EmitBaseType(model.Type, typeProperty.PropertyType)));

            return (BaseParameterMetadata)parameterModel;
        }
    }
}
