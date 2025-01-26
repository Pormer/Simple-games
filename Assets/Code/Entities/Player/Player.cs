using System;
using System.Linq;
using Code.Core.TriggerSystem;
using UnityEngine;
using Code.Entities;
using Code.Entities.Movement;

namespace Code.Entities.Player
{
    public class Player : Entity
    {
        [field: SerializeField] public InputReaderSO InputReader { get; private set; }

        protected override void InitializeComponents()
        {
            base.InitializeComponents();

            InputReader.OnKeyPress += GetCompo<MoveCompo>(true).HandlePressKey;
            InputReader.OnKeyRelease += GetCompo<MoveCompo>(true).HandleReleaseKey;
        }

        private void OnDestroy()
        {
            InputReader.OnKeyPress -= GetCompo<MoveCompo>(true).HandlePressKey;
            InputReader.OnKeyRelease -= GetCompo<MoveCompo>(true).HandleReleaseKey;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            other.gameObject.GetComponentsInChildren<ITriggerEventHandle>()
                 .ToList()
                 .ForEach(evt => evt.HandleTrigger(this));
        }
    }
}