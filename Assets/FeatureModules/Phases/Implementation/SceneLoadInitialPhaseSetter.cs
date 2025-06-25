using System;
using UnityEngine;
using Zenject;

namespace Phases.Scripts
{
    public class SceneLoadInitialPhaseSetter : MonoBehaviour
    {
        [SerializeField] private Phase phaseToSetOnLoadScene;
        
        [Inject] private IPhaseSetter _phaseSetter;

        private void Awake() => _phaseSetter.SetPhase(phaseToSetOnLoadScene);
    }
}
