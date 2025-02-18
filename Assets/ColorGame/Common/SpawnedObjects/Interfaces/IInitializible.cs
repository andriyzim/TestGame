
namespace ColorGame.Common.SpawnedObjects.Interfaces
{
    public interface IInitializible<T>
    {
        void Initialize(T colorSet);
    }
}