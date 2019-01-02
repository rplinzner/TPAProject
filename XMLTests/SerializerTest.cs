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
using System.Text;
using System.Threading.Tasks;

namespace XMLTests
{
    [TestClass]
    public class SerializerTest
    {
        private string path = @"..\..\..\ClassesForTesting\bin\Debug\ClassesForTesting.dll";
        private string XMLFilePath = "test.xml";
        private BaseAssemblyMetadata assemblyModel = new XMLAssemblyMetadata();

        [TestMethod]
        public void HowManyNamespacesTest()
        {
            Reflector reflector = new Reflector(path);
            XMLSerializer xmlSerializer = new XMLSerializer();
            xmlSerializer.Serialize(path, AssemblyModelMapper.MapDown(reflector.AssemblyModel, assemblyModel.GetType()));
            AssemblyMetadata model = AssemblyModelMapper.MapUp(xmlSerializer.Deserialize(path));
            Assert.AreEqual(3, model.Namespaces.Count);
        }
    }
}
