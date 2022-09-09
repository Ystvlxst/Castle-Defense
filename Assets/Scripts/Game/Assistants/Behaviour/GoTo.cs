using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace Game.Assistants.Behaviour
{
    public abstract class GoTo : Action
    {
        public SharedBotCharacterInput BotCharacterInput;

        public override TaskStatus OnUpdate()
        {
            BotCharacterInput.Value.Destination = GetPosition();

            return TaskStatus.Success;
        }

        protected abstract Vector3 GetPosition();
    }
}