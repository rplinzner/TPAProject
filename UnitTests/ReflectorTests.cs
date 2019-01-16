using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel;
using ViewModel.TreeViewItems;
using BusinessLogic.ReflectionItems;
using System.Collections.Generic;
using BusinessLogic.Model;
using System.Linq;
using Data.DataModel;

namespace Tests.ReflectorTests
{
    [TestClass]
    public class ReflectorTests
    {
        private string path = @"..\..\..\ClassesForTesting\bin\Debug\ClassesForTesting.dll";

        [TestMethod]
        public void ReflectorConstructorTest()
        {
            Reflector reflector = new Reflector(path);
            Assert.AreEqual("ClassesForTesting.dll", reflector.AssemblyModel.Name);
            Assert.ThrowsException<ArgumentNullException>(() => new Reflector(""));
        }
        
        [TestMethod]
        public void HowManyNamespacesTest()
        {
            Reflector reflector = new Reflector(path);
            Assert.AreEqual(3, reflector.AssemblyModel.Namespaces.Count);
        }

        [TestMethod]
        public void HowManyClassesTest()
        {
            Reflector reflector = new Reflector(path);

            List<TypeMetadata> classesForTestingTypes = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "Tests.ClassesForTesting").Types;
            Assert.AreEqual(5, classesForTestingTypes.Count);

            List<TypeMetadata> niceNamespaceTypes = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "Tests.ClassesForTesting.NiceNamespace").Types;
            Assert.AreEqual(5, niceNamespaceTypes.Count);

            List<TypeMetadata> recursionTypes = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "Tests.ClassesForTesting.Recursion").Types;
            Assert.AreEqual(3, recursionTypes.Count);
        }

        [TestMethod]
        public void HowManyInterfacesTest()
        {
            Reflector reflector = new Reflector(path);
            List<TypeMetadata> interfaces = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "Tests.ClassesForTesting").Types
                .Where(t => t.Type == TypeEnum.Interface).ToList();
            Assert.AreEqual(1, interfaces.Count);
        }

        [TestMethod]
        public void HowManyClassesWithImplementedInterfacesTest()
        {
            Reflector reflector = new Reflector(path);
            List<TypeMetadata> classesWithImplementedInterfaces = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "Tests.ClassesForTesting").Types
                .Where(t => t.ImplementedInterfaces.Count > 0).ToList();
            Assert.AreEqual(1, classesWithImplementedInterfaces.Count);
        }

        [TestMethod]
        public void HowManyPublicClassesTest()
        {
            Reflector reflector = new Reflector(path);
            List<TypeMetadata> publicClasses = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "Tests.ClassesForTesting.NiceNamespace").Types
                .Where(t => t.Modifiers.AccessLevel == AccessLevel.Public).ToList();
            Assert.AreEqual(5, publicClasses.Count);
        }

        [TestMethod]
        public void HowManyAbstractClassesTest()
        {
            Reflector reflector = new Reflector(path);
            List<TypeMetadata> abstractClasses = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "Tests.ClassesForTesting").Types
                .Where(t => t.Modifiers.AbstractEnum == AbstractEnum.Abstract).ToList();
            Assert.AreEqual(2, abstractClasses.Count);
        }

        [TestMethod]
        public void HowManyClassesWithBaseTypeTest()
        {
            Reflector reflector = new Reflector(path);
            List<TypeMetadata> classesWithBaseType = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "Tests.ClassesForTesting").Types
                .Where(t => t.BaseType != null).ToList();
            Assert.AreEqual(1, classesWithBaseType.Count);
        }

        [TestMethod]
        public void HowManyStaticClassesTest()
        {
            Reflector reflector = new Reflector(path);
            List<TypeMetadata> staticClasses = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "Tests.ClassesForTesting").Types
                .Where(t => t.Modifiers.StaticEnum == StaticEnum.Static).ToList();
            Assert.AreEqual(1, staticClasses.Count);
        }

        [TestMethod]
        public void HowManyClassesWithGenericArgsTest()
        {
            Reflector reflector = new Reflector(path);
            List<TypeMetadata> genericClasses = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "Tests.ClassesForTesting.NiceNamespace").Types
                .Where(t => t.GenericArguments != null).ToList();
            Assert.AreEqual(1, genericClasses.Count);
        }

        [TestMethod]
        public void HowManyClassesWithNestedTypesTest()
        {
            Reflector reflector = new Reflector(path);
            List<TypeMetadata> classesWithNestedTypes = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "Tests.ClassesForTesting.NiceNamespace").Types
                .Where(t => t.NestedTypes.Count > 0).ToList();
            Assert.AreEqual(2, classesWithNestedTypes.Count);
        }

        [TestMethod]
        public void HowManyConstructorsInClassTest()
        {
            Reflector reflector = new Reflector(path);
            List<TypeMetadata> classes = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "Tests.ClassesForTesting").Types
                .Where(t => t.Name == "Class1").ToList();
            Assert.AreEqual(2, classes.First().Constructors.Count);
        }

        [TestMethod]
        public void HowManyMethodsInClassTest()
        {
            Reflector reflector = new Reflector(path);
            List<TypeMetadata> classes = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "Tests.ClassesForTesting").Types
                .Where(t => t.Name == "Class1").ToList();
            Assert.AreEqual(4, classes.First().Methods.Count);
        }

        [TestMethod]
        public void HowManyFieldsInClassTest()
        {
            Reflector reflector = new Reflector(path);
            List<TypeMetadata> classes = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "Tests.ClassesForTesting").Types
                .Where(t => t.Name == "StaticClass1").ToList();
            Assert.AreEqual(1, classes.First().Fields.Count);
        }

        [TestMethod]
        public void HowManyPropertiesInClassTest()
        {
            Reflector reflector = new Reflector(path);
            List<TypeMetadata> classes = reflector.AssemblyModel.Namespaces
                .Find(t => t.Name == "Tests.ClassesForTesting.NiceNamespace").Types
                .Where(t => t.Name == "ClassWithClass").ToList();
            Assert.AreEqual(2, classes.First().Properties.Count);
        }
    }
}
