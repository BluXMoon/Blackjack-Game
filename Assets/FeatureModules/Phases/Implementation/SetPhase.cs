using UnityEngine;
using Zenject;

namespace Phases
{
    public class SetPhase : MonoBehaviour
    {
        [SerializeField] private Phase phaseToSet;
        
        [Inject] private IPhaseSetter _phaseSetter;
        
        public void Set() => _phaseSetter.SetPhase(phaseToSet);
    }
}
