using BusinessLogic.Model;
using XMLData;
using XMLData.XMLModel;
using BusinessLogic.ReflectionItems;
using Data.DataModel;
using BusinessLogic.Mapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using DBData;
using DBData.DBMetadata;
using System.Data.Entity;

namespace Tests.DBTests
{
    [TestClass]
    public class SerializerTest
    {
        private string path = @"..\..\..\TPA.ApplicationArchitecture.dll";
        private string XMLFilePath = "test.xml";
        private BaseAssemblyMetadata assemblyModel = new DBAssemblyMetadata();

        [TestMethod]
        public void HowManyNamespacesTest()
        {
            Reflector reflector = new Reflector(path);
            DatabaseHandler xmlSerializer = new DatabaseHandler();
            xmlSerializer.Serialize(null, AssemblyModelMapper.MapDown(reflector.AssemblyModel, assemblyModel));
            AssemblyMetadata model = AssemblyModelMapper.MapUp(xmlSerializer.Deserialize(null));
            Assert.AreEqual(2, model.Namespaces.Count);
        }

        [TestMethod]
        public void HowManyClassesTest()
        {
            Reflector reflector = new Reflector(path);
            DatabaseHandler xmlSerializer = new DatabaseHandler();
            xmlSerializer.Serialize(XMLFilePath, AssemblyModelMapper.MapDown(reflector.AssemblyModel, assemblyModel));
            AssemblyMetadata model = AssemblyModelMapper.MapUp(xmlSerializer.Deserialize(XMLFilePath));

            List<TypeMetadata> niceNamespaceTypes = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "TPA.ApplicationArchitecture.Data").Types;
            Assert.AreEqual(9, niceNamespaceTypes.Count);

