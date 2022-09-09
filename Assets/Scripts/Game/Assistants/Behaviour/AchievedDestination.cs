using System;
using System.Collections.Generic;
using System.Linq;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Game.Assistants.Behaviour
{
    public class AchievedDestination : Conditional
    {
        private float _treshold = 0.3f;
        
        public SharedBotCharacterInput BotCharacterInput;

        public override TaskStatus OnUpdate()
        {
            return BehaviourHelper.ToTaskStatus(Vector3.Distance(BotCharacterInput.Value.Destination, transform.position) < _treshold);
        }
    }
}