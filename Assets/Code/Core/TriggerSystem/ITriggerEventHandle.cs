using Code.Entities;

namespace Code.Core.TriggerSystem
{
    public interface ITriggerEventHandle
    {
        public void HandleTrigger(Entity target);
    }
}