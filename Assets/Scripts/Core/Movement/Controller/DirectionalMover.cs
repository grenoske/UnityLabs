using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Movement.Data;
using UnityEngine;

namespace Core.Movement.Controller
{
    public class DirectionalMover
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly DirectionalMovementData _directionalMovementData;

        public bool IsMoving { get; private set; }
        public bool IsChangeAnim { get; set; }
        public Vector2 LastMove { get; private set; }
        public DirectionalMover(Rigidbody2D rigidbody, DirectionalMovementData directionalMovementData)
        {
            _rigidbody = rigidbody;
            _directionalMovementData = directionalMovementData;
        }
        public void StayFace()
        {
            _directionalMovementData.IsMoving = false;
            _rigidbody.velocity = Vector2.zero;
            IsChangeAnim = true;
        }
        public void DiagonalMoveSpeedResolver(bool isDiag)
        {
            _directionalMovementData.CurrentMoveSpeed = _directionalMovementData.MoveSpeed;

            if (isDiag)
            {
                _directionalMovementData.CurrentMoveSpeed *= _directionalMovementData.DiagonalMoveModifier;
            }
        }

        public void MoveHorizontally(float horizontalDirection)
        {
            _directionalMovementData.IsMoving = true;
            Vector2 vector2 = _rigidbody.velocity;
            vector2.x = _directionalMovementData.CurrentMoveSpeed * horizontalDirection;
            _rigidbody.velocity = vector2;
            _directionalMovementData.LastMove = new Vector2(horizontalDirection, 0f);
            IsChangeAnim = true;
        }

        public void MoveVertically(float verticalDirection)
        {
            _directionalMovementData.IsMoving = true;
            Vector2 vector2 = _rigidbody.velocity;
            vector2.y = _directionalMovementData.CurrentMoveSpeed * verticalDirection;
            _rigidbody.velocity = vector2;
            _directionalMovementData.LastMove = new Vector2(0f, verticalDirection);
            IsChangeAnim = true;
        }
    }
}
