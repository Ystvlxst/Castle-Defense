using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using Action = BehaviorDesigner.Runtime.Tasks.Action;
using Object = UnityEngine.Object;

namespace Game.Assistants.Behaviour
{
    public class ClearStack : Action
    {
        public SharedStackPresenter SelfStackPresenter;
        
        public override TaskStatus OnUpdate()
        {
            foreach (Stackable stackable in SelfStackPresenter.Value.RemoveAll()) 
                Object.Destroy(stackable.gameObject);

            return TaskStatus.Success;
        }
    }
}