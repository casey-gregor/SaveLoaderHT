using UnityEngine;
using Zenject;

namespace SaveLoaderProject
{
    public class ActionHelper : MonoBehaviour
    {
        private UnitManager unitManager;
        private ResourceService resourceService;
        private SaveLoadManager saveLoadManager;

        [Inject]
        public void Constructor(UnitManager unitManager, ResourceService resourceService, SaveLoadManager saveLoadManager)
        {
            this.unitManager = unitManager;
            this.resourceService = resourceService;
            this.saveLoadManager = saveLoadManager;
        }

        private void Start()
        {
            this.unitManager.SetupUnits(FindObjectsOfType<Unit>());
            this.resourceService.SetResources(FindObjectsOfType<Resource>());
        }

        public void SaveGame()
        {
            this.saveLoadManager.SaveGame();
        }

        public void LoadGame()
        {
            this.saveLoadManager.LoadGame();
        }
    }

}
