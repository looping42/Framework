using Framework.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Logger.Info("hello");
            Console.ReadLine();
        }
    }
}