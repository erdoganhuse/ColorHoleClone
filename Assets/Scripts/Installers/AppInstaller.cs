using Zenject;

namespace Installers
{
    public class AppInstaller : MonoInstaller<AppInstaller>
    {        
        public override void InstallBindings()
        {            
            SignalBusInstaller.Install(Container);
        }
    }
}