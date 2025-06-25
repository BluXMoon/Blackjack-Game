using UnityEngine;

namespace Phases
{
    [CreateAssetMenu(fileName = "Phase", menuName = "ScriptableObjects/New Phase")]
    public class Phase : ScriptableObject
    {
        [SerializeField] private MetaPhase metaPhase;

        public MetaPhase MetaPhase => metaPhase;
    }
}
