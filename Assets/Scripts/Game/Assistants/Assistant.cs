using System;
using BehaviorDesigner.Runtime;
using Game.Assistants.Behaviour;
using UnityEngine;

public class Assistant : MonoBehaviour
{
    [SerializeField] private BehaviorTree _behaviorTree;
    
    public Transform[] WaitPoints { get; private set; }

    public void Init(Transform[] waitPoints, InteractableObjectsContainer interactablesContainer)
    {
        WaitPoints = waitPoints;
        _behaviorTree.SetVariableValue("_interactablesContainer", interactablesContainer);
    }
}