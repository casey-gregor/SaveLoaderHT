

namespace SaveLoaderProject
{
    public abstract class SaveLoader<TData>: ISaveLoader
    {
        public void LoadGame(IGameRepository gameRepository, IMonoHelper monoHelper)
        {
            if(gameRepository.TryGetData(out TData data))
            {
                SetupData(data, monoHelper);
            }
        }


        public void SaveGame(IGameRepository gameRepository, IMonoHelper monoHelper)
        {
            TData data = ConvertToData(monoHelper);
            gameRepository.SetData(data);
        }

        protected abstract TData ConvertToData(IMonoHelper monoHelper);
        protected abstract void SetupData(TData data, IMonoHelper monoHelper);
      
    }


}