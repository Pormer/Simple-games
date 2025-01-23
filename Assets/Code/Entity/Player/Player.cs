using System;
using UnityEngine;
using Code.Entity;
using Code.Entity.Movement;

namespace Code.Entity.Player
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
    }
}