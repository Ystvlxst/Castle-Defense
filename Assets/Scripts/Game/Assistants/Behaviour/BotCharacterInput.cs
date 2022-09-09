using System;
using UnityEngine;

namespace Game.Assistants.Behaviour
{
    public class BotCharacterInput : MonoBehaviour, IInputSource
    {
        public Vector3 Destination { get; set; }

        private void Start() => Destination = transform.position;
    }
}