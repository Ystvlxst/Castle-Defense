using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Game.Assistants.Behaviour
{
    public class SelectRandomWaypoint : Action
    {
        public SharedVector3 Waypoint;
        public SharedAssistant Assistant;

        public override TaskStatus OnUpdate()
        {
            Waypoint.Value = Assistant.Value.GetRandomWaypoint();

            return TaskStatus.Success;
        }
    }
}