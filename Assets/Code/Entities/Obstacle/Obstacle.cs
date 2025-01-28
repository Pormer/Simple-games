using Code.Core.TriggerSystem;
using UnityEngine;

namespace Code.Entities.Obstacle
{
    public abstract class Obstacle : MonoBehaviour, ITriggerEventHandle
    {
        public abstract void SetMovement(Vector2 moveDir, float time);
        public void HandleTrigger(Entity target)
        {
            //닿으면 게임 오버
        }
    }
}