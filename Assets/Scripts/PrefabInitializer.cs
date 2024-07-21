using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveLoaderProject
{
    public class PrefabInitializer : MonoBehaviour
    {
        [SerializeField] private Unit[] unitPrefabArray;
        public Dictionary<string, Unit> unitPrefabDictionary {  get; private set; }

        private void Start()
        {
            unitPrefabDictionary = new Dictionary<string, Unit>();

            foreach(var unitPrefab in unitPrefabArray)
            {
                if (!unitPrefabDictionary.ContainsKey(unitPrefab.Type))
                {
                    unitPrefabDictionary.Add(unitPrefab.Type, unitPrefab);
                }
            }
        }
    }
}
