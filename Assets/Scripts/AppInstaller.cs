using Client;
using Core;
using Ji2Core.Core.Audio;
using UI.Screens;
using UnityEngine;
using View;
using Zenject;

public class AppInstaller : MonoInstaller
{
    [SerializeField] private ScreenNavigator screenNavigator;
    [SerializeField] private AudioService _audioService ;
    
    
    private CellViewConfig _cellViewConfig;

    [Inject]
    private void Construct(CellViewConfig cellViewConfig)
    {
        this._cellViewConfig = cellViewConfig;
    }

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<AudioService>().FromInstance(_audioService).AsSingle();
        Container.BindInterfacesAndSelfTo<ScreenNavigator>().FromInstance(screenNavigator).AsSingle();
        
        Container.BindInterfacesAndSelfTo<UpdateService>().AsSingle();
        Container.Bind<SceneLoader>().AsSingle();
        Container.BindFactory<CellView, CellView.Factory>().FromComponentInNewPrefab(_cellViewConfig.CellView);

        Container.Bind<StateFactory>().AsSingle();
        Container.Bind<StateMachine>().AsSingle();

        Container.Bind<AppSession>().AsSingle();
    }
}