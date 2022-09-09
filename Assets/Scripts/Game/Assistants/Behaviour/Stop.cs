using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace Game.Assistants.Behaviour
{
    public class Stop : Action
    {
        public SharedBotCharacterInput BotCharacterInput;

        public override TaskStatus OnUpdate()
        {
            BotCharacterInput.Value.Destination = transform.position;
            
            return TaskStatus.Success;
        }
    }
}