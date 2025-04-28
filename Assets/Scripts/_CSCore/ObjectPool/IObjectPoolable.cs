namespace BunSamaCSCore
{
    public interface IObjectPoolable
    {
        void OnObjectSpawn(object data, GameObjectPool poolMachine);
    }
}
