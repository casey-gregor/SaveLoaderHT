

namespace SaveLoaderProject
{
    public interface IMonoHelper
    {
        T[] GetAllObjects<T>() where T : UnityEngine.Object;
    }
}