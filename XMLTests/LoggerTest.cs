using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Logging;
using FileLogging;

namespace Tests.XMLTests
{
    [TestClass]
    public class LoggerTest
    {

            [TestMethod]
            public void TestLoggingWorking()
            {
                if (File.Exists("logger.log"))
                    File.Delete("logger.log");

                ILogger logger = new FileLogger();
                logger.Log(new MessageStructure("test", "testOrigin", "testFilename", 3));

                Assert.IsTrue(File.Exists("logger.log"));
            }
    }
}
