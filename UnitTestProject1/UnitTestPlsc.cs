using Framework.LongestCommonString;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject
{
    [TestClass]
    public class UnitTestPlsc
    {
        [TestMethod]
        public void PlscTest()
        {
            //string first = "AGATTA";
            string two = "GATTACA";
            string three = "TACAGA";
            string result;
            int numberlettercommon = Plsc.LongestCommonSubstring(two, three, out result);

            Console.WriteLine("numberlettercommon :" + numberlettercommon);
            Console.WriteLine(result);
        }
    }
}