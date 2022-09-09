using BehaviorDesigner.Runtime.Tasks;

namespace Game.Assistants.Behaviour
{
    public class BehaviourHelper
    {
        public static TaskStatus ToTaskStatus(bool suc) => suc ? TaskStatus.Success : TaskStatus.Failure;
    }
}