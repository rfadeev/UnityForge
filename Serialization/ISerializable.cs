namespace UnityForge.Serialization
{
    interface ISerializable<T> : ILoader<T>, ISaver<T>
    {
    }
}
