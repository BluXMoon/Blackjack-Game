using UnityEngine;

namespace Phases
{
    public class PhaseHandler : MonoBehaviour
    {
        private MetaPhaseHandler[] _metaPhaseHandlers;

        private void Awake() => _metaPhaseHandlers = GetComponentsInChildren<MetaPhaseHandler>();

        public MetaPhaseHandler FindMyMetaPhase(Phase phase)
        {
            foreach (var phaseHandler in _metaPhaseHandlers)
            {
                if (phaseHandler.AmIPartOfThisMetaPhase(phase.MetaPhase))
                {
                    return phaseHandler;
                }
            }

            Debug.Log("Meta phase: " + phase.MetaPhase + " could not be found!");
            return null;
        }
    }
}
