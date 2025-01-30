using System.Collections.Generic;
using System.Linq;
using Code.Entities;
using Code.Entities.Obstacle;
using Code.Score;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Core.TriggerSystem
{
    public class RandomSpawnObstacleTrigger : Trigger
    {
        [SerializeField] private EventChannelSO scoreChannel;
        
        [SerializeField] private Transform obsParent;
        private List<Obstacle> _obstacles;
        
        Dictionary<Obstacle, SpriteRenderer> _obsRenderers = new Dictionary<Obstacle, SpriteRenderer>();
        Dictionary<Obstacle, Collider2D> _obsColliders = new Dictionary<Obstacle, Collider2D>();

        [Header("Data")] 
        [SerializeField] private int wantMinObsCount;
        [SerializeField] private int levelUpdatePoint = 10;
        private int _maxObsCount;
        private int _difficultLevel;
        private int _levelCount = 1;

        private Dictionary<Obstacle, bool> _isObsActive = new Dictionary<Obstacle, bool>();
        private List<int> _obsIndexList = new List<int>();

        private void Awake()
        {
            scoreChannel.AddListener<SetScore>(HandleScoreChange);
            
            _obstacles = obsParent.GetComponentsInChildren<Obstacle>().ToList();
            _obstacles.ForEach(o => _obsRenderers.Add(o, o.transform.Find("Visual").GetComponent<SpriteRenderer>()));
            _obstacles.ForEach(o => _obsColliders.Add(o, o.GetComponent<Collider2D>()));
            _maxObsCount = _obstacles.Count;
            
            int randCountIndex = Random.Range(wantMinObsCount, _maxObsCount);
            _obstacles.ForEach(o => SetActive(o, true));
            RandomOffObstacle(randCountIndex);
        }

        public override void HandleTrigger(Entity target)
        {
            int randCountIndex = Random.Range(wantMinObsCount, _maxObsCount);
            _obstacles.ForEach(o => SetActive(o, true));
            RandomOffObstacle(randCountIndex);
        }

        private void SetActive(Obstacle obs, bool isActive)
        {
            _obsColliders[obs].enabled = isActive;
            _obsRenderers[obs].enabled = isActive;

            if(!_isObsActive.TryAdd(obs, isActive))
                _isObsActive[obs] = isActive;

            if (isActive)
                _obsIndexList.Add(_isObsActive.Keys.ToList().IndexOf(obs));
        }
        
        private void RandomOffObstacle(int count)
        {
            for (int i = 0; i < count; i++)
            {
                int randIndex = Random.Range(0, _obsIndexList.Count);
                int activeIndex = _obsIndexList[randIndex];
                
                SetActive(_obstacles[activeIndex], false);

                _obsIndexList.Remove(activeIndex);
            }
        }
        
        private void HandleScoreChange(GameEvent evt)
        {
            SetScore scoreEvt = evt as SetScore;
            
            if (scoreEvt.score / levelUpdatePoint > _levelCount)
            {
                _maxObsCount--;
                _maxObsCount = Mathf.Clamp(_maxObsCount, wantMinObsCount, _obstacles.Count);

                _levelCount++;
            }
        }
    }
}