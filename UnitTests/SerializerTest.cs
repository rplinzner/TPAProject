using BusinessLogic.Model;
using BusinessLogic.Serialization;
using BusinessLogic.ViewModel.ReflectionItems;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class SerializerTest
    {
        private string path = @"..\..\..\ClassesForTesting\bin\Debug\ClassesForTesting.dll";
        private string XMLFilePath = "test.xml";

        [TestMethod]
        public void HowManyNamespacesTest()
        {
            Reflector reflector = new Reflector(path);
            XMLSerializer xmlSerializer = new XMLSerializer();
            xmlSerializer.Serialize(XMLFilePath, reflector.AssemblyModel);
            AssemblyMetadata model = xmlSerializer.Deserialize<AssemblyMetadata>(XMLFilePath);
            Assert.AreEqual(3, model.Namespaces.Count);
        }
    }
}
