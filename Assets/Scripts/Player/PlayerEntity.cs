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
        private Vector2 _CurrentMove;
        private float _currentMoveSpeed;

        internal bool isMoving { get; set; }
        
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
            Vector2 vector2 = _rigidbody.velocity;
            vector2.x = _currentMoveSpeed * horizontalDirection;
            _rigidbody.velocity = vector2;



            if(vector2.x != 0f)
            {
                isMoving = true;
                _LastMove = new Vector2(horizontalDirection, 0f);
            }
            _CurrentMove.x = horizontalDirection;
            SetAnim();
        }

        internal void MoveVertically(float verticalDirection, bool diag = false)
        {
            Vector2 vector2 = _rigidbody.velocity;
            vector2.y = _currentMoveSpeed * verticalDirection;
            _rigidbody.velocity = vector2;



            if (vector2.y != 0f)
            {
                isMoving = true;
                _LastMove = new Vector2(0f, verticalDirection);
            }
            _CurrentMove.y = verticalDirection;
            SetAnim();
        }

        internal void SetAnim()
        {
            _animator.SetFloat("MoveX", _CurrentMove.x);
            _animator.SetFloat("MoveY", _CurrentMove.y);
            _animator.SetFloat("LastMoveX", _LastMove.x);
            _animator.SetFloat("LastMoveY", _LastMove.y);
            _animator.SetBool("isPlayerMoving", isMoving);
        }
    }
}