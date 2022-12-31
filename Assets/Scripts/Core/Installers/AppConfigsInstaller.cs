using UnityEngine;
using Views.Cell;
using Zenject;

namespace Core.Installers
{
    [CreateAssetMenu(menuName = "AppConfigsInstaller", fileName = "AppConfigsInstaller")]
    public class AppConfigsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private CellViewConfig cellViewConfig;
    
        public override void InstallBindings()
        {
            Container.BindInstance(cellViewConfig);
        }
    }
}