namespace DeveGames.Patterns.FiniteStateMachine.Examples
{
    public static class DroneStateFactory
    {
        public static StateMachine Create(Drone drone)
        {
            StateMachine stateMachine = new StateMachine();
            
            stateMachine.AddState(new WanderState(drone));
            stateMachine.AddState(new AttackState(drone));
            stateMachine.AddState(new ChaseState(drone));

            return stateMachine;
        }
    }
}