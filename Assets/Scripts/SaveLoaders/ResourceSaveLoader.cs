using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SaveLoaderProject
{
    public class ResourceSaveLoader : ISaveLoader
    {
        private ResourceService resourceService;
        private IGameRepository gameRepository;

        public ResourceSaveLoader(ResourceService resourceService, IGameRepository gameRepository)
        {
            this.resourceService = resourceService;
            this.gameRepository = gameRepository;
        }

        public void LoadGame()
        {
            Dictionary<string, Resource> currentResources = resourceService.GetResources().ToDictionary(resource => resource.ID);
            if(this.gameRepository.TryGetData(out ResourceData data))
            {
                foreach(var savedResource in data.savedData)
                {
                    if (currentResources.ContainsKey(savedResource.ID))
                    {
                        currentResources[savedResource.ID].Amount = savedResource.Amount;
                    }
                }
            }
        }

        public void SaveGame()
        {
            IEnumerable<Resource> resources = this.resourceService.GetResources();
            ResourceData data = new ResourceData();

            foreach (Resource resource in resources)
            {
                data.AddResourceData(resource);
            }

            this.gameRepository.SetData(data);
        }
    }
}