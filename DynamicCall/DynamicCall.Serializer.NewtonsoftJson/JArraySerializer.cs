using Newtonsoft.Json.Linq;

namespace DynamicCall.Serializer
{
    public class JArraySerializer : ISerializer
    {
        private JArray _jArray;

        public JArraySerializer(JArray jArray)
        {
            _jArray = jArray;
        }

        public T Get<T>(int index)
        {
            return _jArray[index].ToObject<T>();
        }
    }
}
