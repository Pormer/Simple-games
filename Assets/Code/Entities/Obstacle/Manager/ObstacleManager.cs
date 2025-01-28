using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Entities.Obstacle.Manager
{
    public abstract class ObstacleManager : MonoBehaviour
    {
        [SerializeField] protected Vector2 moveDir;
        [SerializeField] protected float coolTime;
        private float _currentTime;

        [SerializeField] private List<Transform> obsParents; 
        protected List<Obstacle> _obstacleList = new List<Obstacle>();

        protected virtual void Awake()
        {
            foreach (var itemTrm in obsParents)
            {
                itemTrm.GetComponentsInChildren<Obstacle>().ToList().ForEach(obstacle => _obstacleList.Add(obstacle));
            }
        }

        protected virtual void Update()
        {
            _currentTime += Time.deltaTime;
            if(coolTime > _currentTime) return;
            _currentTime = 0;
        }
    }
}