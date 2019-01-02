using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BusinessLogic.Model;
using Data.DataModel;

namespace BusinessLogic.Mapper
{
    public class MethodModelMapper
    {

        public MethodMetadata MapUp(BaseMethodMetadata model)
        {
            MethodMetadata methodModel = new MethodMetadata();
            methodModel.Name = model.Name;
            methodModel.Extension = model.Extension;
            Type type = model.GetType();
            PropertyInfo genericArgumentsProperty = type.GetProperty("GenericArguments", 
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (genericArgumentsProperty?.GetValue(model) != null)
            {
                List<BaseTypeMetadata> genericArguments =
                    (List<BaseTypeMetadata>)HelperClass.ConvertList(typeof(BaseTypeMetadata),
                        (IList)genericArgumentsProperty?.GetValue(model));
                methodModel.GenericArguments =
                    genericArguments.Select(g => TypeModelMapper.EmitType(g)).ToList();
            }

            methodModel.Modifiers = model.Modifiers;

            PropertyInfo parametersProperty = type.GetProperty("Parameters",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (parametersProperty?.GetValue(model) != null)
            {
                List<BaseParameterMetadata> parameters =
                    (List<BaseParameterMetadata>)HelperClass.ConvertList(typeof(BaseParameterMetadata),
                        (IList)parametersProperty?.GetValue(model));

                methodModel.Parameters = parameters
                    .Select(p => new ParameterModelMapper().MapUp(p)).ToList();
            }

            PropertyInfo returnTypeProperty = type.GetProperty("ReturnType", 
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            BaseTypeMetadata returnType = (BaseTypeMetadata)returnTypeProperty?.GetValue(model);
            if (returnType != null)
                methodModel.ReturnType = TypeModelMapper.EmitType(returnType);
            return methodModel;
        }

        public BaseMethodMetadata MapDown(MethodMetadata model, Type methodModelType)
        {
            object methodModel = Activator.CreateInstance(methodModelType);
            PropertyInfo nameProperty = methodModelType.GetProperty("Name");
            PropertyInfo extensionProperty = methodModelType.GetProperty("Extension");
            PropertyInfo genericArgumentsProperty = methodModelType.GetProperty("GenericArguments", 
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            PropertyInfo modifiersProperty = methodModelType.GetProperty("Modifiers");
            PropertyInfo parametersProperty = methodModelType.GetProperty("Parameters", 
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            PropertyInfo returnTypeProperty = methodModelType.GetProperty("ReturnType",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

            nameProperty?.SetValue(methodModel, model.Name);
            extensionProperty?.SetValue(methodModel, model.Extension);
            if (model.GenericArguments != null)
                genericArgumentsProperty?.SetValue(methodModel, 
                    HelperClass.ConvertList(genericArgumentsProperty.PropertyType.GetGenericArguments()[0], 
                        model.GenericArguments.Select(t => TypeModelMapper.EmitBaseType(t, genericArgumentsProperty.PropertyType.GetGenericArguments()[0])).ToList()));
            modifiersProperty?.SetValue(methodModel, model.Modifiers);
            if (model.Parameters != null)
                parametersProperty?.SetValue(methodModel, 
                    HelperClass.ConvertList(parametersProperty.PropertyType.GetGenericArguments()[0], 
                        model.Parameters.Select(p => new ParameterModelMapper().MapDown(p, parametersProperty.PropertyType.GetGenericArguments()[0])).ToList()));
            if (model.ReturnType != null)
                returnTypeProperty?.SetValue(methodModel,
                    returnTypeProperty.PropertyType.Cast(TypeModelMapper.EmitBaseType(model.ReturnType, returnTypeProperty.PropertyType)));

            return (BaseMethodMetadata)methodModel;
        }
    }
}
