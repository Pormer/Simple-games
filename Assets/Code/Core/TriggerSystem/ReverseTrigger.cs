using Code.Entities;
using Code.Entities.Movement;
using Code.Entities.Player;

namespace Code.Core.TriggerSystem
{
    public class ReverseTrigger : Trigger
    {
        public override void HandleTrigger(Entity target)
        {
            MoveCompo mover = target.GetCompo<MoveCompo>(true);
            
            mover.SetReverse();
        }
    }
}