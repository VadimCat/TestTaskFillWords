using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "AppConfigsInstaller", fileName = "AppConfigsInstaller")]
public class AppConfigsInstaller : ScriptableObjectInstaller
{
    [SerializeField] private CellViewConfig cellViewConfig;
    
    public override void InstallBindings()
    {
        Container.BindInstance(cellViewConfig);
    }
}