using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InputReader.Interfaces;
using UnityEngine;
using Core.Services.Updater;

namespace Player
{
    public class PlayerBrain : IDisposable
    {
        private readonly PlayerEntity _playerEntity;
        private readonly List<IEntityInputSource> _inputSources;
        public PlayerBrain(PlayerEntity playerEntity, List<IEntityInputSource> inputSources)
        {
            _playerEntity = playerEntity;
            _inputSources = inputSources;
            ProjectUpdater.Instance.FixedUpdateCalled += OnFixedUpdate;


        }

        public void Dispose() => ProjectUpdater.Instance.FixedUpdateCalled -= OnFixedUpdate;

        private void OnFixedUpdate()
        {
            var horizontalDirection = GetHorizontalDirection();
            var verticalDirection = GetVerticalDirection();

            // idle
            _playerEntity.StayFace();

            // diag move resolv
            if (Mathf.Abs(horizontalDirection) > 0.5f && Mathf.Abs(verticalDirection) > 0.5f)
                _playerEntity.DiagonalMoveResolver(true);
            else
                _playerEntity.DiagonalMoveResolver(false);


            // move
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

    }
}
