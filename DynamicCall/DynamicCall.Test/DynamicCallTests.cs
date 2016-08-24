using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicCall.Tests
{
    [TestClass()]
    public class DynamicCallTests
    {
        [TestMethod()]
        public void ConstructorTest()
        {
            var dc = new DynamicCall();

            Assert.IsNotNull(dc);
            Assert.IsInstanceOfType(dc, typeof(DynamicCall));
        }

        [TestMethod()]
        public void NoParameterIntReturn()
        {
            var testSerializer = new TestSerializer();
            var testObjectToCall = new TestObjectToCallNoParameter<int>(1806);
            var className = testObjectToCall.GetType().Name;
            var method = testObjectToCall.GetType().GetMethod("CallMethod");
            var methodName = method.Name;

            var dc = new DynamicCall();

            dc.Add(testObjectToCall, method);
            dc.Add(testObjectToCall, method, "testInterfaceName");
            dc.Add(testObjectToCall, method, "testInterfaceName", "weirdMethodName");
            dc.Add(testObjectToCall, method, methodName: "weirdMethodName");

            Assert.AreEqual(dc.Call(className, methodName, testSerializer), testObjectToCall.CallMethod());
            Assert.AreEqual(dc.Call("testInterfaceName", methodName, testSerializer), testObjectToCall.CallMethod());
            Assert.AreEqual(dc.Call("testInterfaceName", "weirdMethodName", testSerializer), testObjectToCall.CallMethod());
            Assert.AreEqual(dc.Call(className, "weirdMethodName", testSerializer), testObjectToCall.CallMethod());
        }

        [TestMethod()]
        public void NoParameterStringReturn()
        {
            var testSerializer = new TestSerializer();
            var testObjectToCall = new TestObjectToCallNoParameter<string>("Shane");
            var className = testObjectToCall.GetType().Name;
            var method = testObjectToCall.GetType().GetMethod("CallMethod");
            var methodName = method.Name;

            var dc = new DynamicCall();

            dc.Add(testObjectToCall, method);
            dc.Add(testObjectToCall, method, "testInterfaceName");
            dc.Add(testObjectToCall, method, "testInterfaceName", "weirdMethodName");
            dc.Add(testObjectToCall, method, methodName: "weirdMethodName");

            Assert.AreEqual(dc.Call(className, methodName, testSerializer), testObjectToCall.CallMethod());
            Assert.AreEqual(dc.Call("testInterfaceName", methodName, testSerializer), testObjectToCall.CallMethod());
            Assert.AreEqual(dc.Call("testInterfaceName", "weirdMethodName", testSerializer), testObjectToCall.CallMethod());
            Assert.AreEqual(dc.Call(className, "weirdMethodName", testSerializer), testObjectToCall.CallMethod());
        }

        [TestMethod()]
        public void NoParameterObjectReturn()
        {
            var testSerializer = new TestSerializer();
            var testObjectToCall = new TestObjectToCallNoParameter<TestReturnObject>(new TestReturnObject { A = "Shane", X = 1806 });
            var className = testObjectToCall.GetType().Name;
            var method = testObjectToCall.GetType().GetMethod("CallMethod");
            var methodName = method.Name;

            var dc = new DynamicCall();

            dc.Add(testObjectToCall, method);
            dc.Add(testObjectToCall, method, "testInterfaceName");
            dc.Add(testObjectToCall, method, "testInterfaceName", "weirdMethodName");
            dc.Add(testObjectToCall, method, methodName: "weirdMethodName");

            Assert.AreEqual(dc.Call(className, methodName, testSerializer), testObjectToCall.CallMethod());
            Assert.AreEqual(dc.Call("testInterfaceName", methodName, testSerializer), testObjectToCall.CallMethod());
            Assert.AreEqual(dc.Call("testInterfaceName", "weirdMethodName", testSerializer), testObjectToCall.CallMethod());
            Assert.AreEqual(dc.Call(className, "weirdMethodName", testSerializer), testObjectToCall.CallMethod());
        }

        [TestMethod()]
        public void OneIntParameter()
        {
            var testSerializer = new TestSerializer(1990);
            var testObjectToCall = new TestObjectToCallOneParameter<int, int>(1806, 1990);
            var className = testObjectToCall.GetType().Name;
            var method = testObjectToCall.GetType().GetMethod("CallMethod");
            var methodName = method.Name;

            var dc = new DynamicCall();

            dc.Add(testObjectToCall, method);
            dc.Add(testObjectToCall, method, "testInterfaceName");
            dc.Add(testObjectToCall, method, "testInterfaceName", "weirdMethodName");
            dc.Add(testObjectToCall, method, methodName: "weirdMethodName");

            Assert.AreEqual(dc.Call(className, methodName, testSerializer), testObjectToCall.CallMethod(testSerializer.Get<int>(0)));
            Assert.AreEqual(dc.Call("testInterfaceName", methodName, testSerializer), testObjectToCall.CallMethod(testSerializer.Get<int>(0)));
            Assert.AreEqual(dc.Call("testInterfaceName", "weirdMethodName", testSerializer), testObjectToCall.CallMethod(testSerializer.Get<int>(0)));
            Assert.AreEqual(dc.Call(className, "weirdMethodName", testSerializer), testObjectToCall.CallMethod(testSerializer.Get<int>(0)));
        }

        [TestMethod()]
        public void OneStringParameter()
        {
            var testSerializer = new TestSerializer("Grueling");
            var testObjectToCall = new TestObjectToCallOneParameter<string, string>("Shane", "Grueling");
            var className = testObjectToCall.GetType().Name;
            var method = testObjectToCall.GetType().GetMethod("CallMethod");
            var methodName = method.Name;

            var dc = new DynamicCall();

            dc.Add(testObjectToCall, method);
            dc.Add(testObjectToCall, method, "testInterfaceName");
            dc.Add(testObjectToCall, method, "testInterfaceName", "weirdMethodName");
            dc.Add(testObjectToCall, method, methodName: "weirdMethodName");

            Assert.AreEqual(dc.Call(className, methodName, testSerializer), testObjectToCall.CallMethod(testSerializer.Get<string>(0)));
            Assert.AreEqual(dc.Call("testInterfaceName", methodName, testSerializer), testObjectToCall.CallMethod(testSerializer.Get<string>(0)));
            Assert.AreEqual(dc.Call("testInterfaceName", "weirdMethodName", testSerializer), testObjectToCall.CallMethod(testSerializer.Get<string>(0)));
            Assert.AreEqual(dc.Call(className, "weirdMethodName", testSerializer), testObjectToCall.CallMethod(testSerializer.Get<string>(0)));
        }

        [TestMethod()]
        public void OneObjectParameter()
        {
            var o = new TestReturnObject { A = "Chen", X = 1806 };
            var testSerializer = new TestSerializer(o);
            var testObjectToCall = new TestObjectToCallOneParameter<TestReturnObject, TestReturnObject>(new TestReturnObject { A = "Shane", X = 1806 }, o);
            var className = testObjectToCall.GetType().Name;
            var method = testObjectToCall.GetType().GetMethod("CallMethod");
            var methodName = method.Name;

            var dc = new DynamicCall();

            dc.Add(testObjectToCall, method);
            dc.Add(testObjectToCall, method, "testInterfaceName");
            dc.Add(testObjectToCall, method, "testInterfaceName", "weirdMethodName");
            dc.Add(testObjectToCall, method, methodName: "weirdMethodName");

            Assert.AreEqual(dc.Call(className, methodName, testSerializer), testObjectToCall.CallMethod(testSerializer.Get<TestReturnObject>(0)));
            Assert.AreEqual(dc.Call("testInterfaceName", methodName, testSerializer), testObjectToCall.CallMethod(testSerializer.Get<TestReturnObject>(0)));
            Assert.AreEqual(dc.Call("testInterfaceName", "weirdMethodName", testSerializer), testObjectToCall.CallMethod(testSerializer.Get<TestReturnObject>(0)));
            Assert.AreEqual(dc.Call(className, "weirdMethodName", testSerializer), testObjectToCall.CallMethod(testSerializer.Get<TestReturnObject>(0)));
        }
    }
}