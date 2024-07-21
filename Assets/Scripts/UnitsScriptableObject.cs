using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveLoaderProject
{

    [CreateAssetMenu(fileName ="UnitConfig", menuName ="Config/New UnitConfig")]
    public class UnitsScriptableObject : ScriptableObject
    {
        public Transform container;
        public Unit Orc_MountedShamanPrefab;
        public Unit Orc_ArcherPrefab;
        public Unit WK_workerPrefab;
        public Unit WK_CatapultPrefab;
        public Unit WK_spearmanPrefab;
    }
}
