using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Player.Interfaces;
using UnityEngine;

namespace Player
{
    internal class PlayerBrain
    {
        private readonly PlayerEntity _playerEntity;
        private readonly List<IEntityInputSource> _inputSources;
        public PlayerBrain(PlayerEntity playerEntity, List<IEntityInputSource> inputSources)
        {
            _playerEntity = playerEntity;
            _inputSources = inputSources;
        }

        public void OnFixedUpdate()
        {
            var horizontalDirection = GetHorizontalDirection();
            var verticalDirection = GetVerticalDirection();

            if (horizontalDirection > verticalDirection)
                _playerEntity.FaceHorizontally(horizontalDirection);
            else
                _playerEntity.FaceVertically(verticalDirection);


            // diag move
            if (Mathf.Abs(horizontalDirection) > 0.5f && Mathf.Abs(verticalDirection) > 0.5f)
                _playerEntity.DiagonalMoveResolver(true);
            else
                _playerEntity.DiagonalMoveResolver(false);



            if (horizontalDirection >= 0.5f || horizontalDirection <= -0.5f)
                _playerEntity.MoveHorizontally(horizontalDirection);
            if (verticalDirection >= 0.5f || verticalDirection <= -0.5f)
                _playerEntity.MoveVertically(verticalDirection);







        }

        private float GetHorizontalDirection()
        {
            foreach (var inputSource in _inputSources)
            {
                if (inputSource.HorizontalDirection == 0)
                    continue;
                return inputSource.HorizontalDirection;
            }

            return 0;
        }

        private float GetVerticalDirection()
        {
            foreach (var inputSource in _inputSources)
            {
                if (inputSource.VerticalDirection == 0)
                    continue;
                return inputSource.VerticalDirection;
            }

            return 0;
        }

        // private bool isAttack => _inputSources.Any(source => source.Attack);
    }
}
