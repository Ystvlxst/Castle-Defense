using System;
using BehaviorDesigner.Runtime;

namespace Game.Assistants.Behaviour
{
    [Serializable]
    public class SharedBotCharacterInput : SharedVariable<BotCharacterInput>
    {
        public static implicit operator SharedBotCharacterInput(BotCharacterInput value) =>
            new SharedBotCharacterInput {Value = value};
    }
}