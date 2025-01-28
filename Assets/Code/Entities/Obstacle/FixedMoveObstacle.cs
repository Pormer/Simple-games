using System;
using Code.Enum;
using DG.Tweening;
using UnityEngine;

namespace Code.Entities.Obstacle
{
    public class FixedMoveObstacle : Obstacle
    {
        [Header("Initialize")]
        [SerializeField] private DirectionType dirType;
        [field: SerializeField] public bool IsMoving { get; set; }

        public override void SetMovement(Vector2 mValue, float time)
        {
            transform.DOLocalMove(mValue, time);
        }
    }
}