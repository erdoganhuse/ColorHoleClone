using Core.Controller;
using Core.Controller.User;
using Core.Data.User;
using Core.Signal;
using DeveGames.SceneManager;
using UnityEngine;
using Utilities.Enums;
using Zenject;

namespace Installers.Main
{
    public class MainInstaller : MonoInstaller<MainInstaller>
    {
        [SerializeField] private UserData _defaultUserData;
        
        public override void InstallBindings()
        {
            InstallData();
            InstallSettings();
            InstallSignals();
            
            Container.Bind<ZenjectSceneManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle().NonLazy();
            Container.Bind<UserDataController>().AsSingle().NonLazy();
        }

        private void InstallData()
        {            
            Container.BindInstance(_defaultUserData).WithId(BindingIds.DefaultUserData).AsSingle().NonLazy();
        }
        
        private void InstallSettings()
        {
            
        }
        
        private void InstallSignals()
        {
            Container.DeclareSignal<GameStartedSignal>();
        }        
    }
}