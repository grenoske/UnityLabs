using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _diagonalMoveModifier;
        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private Vector2 _LastMove;
        private Vector2 _FaceDirection;
        private float _currentMoveSpeed;
        private bool isMoving;
        
        // Start is called before the first frame update
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
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
            isMoving = true;
            Vector2 vector2 = _rigidbody.velocity;
            vector2.x = _currentMoveSpeed * horizontalDirection;
            _FaceDirection.x = horizontalDirection;
            _rigidbody.velocity = vector2;
            _LastMove = new Vector2(horizontalDirection, 0f);
            SetAnim();
            _FaceDirection = Vector2.zero;
        }

        public void FaceHorizontally(float horizontalDirection)
        {
            isMoving = false;
            _rigidbody.velocity = Vector2.zero;
            //_FaceDirection = new Vector2(horizontalDirection, 0f);
            SetAnim();
        }

        internal void MoveVertically(float verticalDirection, bool diag = false)
        {
            isMoving = true;
            Vector2 vector2 = _rigidbody.velocity;
            vector2.y = _currentMoveSpeed * verticalDirection;
            _rigidbody.velocity = vector2;
            _LastMove = new Vector2(0f, verticalDirection);
            _FaceDirection = _LastMove;
            SetAnim();
            _FaceDirection = Vector2.zero;
        }

        public void FaceVertically(float verticalDirection)
        {
            isMoving = false;
            _rigidbody.velocity = Vector2.zero;
            //_FaceDirection = new Vector2(0f, verticalDirection);
            SetAnim();
        }

        internal void SetAnim()
        {
            _animator.SetFloat("MoveX", _FaceDirection.x);
            _animator.SetFloat("MoveY", _FaceDirection.y);
            _animator.SetFloat("LastMoveX", _LastMove.x);
            _animator.SetFloat("LastMoveY", _LastMove.y);
            _animator.SetBool("isPlayerMoving", isMoving);
        }
    }
}