using DynamicCall.Serializer;

namespace DynamicCall.Tests
{
    class TestSerializer : ISerializer
    {
        private object[] _objects;

        public TestSerializer(params object[] objects)
        {
            _objects = objects;
        }

        public T Get<T>(int index)
        {
            return (T)_objects[index];
        }
    }
}
