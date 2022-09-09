using System;
using BehaviorDesigner.Runtime;

namespace Game.Assistants.Behaviour
{
    [Serializable]
    public class SharedStackPresenter : SharedVariable<StackPresenter>
    {
        public static implicit operator SharedStackPresenter(StackPresenter value) =>
            new SharedStackPresenter {Value = value};
    }
}