using System.Collections.Generic;
using UnityEngine;
using InputReader;
using InputReader.Interfaces;
using System;
using Core.Services.Updater;

namespace Player
{
    public class PlayerSystem : IDisposable
    {
        private readonly PlayerEntity _playerEntity;
        private readonly PlayerBrain _playerBrain;

        private List<IDisposable> _disposables;

        public PlayerSystem(PlayerEntity playerEntity, List<IEntityInputSource> inputSources)
        {
            _disposables = new List<IDisposable>();

            _playerEntity = playerEntity;
            _playerBrain = new PlayerBrain(_playerEntity, inputSources);
            _disposables.Add(_playerBrain);
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }
    }
}
