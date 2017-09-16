namespace UnityForge.Serialization
{
    public interface ILoader<T>
    {
        void Load(T fromData);
    }
}
