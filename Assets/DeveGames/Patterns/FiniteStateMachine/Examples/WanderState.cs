using System;

namespace DeveGames.Patterns.FiniteStateMachine.Examples
{
    public class WanderState : BaseState
    {
        private Drone _drone;
        
        public WanderState(Drone drone) : base()
        {
            _drone = drone;
        }

        public override void Enter() { }
        public override Type Tick() { return null; }
        public override void FixedTick() { }
        public override void Exit() { }
    }
}