using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    public void Enter()
    {
        if(enabled == false)
        {
            enabled = true;

            foreach(var transition in _transitions)
            {
                transition.enabled = true;
            }
        }
    }

    public void Exit()
    {
        if(enabled == true)
        {
            foreach (var transition in _transitions)
                transition.enabled = false;

            enabled = false;
        }
    }

    public State GetNextState()
    {
        foreach(var traansition in _transitions)
        {
            if (traansition.NeedTransit)
                return traansition.TargetState;
        }

        return null;
    }
}
