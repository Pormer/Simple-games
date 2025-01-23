using System;
using UnityEngine;
using Input = UnityEngine.Windows.Input;

namespace Code.Entity.Movement
{
    public abstract class MoveCompo : MonoBehaviour, IEntityComponent
    {
        protected Rigidbody2D _rigidCompo;

        protected Vector2 _moveDir;
        [SerializeField] protected float moveSpeed;

        private int _reverseDir = 1;

        public virtual void Initialize(Entity entity)
        {
            _rigidCompo = entity.GetComponent<Rigidbody2D>();
        }

        protected virtual void FixedUpdate()
        {
        }

        public virtual void SetMovement(Vector2 dir) => _moveDir = dir * _reverseDir;

        public virtual Vector2 SetVelocity(Vector2 dir, bool isGravity)
        {
            if (isGravity)
            {
                _rigidCompo.linearVelocity = new Vector2(dir.x * moveSpeed, _rigidCompo.linearVelocityY);
                return _rigidCompo.linearVelocity;
            }

            _rigidCompo.linearVelocity = _moveDir * moveSpeed;
            return _rigidCompo.linearVelocity;
        }

        public virtual void SetAddForce(Vector2 force)
        {
            _rigidCompo.linearVelocity = Vector2.zero;
            _rigidCompo.AddForce(force, ForceMode2D.Impulse);
        }

        public void SetReverse() => _reverseDir *= -1;

        public virtual void HandlePressKey()
        {
        }

        public virtual void HandleReleaseKey()
        {
        }

#if UNITY_EDITOR
        [ContextMenu("Tester")]
        private void TestMethod()
        {
            SetReverse();
        }
#endif
    }
}