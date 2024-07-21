using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SaveLoaderProject
{

    [CreateAssetMenu(fileName ="UnitConfigInstaller",menuName ="Config/New UnitConfigInstaller")]
    public class UnitConfigInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private UnitsScriptableObject unitConfig;

        public override void InstallBindings()
        {
            Container.Bind<UnitsScriptableObject>().AsSingle().NonLazy();
        }
    }
}
