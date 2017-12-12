using Framework.Fibonnaci;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject
{
    [TestClass]
    public class UnittestFibonnacci
    {
        [TestMethod]
        public void TestFibonnaci()
        {
            FibonnaciHeap<int> fibo = new FibonnaciHeap<int>();
            FibHeapNode<int> test = new FibHeapNode<int>(15, 12);
            FibHeapNode<int> test2 = new FibHeapNode<int>(17, 69);
            FibHeapNode<int> test3 = new FibHeapNode<int>(16, 70);
            FibHeapNode<int> test4 = new FibHeapNode<int>(19, 71);
            FibHeapNode<int> test5 = new FibHeapNode<int>(20, 172);

            fibo.Insert(test2);
            fibo.Insert(test3);
            fibo.Insert(test4);
            fibo.Insert(test5);
            fibo.Insert(test);
            fibo.display();
            fibo.DecreaseKey(test3, 45);
            fibo.display();
            Console.WriteLine(fibo.Min().Key);
            Console.WriteLine(fibo.Min().Data);
            FibHeapNode<int> temp = fibo.ExtractMin();
            Console.WriteLine("newnodepetit" + temp.Key);
            Console.WriteLine("newnodepetit" + temp.Data);
            fibo.display();

            fibo.Delete(test5);
            fibo.display();

            fibo.Delete(test4);
            fibo.display();

            fibo.Delete(test2);
            fibo.display();

            //fibo.display();
        }
    }
}