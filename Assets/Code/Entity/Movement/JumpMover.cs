using UnityEngine;

namespace Code.Entity.Movement
{
    public class JumpMover : MoveCompo
    {
        [SerializeField] private float jumpForce;

        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);

            _rigidCompo.gravityScale = 1f;
        }

        protected override void FixedUpdate()
        {
            SetMovement(Vector2.right);
            SetVelocity(_moveDir, true);
        }

        public override void HandlePressKey()
        {
            SetAddForce(Vector2.up * jumpForce);
        }
    }
}