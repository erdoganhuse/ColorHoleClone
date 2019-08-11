using System;
using UnityEngine;

namespace DeveGames.Patterns.FiniteStateMachine.Examples
{
    public class AttackState : BaseState
    {
        private Drone _drone;

        public AttackState(Drone drone) : base()
        {
            _drone = drone;
        }

        public override void Enter()
        {
            Debug.Log("Drone Entered to Attack State!!!");
        }

        public override Type Tick()
        {
            if (_drone.Target == null)
            {
                return typeof(WanderState);
            }

            return null;
        }

        public override void FixedTick()
        {
            throw new NotImplementedException();
        }

        public override void Exit()
        {
            Debug.Log("Drone Exited from Attack State!!!");
        }
    }
}
