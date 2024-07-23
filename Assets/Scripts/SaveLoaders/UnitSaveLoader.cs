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
            var allUnits = this.unitManager.GetAllUnits().ToList();

            foreach (var unit in allUnits)
            {
                unitManager.DestroyUnit(unit);
            }

            foreach (var unit in data.savedData)
            {
                Unit unitPrefab = GetPrefabByType(unit.type);
                if (unitPrefab != null)
                {
                    Quaternion rotation = Quaternion.Euler(unit.Rotation);
                    Unit newUnit = this.unitManager.SpawnUnit(unitPrefab, unit.Position, rotation);
                    newUnit.name = unitPrefab.name;
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