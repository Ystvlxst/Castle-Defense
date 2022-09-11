using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace Game.Assistants.Behaviour
{
    public class GoTo : Action
    {
        public SharedBotCharacterInput BotCharacterInput;
        public SharedVector3 Destination;

        public override TaskStatus OnUpdate()
        {
            BotCharacterInput.Value.Destination = Destination.Value;

            return TaskStatus.Running;
        }
    }
}