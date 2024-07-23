using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SaveLoaderProject
{
    public class UnitSaveLoader : SaveLoader<UnitData>
    {
        private Transform unitsContainer;
        private UnitManager unitManager;
        private PrefabInitializer prefabInitializer;

        public UnitSaveLoader(
            Transform unitsContainer, 
            UnitManager unitManager,
            PrefabInitializer prefabInitializer
            )
        {
            this.unitsContainer = unitsContainer;
            this.unitManager = unitManager;
            this.unitManager.SetContainer(this.unitsContainer);
            this.prefabInitializer = prefabInitializer;
        }
       

        protected override UnitData ConvertToData(IMonoHelper monoHelper)
        {

            this.unitManager.SetupUnits(monoHelper.GetAllObjects<Unit>());
            IEnumerable<Unit> allUnits = this.unitManager.GetAllUnits();
            UnitData unitData = new UnitData();

            foreach (var unit in allUnits)
            {
                unitData.AddUnitData(unit);
            }

            return unitData;
        }

        protected override void SetupData(UnitData data, IMonoHelper monoHelper)
        {
            this.unitManager.SetupUnits(monoHelper.GetAllObjects<Unit>());

            Dictionary<string, List<Unit>> unitsOnScene = new Dictionary<string, List<Unit>>();
            HashSet<UnitData.SavedData> savedData = new HashSet<UnitData.SavedData>(data.savedData);

            FillUnitsOnSceneDict(unitsOnScene);

            List<UnitData.SavedData> itemsToRemove = LoadDataInExistingUnits(data, unitsOnScene);

            foreach (var item in itemsToRemove)
            {
                savedData.Remove(item);
            }

            DestroyNotSavedUnits(unitsOnScene);

            InstantiateSavedUnits(savedData);
        }

        private void InstantiateSavedUnits(HashSet<UnitData.SavedData> savedData)
        {
            if (savedData.Count > 0)
            {
                foreach (var unit in savedData)
                {
                    Unit unitPrefab = GetPrefabByType(unit.type);
                    Unit newUnit = this.unitManager.SpawnUnit(unitPrefab, unit.Position, Quaternion.Euler(unit.Rotation));
                    newUnit.HitPoints = unit.HitPoints;
                    newUnit.name = unitPrefab.name;
                }
            }
        }

        private void DestroyNotSavedUnits(Dictionary<string, List<Unit>> unitsOnScene)
        {
            if (unitsOnScene.Count > 0)
            {
                foreach (var unitList in unitsOnScene.Values)
                {
                    foreach (var unit in unitList)
                    {
                        this.unitManager.DestroyUnit(unit);
                    }
                }
                unitsOnScene.Clear();
            }
        }

        private List<UnitData.SavedData> LoadDataInExistingUnits(UnitData data, Dictionary<string, List<Unit>> unitsOnScene)
        {
            var itemsToRemove = new List<UnitData.SavedData>();

            foreach (var item in data.savedData)
            {
                if (unitsOnScene.Keys.Contains(item.type))
                {
                    var unitToUpdate = unitsOnScene[item.type][0];
                    unitToUpdate.transform.position = item.Position;
                    unitToUpdate.transform.rotation = Quaternion.Euler(item.Rotation);
                    unitToUpdate.HitPoints = item.HitPoints;

                    unitsOnScene[item.type].Remove(unitToUpdate);

                    if (unitsOnScene[item.type].Count == 0)
                    {
                        unitsOnScene.Remove(item.type);
                    }
                    itemsToRemove.Add(item);
                }

            }

            return itemsToRemove;
        }

        private void FillUnitsOnSceneDict(Dictionary<string, List<Unit>> unitsOnScene)
        {
            foreach (var unit in this.unitManager.GetAllUnits())
            {
                if (unitsOnScene.ContainsKey(unit.Type))
                {
                    unitsOnScene[unit.Type].Add(unit);
                }
                else
                {
                    unitsOnScene[unit.Type] = new List<Unit>() { unit };
                }
            }
        }

        private Unit GetPrefabByType(string type)
        {
            if (this.prefabInitializer.unitPrefabDictionary.TryGetValue(type, out Unit prefab))
            {
                return prefab;
            }
            return null;
        }
    }
}