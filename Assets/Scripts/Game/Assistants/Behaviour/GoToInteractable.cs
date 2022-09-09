using BehaviorDesigner.Runtime.Tasks;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace Game.Assistants.Behaviour
{
    public class GoToInteractable : Action
    {
        public SharedInteractable CurrentInteractable;
        public SharedBotCharacterInput BotCharacterInput;

        public override TaskStatus OnUpdate()
        {
            BotCharacterInput.Value.Destination = CurrentInteractable.Value.CharacterInteractable.InteractPoint;
            
            return TaskStatus.Success;
        }
    }
}