using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveLoaderProject
{
    public class ResourceData
    {
        public class SavedData
        {
            public string ID;
            public int Amount { get; set; }

        }

        public HashSet<SavedData> savedData;

        public ResourceData()
        {
            savedData = new HashSet<SavedData>();
        }

        public void AddResourceData(Resource resource)
        {
            SavedData data = new SavedData();
            data.ID = resource.ID;
            data.Amount = resource.Amount;
            savedData.Add(data);
        }
    }
}
