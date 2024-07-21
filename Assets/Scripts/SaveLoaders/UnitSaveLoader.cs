using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SaveLoaderProject
{
    public class UnitSaveLoader : SaveLoader<UnitData>
    {
        private Transform unitsContainer;
        private UnitManager unitManager;
        private IGameRepository gameRepository;
        private PrefabInitializer prefabInitializer;

        public UnitSaveLoader(Transform unitsContainer, UnitManager unitManager, IGameRepository gameRepository,
            PrefabInitializer prefabInitializer)
        {
            this.unitsContainer = unitsContainer;
            this.unitManager = unitManager;
            this.unitManager.SetContainer(this.unitsContainer);

            this.gameRepository = gameRepository;
            this.prefabInitializer = prefabInitializer;
        }
       

        protected override UnitData ConvertToData()
        {
            IEnumerable<Unit> allUnits = this.unitManager.GetAllUnits();
            UnitData unitData = new UnitData();

            foreach (var unit in allUnits)
            {
                unitData.AddUnitData(unit);
            }

            return unitData;
        }

        protected override void SetupData(UnitData data)
        {
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