            List<TypeMetadata> recursionTypes = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "TPA.ApplicationArchitecture.Data.CircularReference").Types;
            Assert.AreEqual(2, recursionTypes.Count);
        }

        [TestMethod]
        public void HowManyInterfacesTest()
        {
            Reflector reflector = new Reflector(path);
            DatabaseHandler xmlSerializer = new DatabaseHandler();
            xmlSerializer.Serialize(XMLFilePath, AssemblyModelMapper.MapDown(reflector.AssemblyModel, assemblyModel));
            AssemblyMetadata model = AssemblyModelMapper.MapUp(xmlSerializer.Deserialize(XMLFilePath));

            List<TypeMetadata> interfaces = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "TPA.ApplicationArchitecture.Data").Types
                .Where(t => t.Type == TypeEnum.Interface).ToList();
            Assert.AreEqual(1, interfaces.Count);
        }

        [TestMethod]
        public void HowManyClassesWithImplementedInterfacesTest()
        {
            Reflector reflector = new Reflector(path);
            DatabaseHandler xmlSerializer = new DatabaseHandler();
            xmlSerializer.Serialize(XMLFilePath, AssemblyModelMapper.MapDown(reflector.AssemblyModel, assemblyModel));
            AssemblyMetadata model = AssemblyModelMapper.MapUp(xmlSerializer.Deserialize(XMLFilePath));

            List<TypeMetadata> classesWithImplementedInterfaces = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "TPA.ApplicationArchitecture.Data").Types
                .Where(t => t.ImplementedInterfaces.Count > 0).ToList();
            Assert.AreEqual(1, classesWithImplementedInterfaces.Count);
        }

        [TestMethod]
        public void HowManyPublicClassesTest()
        {
            Reflector reflector = new Reflector(path);
            DatabaseHandler xmlSerializer = new DatabaseHandler();
            xmlSerializer.Serialize(XMLFilePath, AssemblyModelMapper.MapDown(reflector.AssemblyModel, assemblyModel));
            AssemblyMetadata model = AssemblyModelMapper.MapUp(xmlSerializer.Deserialize(XMLFilePath));

            List<TypeMetadata> publicClasses = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "TPA.ApplicationArchitecture.Data").Types
                .Where(t => t.Modifiers.AccessLevel == AccessLevel.Public).ToList();
            Assert.AreEqual(9, publicClasses.Count);
        }

        [TestMethod]
        public void HowManyAbstractClassesTest()
        {
            Reflector reflector = new Reflector(path);
            DatabaseHandler xmlSerializer = new DatabaseHandler();
            xmlSerializer.Serialize(XMLFilePath, AssemblyModelMapper.MapDown(reflector.AssemblyModel, assemblyModel));
            AssemblyMetadata model = AssemblyModelMapper.MapUp(xmlSerializer.Deserialize(XMLFilePath));

            List<TypeMetadata> abstractClasses = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "TPA.ApplicationArchitecture.Data").Types
                .Where(t => t.Modifiers.AbstractEnum == AbstractEnum.Abstract).ToList();
            Assert.AreEqual(2, abstractClasses.Count);
        }

        [TestMethod]
        public void HowManyClassesWithBaseTypeTest()
        {
            Reflector reflector = new Reflector(path);
            DatabaseHandler xmlSerializer = new DatabaseHandler();
            xmlSerializer.Serialize(XMLFilePath, AssemblyModelMapper.MapDown(reflector.AssemblyModel, assemblyModel));
            AssemblyMetadata model = AssemblyModelMapper.MapUp(xmlSerializer.Deserialize(XMLFilePath));

            List<TypeMetadata> classesWithBaseType = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "TPA.ApplicationArchitecture.Data").Types
                .Where(t => t.BaseType != null).ToList();
            Assert.AreEqual(1, classesWithBaseType.Count);
        }

        [TestMethod]
        public void HowManyStaticClassesTest()
        {
            Reflector reflector = new Reflector(path);
            DatabaseHandler xmlSerializer = new DatabaseHandler();
            xmlSerializer.Serialize(XMLFilePath, AssemblyModelMapper.MapDown(reflector.AssemblyModel, assemblyModel));
            AssemblyMetadata model = AssemblyModelMapper.MapUp(xmlSerializer.Deserialize(XMLFilePath));

            List<TypeMetadata> staticClasses = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "TPA.ApplicationArchitecture.Data").Types
                .Where(t => t.Modifiers.StaticEnum == StaticEnum.Static).ToList();
            Assert.AreEqual(1, staticClasses.Count);
        }

        [TestMethod]
        public void HowManyClassesWithGenericArgsTest()
        {
            Reflector reflector = new Reflector(path);
            DatabaseHandler xmlSerializer = new DatabaseHandler();
            xmlSerializer.Serialize(XMLFilePath, AssemblyModelMapper.MapDown(reflector.AssemblyModel, assemblyModel));
            AssemblyMetadata model = AssemblyModelMapper.MapUp(xmlSerializer.Deserialize(XMLFilePath));

            List<TypeMetadata> genericClasses = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "TPA.ApplicationArchitecture.Data").Types
                .Where(t => t.GenericArguments != null).ToList();
            Assert.AreEqual(1, genericClasses.Count);
        }

        [TestMethod]
        public void HowManyClassesWithNestedTypesTest()
        {
            Reflector reflector = new Reflector(path);
            DatabaseHandler xmlSerializer = new DatabaseHandler();
            xmlSerializer.Serialize(XMLFilePath, AssemblyModelMapper.MapDown(reflector.AssemblyModel, assemblyModel));
            AssemblyMetadata model = AssemblyModelMapper.MapUp(xmlSerializer.Deserialize(XMLFilePath));

            List<TypeMetadata> classesWithNestedTypes = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "TPA.ApplicationArchitecture.Data").Types
                .Where(t => t.NestedTypes.Count > 0).ToList();
            Assert.AreEqual(1, classesWithNestedTypes.Count);
        }

        [TestMethod]
        public void HowManyConstructorsInClassTest()
        {
            Reflector reflector = new Reflector(path);
            DatabaseHandler xmlSerializer = new DatabaseHandler();
            xmlSerializer.Serialize(XMLFilePath, AssemblyModelMapper.MapDown(reflector.AssemblyModel, assemblyModel));
            AssemblyMetadata model = AssemblyModelMapper.MapUp(xmlSerializer.Deserialize(XMLFilePath));

            List<TypeMetadata> classes = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "TPA.ApplicationArchitecture.Data").Types
                .Where(t => t.Name == "ClassWithAttribute").ToList();
            Assert.AreEqual(1, classes.First().Constructors.Count);
        }

        [TestMethod]
        public void HowManyMethodsInClassTest()
        {
            Reflector reflector = new Reflector(path);
            DatabaseHandler xmlSerializer = new DatabaseHandler();
            xmlSerializer.Serialize(XMLFilePath, AssemblyModelMapper.MapDown(reflector.AssemblyModel, assemblyModel));
            AssemblyMetadata model = AssemblyModelMapper.MapUp(xmlSerializer.Deserialize(XMLFilePath));

            List<TypeMetadata> classes = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "TPA.ApplicationArchitecture.Data").Types
                .Where(t => t.Name == "StaticClass").ToList();
            Assert.AreEqual(4, classes.First().Methods.Count);
        }

        [TestMethod]
        public void HowManyFieldsInClassTest()
        {
            Reflector reflector = new Reflector(path);
            DatabaseHandler xmlSerializer = new DatabaseHandler();
            xmlSerializer.Serialize(XMLFilePath, AssemblyModelMapper.MapDown(reflector.AssemblyModel, assemblyModel));
            AssemblyMetadata model = AssemblyModelMapper.MapUp(xmlSerializer.Deserialize(XMLFilePath));

            List<TypeMetadata> classes = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "TPA.ApplicationArchitecture.Data").Types
                .Where(t => t.Name == "StaticClass").ToList();
            Assert.AreEqual(2, classes.First().Fields.Count);
        }

        [TestMethod]
        public void HowManyPropertiesInClassTest()
        {
            Reflector reflector = new Reflector(path);
            DatabaseHandler xmlSerializer = new DatabaseHandler();
            xmlSerializer.Serialize(XMLFilePath, AssemblyModelMapper.MapDown(reflector.AssemblyModel, assemblyModel));
            AssemblyMetadata model = AssemblyModelMapper.MapUp(xmlSerializer.Deserialize(XMLFilePath));

            List<TypeMetadata> classes = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "TPA.ApplicationArchitecture.Data").Types
                .Where(t => t.Name == "StaticClass").ToList();
            Assert.AreEqual(1, classes.First().Properties.Count);
        }
    }
}
