using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace MyLibrary.Tests
{
    [TestClass]
    public class MyServiceTests
    {
        [TestMethod]
        public void TestPublicMethod()
        {
            var service = new MyService();
            Assert.AreEqual("Hello World", service.GetResult());
        }

#if NETCOREAPP3_1

        [TestMethod]
        public void TestPrivateMethod()
        {
            //var service = new MyService();
            //var method = typeof(MyService).GetMethod("PrivateGetResult", BindingFlags.NonPublic | BindingFlags.Instance);
            //var result = method.Invoke(service, new object[0]);

            //Assert.AreEqual("Private Hello World", result);

            // or
            var privateObject = new PrivateTestObject(new MyService());
            var result = privateObject.Invoke("PrivateGetResult");
            Assert.AreEqual("Private Hello World", result);
        }

#else

        [TestMethod]
        public void TestPrivateMethod()
        {
            var privateObject = new PrivateObject(new MyService());
            var result = privateObject.Invoke("PrivateGetResult");
            Assert.AreEqual("Private Hello World", result);
        }
#endif
    }

    public class PrivateTestObject
    {
        private object instance;

        public PrivateTestObject(object instance)
        {
            if (instance is null)
                throw new ArgumentNullException(nameof(instance));
            this.instance = instance;
        }

        public object Invoke(string methodName, object[] parameters)
        {
            MethodInfo method = instance.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            object result = method.Invoke(instance, parameters);
            return result;
        }

        public object Invoke(string methodName)
        {
            return Invoke(methodName, new object[0]);
        }
    }
}
