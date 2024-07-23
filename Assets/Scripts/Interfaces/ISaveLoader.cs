

namespace SaveLoaderProject
{
    public interface ISaveLoader
    {
        void SaveGame(IGameRepository gameRepository, IMonoHelper monoHelper);
        void LoadGame(IGameRepository gameRepository, IMonoHelper monoHelper);
       
    }

}