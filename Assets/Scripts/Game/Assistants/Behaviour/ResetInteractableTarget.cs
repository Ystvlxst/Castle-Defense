using System;
using System.Collections.Generic;
using System.Linq;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace Game.Assistants.Behaviour
{
    public class ResetInteractableTarget : Action
    {
        public SharedInteractable CurrentInteractable;

        public override TaskStatus OnUpdate()
        {
            var canReset = CurrentInteractable.Value != null;
            
            if(canReset)
                CurrentInteractable.Value.CharacterInteractable = null;

            return BehaviourHelper.ToTaskStatus(canReset);
        }
    }
}