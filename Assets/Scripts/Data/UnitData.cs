using System.Collections.Generic;
using UnityEngine;

namespace SaveLoaderProject
{
    public class UnitData
    {
        public HashSet<SavedData> savedData;
        public class SavedData
        {
            public string type;
            public int HitPoints {  get; set; }
            public Vector3 Position { get; set; }
            public Vector3 Rotation { get; set; }

        }


        public UnitData()
        {
            this.savedData = new HashSet<SavedData>();
        }

        public void AddUnitData(Unit unit)
        {
            SavedData newData = new SavedData();
            newData.type = unit.Type;
            newData.HitPoints = unit.HitPoints;
            newData.Position = unit.Position;
            newData.Rotation = unit.Rotation;

            this.savedData.Add(newData);

        }

    }

   
}