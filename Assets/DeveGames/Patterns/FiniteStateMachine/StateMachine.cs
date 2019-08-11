using System;
using System.Collections.Generic;
using System.Linq;

namespace DeveGames.Patterns.FiniteStateMachine
{
    public class StateMachine
    {
        private readonly Dictionary<Type, BaseState> _availableStates;
        
        public BaseState CurrentState { get; private set; }
        public BaseState PreviousState { get; private set; }
        public event Action<BaseState> OnStateChanged;

        public StateMachine()
        {
            _availableStates = new Dictionary<Type, BaseState>();
        }

        public StateMachine(params BaseState[] states) : this()
        {
            for (int i = 0; i < states.Length; i++)
            {
                AddState(states[i]);
            }
        }

        public void Clear()
        {
            ChangeState(_availableStates.Keys.First());
        }
        
        public void Tick()
        {
            if (CurrentState == null)
            {
                PreviousState = null;
                ChangeState(_availableStates.Keys.First());
            }

            var nextState = CurrentState?.Tick();

            if (nextState != null && nextState != CurrentState?.GetType())
            {
                ChangeState(nextState);   
            }
        }

        public void FixedTick()
        {
            CurrentState?.FixedTick();
        }
        
        public void AddState(BaseState state)
        {
            _availableStates.Add(state.GetType(), state);
        }

        public T GetState<T>() where T : BaseState
        {
            return Get<T>(_availableStates.Values);
        }
        
        public void ChangeState(Type state)
        {
            PreviousState = CurrentState;
            CurrentState = _availableStates[state];
            
            PreviousState?.Exit();
            CurrentState?.Enter();
            
            OnStateChanged?.Invoke(CurrentState);
        }
        
        private T Get<T>(IEnumerable<BaseState> popups) where T : BaseState
        {
            return popups.First(item => item.GetType() == typeof(T)) as T;
        }
    }
}
