using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicCall.Tests
{
    class TestReturnObject
    {
        public string A;
        public int X;
    }

    class TestObjectToCallNoParameter<TRet>
    {
        private TRet _returnObj;

        public TestObjectToCallNoParameter(TRet returnObj)
        {
            _returnObj = returnObj;
        }

        public TRet CallMethod()
        {
            return _returnObj;
        }
    }

    class TestObjectToCallOneParameter<TRet, T1>
    {
        private T1 _parameter1;
        private TRet _returnObj;

        public TestObjectToCallOneParameter(TRet returnObj, T1 parameter)
        {
            _returnObj = returnObj;
            _parameter1 = parameter;
        }

        public TRet CallMethod(T1 s)
        {
            Assert.AreEqual(s, _parameter1);
            return _returnObj;
        }
    }

    class TestObjectToCallTwoParameter<TRet, T1, T2>
    {
        private T1 _parameter1;
        private TRet _returnObj;
        private T2 _parameter2;

        public TestObjectToCallTwoParameter(TRet returnObj, T1 parameter1, T2 parameter2)
        {
            _returnObj = returnObj;
            _parameter1 = parameter1;
            _parameter2 = parameter2;
        }

        public TRet CallMethod(T1 s, T1 s2)
        {
            Assert.AreEqual(s, _parameter1);
            Assert.AreEqual(s2, _parameter2);

            return _returnObj;
        }
    }
}
