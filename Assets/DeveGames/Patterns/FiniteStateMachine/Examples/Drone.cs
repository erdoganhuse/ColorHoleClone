using UnityEngine;

namespace DeveGames.Patterns.FiniteStateMachine.Examples
{
    public class Drone : MonoBehaviour
    {
        public Transform Target { get; private set; }
        public StateMachine StateMachine { get; private set; }

        private void Awake()
        {
            StateMachine = DroneStateFactory.Create(this);
            StateMachine.OnStateChanged += StateMachine_OnStateChanged;
        }

        private void Update()
        {
            StateMachine.Tick();
        }
        
        public void SetTarget(Transform target)
        {
            Target = target;
        }
        
        private void StateMachine_OnStateChanged(BaseState state)
        {
            
        }
    }
}