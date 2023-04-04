using System;
using UnityEngine;
using Core.Movement.Controller;
using Core.Movement.Data;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] DirectionalMovementData _directionalMovementData;
        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private DirectionalMover _directionalMover;

        public void MoveHorizontally(float horizontalDirection) => _directionalMover.MoveHorizontally(horizontalDirection);
        public void MoveVertically(float verticalDirection) => _directionalMover.MoveVertically(verticalDirection);
        public void StayFace() => _directionalMover.StayFace();
        public void DiagonalMoveResolver(bool isDiag) => _directionalMover.DiagonalMoveSpeedResolver(isDiag);

        // Start is called before the first frame update
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _directionalMover = new DirectionalMover(_rigidbody, _directionalMovementData);
        }

        private void Update()
        {
           if(_directionalMover.IsChangeAnim)
            {
                UpdateAnim();
                _directionalMover.IsChangeAnim = false;
            }
        }

        private void UpdateAnim()
        {
            _animator.SetFloat("LastMoveX", _directionalMovementData.LastMove.x);
            _animator.SetFloat("LastMoveY", _directionalMovementData.LastMove.y);
            _animator.SetBool("isPlayerMoving", _directionalMovementData.IsMoving);
        }
    }
}