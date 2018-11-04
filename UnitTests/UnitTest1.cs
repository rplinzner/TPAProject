using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic.ViewModel;
using BusinessLogic.ViewModel.TreeViewItems;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            TreeViewParameter tw = new TreeViewParameter(null);
            Console.Out.WriteLine(tw.GetType());
        }
    }
}
