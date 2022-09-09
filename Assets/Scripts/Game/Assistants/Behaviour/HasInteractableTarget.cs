using System;
using System.Collections.Generic;
using System.Linq;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Game.Assistants.Behaviour
{
    public class HasInteractableTarget : Conditional
    {
        public SharedInteractable CurrentInteractable;

        public override TaskStatus OnUpdate() => 
            BehaviourHelper.ToTaskStatus(CurrentInteractable.Value?.CharacterInteractable != null);
    }
}