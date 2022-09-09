using BehaviorDesigner.Runtime;
using UnityEngine;

namespace Game.Assistants.Behaviour
{
    public class GoToWaitPoint : GoTo
    {
        public SharedVector3 Waypoint;

        protected override Vector3 GetPosition() => Waypoint.Value;
    }
}