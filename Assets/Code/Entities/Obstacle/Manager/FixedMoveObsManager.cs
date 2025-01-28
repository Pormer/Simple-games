using System;
using System.Collections.Generic;
using Code.Core.TriggerSystem;
using UnityEngine;

namespace Code.Entities.Obstacle.Manager
{
    public class FixedMoveObsManager : ObstacleManager, ITriggerEventHandle
    {
        private Dictionary<FixedMoveObstacle, Vector3> _obsPosDictionary = new Dictionary<FixedMoveObstacle, Vector3>();
        
        protected override void Awake()
        {
            base.Awake();

            foreach (var item in _obstacleList)
            {
                FixedMoveObstacle fixedMoveObstacle = (FixedMoveObstacle)item;
                
                _obsPosDictionary.Add(fixedMoveObstacle, item.transform.localPosition);
                
                if (fixedMoveObstacle.IsMoving)
                {
                    Vector2 movePos = (Vector2)_obsPosDictionary[fixedMoveObstacle] + moveDir;
                    fixedMoveObstacle.SetMovement(movePos, coolTime);
                    fixedMoveObstacle.IsMoving = true;
                }
                else
                { 
                    Vector2 movePos = _obsPosDictionary[fixedMoveObstacle];
                    fixedMoveObstacle.SetMovement(movePos, coolTime);
                    fixedMoveObstacle.IsMoving = false;
                }
            }
        }

        public void HandleTrigger(Entity target)
        {
            _obstacleList.ForEach(obs =>
            {
                FixedMoveObstacle fixedMoveObstacle = (FixedMoveObstacle)obs;
                
                if (fixedMoveObstacle.IsMoving)
                {
                    Vector2 obsPos = _obsPosDictionary[fixedMoveObstacle];

                    fixedMoveObstacle.SetMovement(obsPos, coolTime);
                    fixedMoveObstacle.IsMoving = false;
                }
                else
                {
                    Vector2 obsPos = (Vector2)_obsPosDictionary[fixedMoveObstacle] + moveDir;
                    fixedMoveObstacle.SetMovement(obsPos, coolTime);
                    fixedMoveObstacle.IsMoving = true;
                }
            });
        }
    }
}