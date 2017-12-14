using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Framework.BackTrace;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTestPermutString
    {
        [TestMethod]
        public void TestPermutString()
        {
            char[] str = "ABC".ToCharArray();
            int n = str.Length;
            PermutString.Permute(str, 0, n - 1);
        }
    }
}