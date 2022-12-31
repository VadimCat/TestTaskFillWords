using UnityEngine;
using Zenject;

namespace Core.Installers
{
    public class EntryPointInstaller : MonoInstaller
    {
        [SerializeField] private EntryPoint entryPoint;
    
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EntryPoint>().FromInstance(entryPoint);
        }
    }
}