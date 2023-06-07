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
        private float _attackTimeCounter;
        private bool _isAttacking=false;
        public PlayerBrain(PlayerEntity playerEntity, List<IEntityInputSource> inputSources)
        {
            _playerEntity = playerEntity;
            _inputSources = inputSources;
            ProjectUpdater.Instance.FixedUpdateCalled += OnFixedUpdate;


        }

        public void Dispose() => ProjectUpdater.Instance.FixedUpdateCalled -= OnFixedUpdate;

        private void OnFixedUpdate()
        {
            if (!_isAttacking)
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
                if (isAttack)
                {
                    _playerEntity.StayFace();
                    _playerEntity.StartAttack();
                    _isAttacking = true;
                    _attackTimeCounter = _playerEntity.AttackTime;
                }

                foreach (var inputSource in _inputSources)
                    inputSource.ResetOneTimeAction();

            }
            else if (_attackTimeCounter >= 0)
            {
                _attackTimeCounter -= Time.deltaTime;
            }
            else if(_attackTimeCounter < 0)
            {
                _isAttacking = false;
                _playerEntity.StopAttack();
            }
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

        private bool isAttack => _inputSources.Any(inputSource => inputSource.Attack);

    }
}
