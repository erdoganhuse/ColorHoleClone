using System;

namespace DeveGames.Patterns.FiniteStateMachine
{
    public abstract class BaseState
    {
        protected BaseState(){ }

        public abstract void Enter();

        public abstract Type Tick();

        public abstract void FixedTick();
        
        public abstract void Exit();
    }
}