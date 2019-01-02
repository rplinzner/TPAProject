using System;
using System.Reflection;
using BusinessLogic.Model;
using Data.DataModel;

namespace BusinessLogic.Mapper
{
    public class PropertyModelMapper
    {
        public PropertyMetadata MapUp(BasePropertyMetadata model)
        {
            PropertyMetadata propertyModel = new PropertyMetadata();
            propertyModel.Name = model.Name;
            Type type = model.GetType();
            PropertyInfo typeProperty = type.GetProperty("Type",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            BaseTypeMetadata typeModel = (BaseTypeMetadata)typeProperty?.GetValue(model);

            if (typeModel != null)
                propertyModel.Type = TypeModelMapper.EmitType(typeModel);

            return propertyModel;
        }

        public BasePropertyMetadata MapDown(PropertyMetadata model,Type propertyModelType)
        {
            object propertyModel = Activator.CreateInstance(propertyModelType);
            PropertyInfo nameProperty = propertyModelType.GetProperty("Name");
            PropertyInfo typeProperty = propertyModelType.GetProperty("Type", 
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            nameProperty?.SetValue(propertyModel, model.Name);

            if (model.Type != null)
                typeProperty?.SetValue(propertyModel, 
                    typeProperty.PropertyType.Cast(TypeModelMapper.EmitBaseType(model.Type, typeProperty.PropertyType)));

            return (BasePropertyMetadata)propertyModel;
        }
    }
}
