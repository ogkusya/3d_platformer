using System.Collections.Generic;

public abstract class State
{
    public List<ITransition> Transitions { get; private set; } = new List<ITransition>(); //All added transitions

    public virtual void Tick() //Called every frame
    {
    }

    public virtual void OnStateEntered() //Enter state
    {
    }

    public virtual void OnStateExited() //Exit state
    {
    }

    public virtual void OnUpdate()
    {
    }

    public virtual void OnFixedUpdate()
    {
    }

    public void InitializeTransitions() //Initialize all added transitions
    {
        foreach (var stateTransition in Transitions)
        {
            stateTransition.OnStateEntered();
        }
    }

    public void DeInitializeTransitions() //DeInitialize all added transitions
    {
        foreach (var stateTransition in Transitions)
        {
            stateTransition.OnStateEntered();
        }
    }

    public void AddTransition(ITransition transition) //Add a new transition to another state
    {
        Transitions.Add(transition);
    }

    public void RemoveTransition(ITransition transition) //Remove added transition
    {
        if (Transitions.Contains(transition))
        {
            Transitions.Remove(transition);
        }
    }
}