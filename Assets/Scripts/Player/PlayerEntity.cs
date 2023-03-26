using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _diagonalMoveModifier;
        private float _currentMoveSpeed;
        private Rigidbody2D _rigidbody;

        // Start is called before the first frame update
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void DiagonalMoveResolver(bool isDiag)
        {
            if (isDiag)
            {
                _currentMoveSpeed = _moveSpeed * _diagonalMoveModifier;
            }
            else
            {
                _currentMoveSpeed = _moveSpeed;
            }
        }

        public void MoveHorizontally(float horizontalDirection, bool diag = false)
        {
            Vector2 vector2 = _rigidbody.velocity;
            vector2.x = _currentMoveSpeed * horizontalDirection;
            _rigidbody.velocity = vector2;
        }

        internal void MoveVertically(float verticalDirection, bool diag = false)
        {
            Vector2 vector2 = _rigidbody.velocity;
            vector2.y = _currentMoveSpeed * verticalDirection;
            _rigidbody.velocity = vector2;
        }
    }
}