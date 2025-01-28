using System;
using System.Collections.Generic;
using System.Linq;
using Code.Entities;
using Code.Entities.Obstacle;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Core.TriggerSystem
{
    public class RandomSpawnObstacleTrigger : Trigger
    {
        [SerializeField] private Transform obsParent;
        private List<Obstacle> _obstacles;
        
        Dictionary<Obstacle, SpriteRenderer> _obsRenderers = new Dictionary<Obstacle, SpriteRenderer>();
        Dictionary<Obstacle, Collider2D> _obsColliders = new Dictionary<Obstacle, Collider2D>();

        private void Awake()
        {
            _obstacles = obsParent.GetComponentsInChildren<Obstacle>().ToList();
            _obstacles.ForEach(o => _obsRenderers.Add(o, o.transform.Find("Visual").GetComponent<SpriteRenderer>()));
            _obstacles.ForEach(o => _obsColliders.Add(o, o.GetComponent<Collider2D>()));
            
            int randIndex = Random.Range(0, _obstacles.Count);
            
            _obstacles.ForEach(o => SetActive(o, true));
            SetActive(_obstacles[randIndex], false);
        }

        public override void HandleTrigger(Entity target)
        {
            int randIndex = Random.Range(0, _obstacles.Count);
                
            _obstacles.ForEach(o => SetActive(o, true));
            SetActive(_obstacles[randIndex], false);
        }

        private void SetActive(Obstacle obs, bool isActive)
        {
            _obsColliders[obs].enabled = isActive;
            _obsRenderers[obs].enabled = isActive;
        }
    }
}