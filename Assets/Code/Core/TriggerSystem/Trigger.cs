using System;
using Code.Entities;
using UnityEngine;

namespace Code.Core.TriggerSystem
{
    public abstract class Trigger : MonoBehaviour, ITriggerEventHandle
    {
        public abstract void HandleTrigger(Entity target);
    }
}