namespace DynamicCall.Serializer
{
    public interface ISerializer
    {
        T Get<T>(int index);
    }
}
