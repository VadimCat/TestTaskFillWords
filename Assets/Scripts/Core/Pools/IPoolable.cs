namespace Core.Pools
{
    public interface IPoolable
    {
        void Spawn();
        void DeSpawn();
    }
}