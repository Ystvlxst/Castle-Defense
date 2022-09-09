using BehaviorDesigner.Runtime;

namespace Game.Assistants.Behaviour
{
    public class SharedAssistant : SharedVariable<Assistant>
    {
        public static implicit operator SharedAssistant(Assistant value) =>
            new SharedAssistant {Value = value};
    }
}