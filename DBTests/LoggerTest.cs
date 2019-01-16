using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Logging;
using DBData;

namespace Tests.DBTests
{

    [TestClass]
    public class LoggerTest
    {

            [TestMethod]
            public void TestLoggingWorking()
            {
                ILogger logger = new DatabaseLogger();
                logger.Log(new MessageStructure("test", "testOrigin", "testFilename", 3));
            }
    }
}
