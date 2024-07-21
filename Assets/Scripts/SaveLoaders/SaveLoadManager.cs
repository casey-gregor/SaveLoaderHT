using UnityEngine;

namespace SaveLoaderProject
{
    public class SaveLoadManager
    {
        private ISaveLoader[] saveLoaders;
        private GameRepository gameRepository;

        public SaveLoadManager(ISaveLoader[] saveLoaders, IGameRepository gameRepository)
        {
            this.saveLoaders = saveLoaders;
            this.gameRepository = gameRepository as GameRepository;
        }

        public void SaveGame()
        {
            foreach (var loader in saveLoaders)
            {
                loader.SaveGame();
            }

            this.gameRepository.SaveState();

        }

        public void LoadGame()
        {
            this.gameRepository.LoadState();
            
            foreach(var loader in saveLoaders)
            {
                loader.LoadGame();
            }

        }
    }
}