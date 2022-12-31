using UnityEngine;
using Zenject;

public class EntryPointInstaller : MonoInstaller
{
    [SerializeField] private EntryPoint entryPoint;
    
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<EntryPoint>().FromInstance(entryPoint);
    }
}