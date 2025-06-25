using Zenject;

namespace Blackjack
{
    public class DealerRuleFactorySceneContext : MonoInstaller<DealerRuleFactorySceneContext>
    {
        public override void InstallBindings()
        {
            Container.Bind<IDealerRuleFactory>().To<DealerRuleFactory>().AsSingle().NonLazy();
        }
    }
}