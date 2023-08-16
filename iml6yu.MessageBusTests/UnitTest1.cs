using iml6yu.MessageBus;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Concurrent;

namespace iml6yu.MessageBusTests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        P p = new P();
        Assert.AreEqual("T", Cons(p));
    }

    [TestMethod]
    public void TestMethod2()
    {
        P p = new P();
        //var o = (object)p;
        var r = Convert.ChangeType(p, typeof(P));
        Assert.AreEqual("T", Cons(r));
    }


    [TestMethod]
    public static void MyTestMethod()
    {
        ConcurrentQueue<dynamic> list = new ConcurrentQueue<dynamic>();
        AddMessage(list, new UnitTest1());

        var a = GetMessage(list);
        if (a != null)
        {
            var tp = a.Data.GetType();
            Assert.AreEqual(tp, typeof(UnitTest1));
        }
        else
            Assert.Fail();
    }

    public static void AddMessage<T>(ConcurrentQueue<dynamic> list, T message)
    {
        MessageBusArges<T> msg = new MessageBusArges<T>(message);
        list.Enqueue(msg);
    }

    public static dynamic GetMessage(ConcurrentQueue<dynamic> list)
    {
        if (list.TryDequeue(out dynamic e))
            return e;
        return null;
    }

    public string Cons<T>(T value)
    {
        return "T";
    }

    public string Cons(object value)
    {
        return "O";
    }

    public class P
    {
        //(int,string) x = Tuple.Create(24,"");


        public Tuple<int, string, bool> A()
        {
            var p = (24, "", true);// (24:age,"zs":name,true:isstudent);
            //(int, string) x = Tuple.Create(24, "zs");
            //(int, string) x1 = ValueTuple.Create(24, "zs");

            var person = (age: 24, name: "张三");
            person.age = 3;

            var person1 = Tuple.Create(24, "张三");
            var x1= person1.Item1;
            return null;
        }
    }

    public class TT<T>
    {
        public T A { get; set; }
    }
}