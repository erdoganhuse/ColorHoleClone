using System;

namespace DeveGames.Patterns.FiniteStateMachine.Examples
{
    public class ChaseState : BaseState
    {
        public ChaseState(Drone drone) : base(){ }

        public override void Enter() { }
        public override Type Tick() { return null; }
        public override void FixedTick() { }
        public override void Exit() { }
    }
}
