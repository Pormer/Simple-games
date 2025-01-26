using System;
using Code.Enum;
using DG.Tweening;
using UnityEngine;

namespace Code.Entities.Obstacle
{
    public class FixedDirMoveObstacle : Obstacle
    {
        [Header("Initialize")]
        [SerializeField] private DirectionType dirType;
        [SerializeField] private bool isStartTarget;

        [Header("Member variables")] 
        [SerializeField] private float moveValue;

        [SerializeField] private float moveTime;
        
        private bool _isMove;

        private void Awake()
        {
            if (isStartTarget)
            {
                SetMovement();
            }
        }


        public void MovementXY(float mValue, float time)
        {
            float lenght;
            
            switch (dirType)
            {
                case DirectionType.Horizontal:
                    lenght = transform.position.x + mValue;
                    transform.DOMoveX(lenght, time);
                    break;
                case DirectionType.Vertical:
                    lenght = transform.position.y + mValue;
                    transform.DOMoveY(lenght, time);
                    break;
            }

            _isMove = !_isMove;
        }

        public override void SetMovement()
        {
            if (!_isMove)
            {
                MovementXY(moveValue, moveTime);
            }
            else
            {
                MovementXY(-moveValue, moveTime);
            }
        }
    }
}