using UnityEngine;
using Zenject;

namespace Phases
{
    public class PhaseSetterSceneContext : MonoInstaller<PhaseSetterSceneContext>
    {
        [SerializeField] private GameObject phasesHandler;
        public override void InstallBindings()
        {
            Container.Bind<PhaseHandler>().FromComponentInNewPrefab(phasesHandler).AsSingle().NonLazy();
            Container.Bind<IPhaseSetter>().To<PhaseSetter>().AsSingle().NonLazy();
        }
    }
}