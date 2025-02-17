using ColorGame.Common.ColorManagement;

namespace ColorGame.Common.SpawnedObjects.Interfaces
{
    public interface IInitializible
    {
        void Initialize(ColorSet colorSet);
    }
}