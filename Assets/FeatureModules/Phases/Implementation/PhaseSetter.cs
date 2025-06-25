using Zenject;

namespace Phases
{
    public class PhaseSetter : IPhaseSetter
    {
        [Inject] private PhaseHandler _phaseHandler;
        
        private Phase _phaseToSet;

        public void SetPhase(Phase phase)
        {
            _phaseToSet = phase;
            SetPhase();
        }
        
        private void SetPhase()
        {
            var metaPhaseHandler = _phaseHandler.FindMyMetaPhase(_phaseToSet);
            metaPhaseHandler.CurrentPhase = _phaseToSet;
            metaPhaseHandler.OnPhaseChanged?.Invoke();
        }
    }
}