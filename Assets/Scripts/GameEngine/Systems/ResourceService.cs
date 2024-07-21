using System;
using System.Collections.Generic;
using System.Linq;

namespace SaveLoaderProject
{
    //Нельзя менять!
    [Serializable]
    public sealed class ResourceService
    {
        private Dictionary<string, Resource> sceneResources = new();

        public void SetResources(IEnumerable<Resource> resources)
        {
            this.sceneResources = resources.ToDictionary(it => it.ID);
        }

        public IEnumerable<Resource> GetResources()
        {
            return this.sceneResources.Values;
        }
    }
}