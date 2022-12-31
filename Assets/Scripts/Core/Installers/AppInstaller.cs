using Core.Audio;
using Core.States;
using UnityEngine;
using Views.Cell;
using Views.UI.Screens;
using Zenject;

namespace Core.Installers
{
    public class AppInstaller : MonoInstaller
    {
        [SerializeField] private ScreenNavigator screenNavigator;
        [SerializeField] private AudioService audioService ;
    
    
        private CellViewConfig _cellViewConfig;

        [Inject]
        private void Construct(CellViewConfig cellViewConfig)
        {
            this._cellViewConfig = cellViewConfig;
        }

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AudioService>().FromInstance(audioService).AsSingle();
            Container.BindInterfacesAndSelfTo<ScreenNavigator>().FromInstance(screenNavigator).AsSingle();
        
            Container.BindInterfacesAndSelfTo<UpdateService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
            Container.BindFactory<CellView, CellView.Factory>().FromComponentInNewPrefab(_cellViewConfig.CellView);

            Container.Bind<StateFactory>().AsSingle();
            Container.Bind<StateMachine>().AsSingle();

            Container.Bind<AppSession>().AsSingle();
        }
    }
}