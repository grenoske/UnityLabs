using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Player;
using Player.Interfaces;

namespace Core
{
    internal class GameLevelInitializer : MonoBehaviour
    {
        [SerializeField] private PlayerEntity _playerEntity;
        [SerializeField] private GameUIInputView _gameUIInputView;

        private ExternalDevicesInputReader _externalDeviceInput;
        private PlayerBrain _playerBrain;
        private bool _onPause;

        private void Awake()
        {
            _externalDeviceInput = new ExternalDevicesInputReader();
            //_gameUIInputView = new GameUIInputView();
            _playerBrain = new PlayerBrain(_playerEntity, new List<IEntityInputSource>
            {
                _gameUIInputView,
                _externalDeviceInput
            });
            
        }

        private void Update()
        {
            if (_onPause)
                return;
            // _externalDeviceInput.OnUpdate();
        }

        private void FixedUpdate()
        {
            _playerBrain.OnFixedUpdate();
        }
    }
}
