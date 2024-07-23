using System.Collections.Generic;
using System.Linq;

namespace SaveLoaderProject
{
    public class ResourceSaveLoader : SaveLoader<ResourceData>
    {
        private ResourceService resourceService;

        public ResourceSaveLoader(ResourceService resourceService)
        {
            this.resourceService = resourceService;
        }

        protected override ResourceData ConvertToData(IMonoHelper helper)
        {
            this.resourceService.SetResources(helper.GetAllObjects<Resource>());
            IEnumerable<Resource> resources = this.resourceService.GetResources();
            ResourceData data = new ResourceData();

            foreach (Resource resource in resources)
            {
                data.AddResourceData(resource);
            }

            return data;
        }

        protected override void SetupData(ResourceData data, IMonoHelper helper)
        {
            this.resourceService.SetResources(helper.GetAllObjects<Resource>());
            Dictionary<string, Resource> currentResources = resourceService.GetResources().ToDictionary(resource => resource.ID);
            foreach (var savedResource in data.savedData)
            {
                if (currentResources.ContainsKey(savedResource.ID))
                {
                    currentResources[savedResource.ID].Amount = savedResource.Amount;
                }
            }
        }
    }
}