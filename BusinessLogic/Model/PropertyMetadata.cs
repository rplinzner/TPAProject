using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BusinessLogic.Model
{
    public class PropertyMetadata
    {
        #region Properties

        public string Name { get; set; }
        public TypeMetadata Type { get; set; }
        #endregion

        public PropertyMetadata(string name, TypeMetadata propertyType)
        {
            Name = name;
            Type = propertyType;
        }
        public static List<PropertyMetadata> EmitProperties(Type type)
        {
            //use binding flags to get not only public stuff
            List<PropertyInfo> props = type
                .GetProperties(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                               BindingFlags.Static | BindingFlags.Instance).ToList();//

            return props.Where(t => t.GetGetMethod().GetVisible() || t.GetSetMethod().GetVisible())
                .Select(t => new PropertyMetadata(t.Name, TypeMetadata.EmitReference(t.PropertyType))).ToList();
        }
    }
}