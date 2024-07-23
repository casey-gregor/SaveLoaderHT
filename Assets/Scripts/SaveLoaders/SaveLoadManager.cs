using UnityEngine;

namespace SaveLoaderProject
{
    public class SaveLoadManager
    {
        private ISaveLoader[] saveLoaders;
        private GameRepository gameRepository;
        private readonly Encrypter encrypter;
        private readonly FileSaveLoader fileSaveLoader;

        public SaveLoadManager(
            ISaveLoader[] saveLoaders, 
            IGameRepository gameRepository,
            Encrypter encrypter,
            FileSaveLoader fileSaveLoader)
        {
            this.saveLoaders = saveLoaders;
            this.gameRepository = gameRepository as GameRepository;
            this.encrypter = encrypter;
            this.fileSaveLoader = fileSaveLoader;
        }

        public void SaveGame(IMonoHelper monoHelper)
        {
            foreach (var saveLoader in saveLoaders)
            {
                saveLoader.SaveGame(this.gameRepository, monoHelper);
            }

            string savedData = this.gameRepository.SaveState();
            string encryptedData = this.encrypter.EncryptDecrypt(savedData);
            this.fileSaveLoader.SaveToFile(encryptedData);        }

        public void LoadGame(IMonoHelper monoHelper)
        {
            string savedData = this.fileSaveLoader.LoadFromFile();
            string decryptedData = this.encrypter.EncryptDecrypt(savedData);
            if (!string.IsNullOrEmpty(decryptedData))
            {
                
                this.gameRepository.LoadState(decryptedData);    
                foreach(var loader in saveLoaders)
                {
                    loader.LoadGame(this.gameRepository, monoHelper);
                    
                }
                Debug.Log("GameState loaded successfully");
            }
            else
            {
                Debug.Log("Loading failed. File is not found or empty");
            }

        }
    }
}