using Zenject;

namespace Blackjack
{
    public class DeckManagerSceneContext : MonoInstaller<DeckManagerSceneContext>
    {
        public override void InstallBindings()
        {
            Container.Bind<IDeckManager>().To<DeckManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}