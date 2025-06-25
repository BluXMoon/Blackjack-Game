using System;
using UnityEngine;

namespace Phases
{
    public class MetaPhaseHandler : MonoBehaviour
    {
        [SerializeField] private MetaPhase metaPhase;

        public Action OnPhaseChanged;
        public Phase CurrentPhase;

        public bool AmIPartOfThisMetaPhase(MetaPhase phase) => phase == metaPhase;
    }
}
