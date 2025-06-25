using Zenject;

namespace Blackjack
{
    public class BlackjackGameManagerSceneContext : MonoInstaller<BlackjackGameManagerSceneContext>
    {
        public override void InstallBindings()
        {
            Container.Bind<IBlackjackGameManager>().To<BlackjackGameManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}