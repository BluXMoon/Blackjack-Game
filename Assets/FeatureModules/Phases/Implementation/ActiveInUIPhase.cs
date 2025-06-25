using System.Linq;
using UnityEngine;
using Zenject;

namespace Phases
{
    public class ActiveInUIPhase : MonoBehaviour
    {
        [SerializeField] private Phase[] activeInPhase;

        [Inject] private PhaseHandler _phaseHandler;

        private Phase _firstPhase;
        private MetaPhaseHandler _metaPhaseHandler;
    
        private void Start()
        {
            if (activeInPhase.Length == 0)
            {
                Debug.LogWarning("There are no phases assigned for this object: " + gameObject.name);
                return;
            }
            
            _firstPhase = activeInPhase.First();
            ValidatePhases();

            _metaPhaseHandler = _phaseHandler.FindMyMetaPhase(_firstPhase);
            _metaPhaseHandler.OnPhaseChanged += PhaseChanged;
        
            PhaseChanged();
        }

        private void ValidatePhases()
        {
            foreach (var phase in activeInPhase)
            {
                if(_firstPhase.MetaPhase != phase.MetaPhase) Debug.LogWarning("There are some different meta phase phases " +
                                                                              "assigned to " + gameObject.name + ". One Game Object can only" +
                                                                              "accept one type of metaphases!");
            }
        }

        private void OnDestroy()
        {
            if (_phaseHandler is null) return;
            _metaPhaseHandler.OnPhaseChanged -= PhaseChanged;
        }

        private void PhaseChanged()
        {
            var newPhase = _metaPhaseHandler.CurrentPhase;
        
            if (activeInPhase.Contains(newPhase))
            {
                gameObject.SetActive(true);
                return;
            }
        
            gameObject.SetActive(false);
        }
    }
}
