using UnityEngine;
using Zenject;

namespace SaveLoaderProject
{

    public class ActionHelper : MonoBehaviour, IMonoHelper
    {
        private SaveLoadManager saveLoadManager;

        [Inject]
        public void Constructor(SaveLoadManager saveLoadManager)
        {
            this.saveLoadManager = saveLoadManager;
        }


        public void SaveGame()
        {
            this.saveLoadManager.SaveGame(this);
        }

        public void LoadGame()
        {
            this.saveLoadManager.LoadGame(this);
        }

        public T[] GetAllObjects<T>() where T : Object
        {
            return FindObjectsOfType<T>();
        }
    }

}
