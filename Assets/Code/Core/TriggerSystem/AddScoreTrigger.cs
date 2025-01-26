using Code.Entities;
using Code.Score;
using UnityEngine;

namespace Code.Core.TriggerSystem
{
    public class AddScoreTrigger : Trigger
    {
        [SerializeField] private EventChannelSO scoreEventChannel;
        
        public override void HandleTrigger(Entity target)
        {
            SetScore scoreEvt = ScoreEvent.SetScore;
            scoreEvt.score++;
            
            scoreEventChannel.RaiseEvent(scoreEvt);
        }
    }
}