using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using InputReader;
using Player;
using InputReader.Interfaces;
using Core.Services.Updater;
using Assets.Scripts.Core.Services;
using UnityEngine.SceneManagement;

namespace Core
{
    public class GameLevelInitializer : MonoBehaviour
    {
        [SerializeField] private PlayerEntity _playerEntity;
        [SerializeField] private GameUIInputView _gameUIInputView;
        [SerializeField] private LocationStartPoint _locationPoint;

        private ExternalDevicesInputReader _externalDeviceInput;
        private PlayerSystem _playerSystem;
        private ProjectUpdater _projectUpdater;
        public static bool onPause = false;
        public static bool onRestart = false;
        private List<IDisposable> _disposables;

        private void Awake()
        {
            _disposables = new List<IDisposable>();
            if (ProjectUpdater.Instance == null)
                _projectUpdater = new GameObject().AddComponent<ProjectUpdater>();
            else
                _projectUpdater = ProjectUpdater.Instance as ProjectUpdater;
            _externalDeviceInput = new ExternalDevicesInputReader();
            if (_locationPoint != null && ProjectUpdater.LocationStartPoint != null)
                _locationPoint.SetPlayerPos(ProjectUpdater.LocationStartPoint);
            _playerSystem = new PlayerSystem(_playerEntity, new List<IEntityInputSource>
            {
                _gameUIInputView,
                _externalDeviceInput
            });
            _disposables.Add(_playerSystem);
            _disposables.Add(_externalDeviceInput);

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || onPause == true)
            {
                _projectUpdater.IsPaused = !_projectUpdater.IsPaused;
                onPause = false;
            }
            if (onRestart == true)
            {
                _projectUpdater.IsPaused = !_projectUpdater.IsPaused;
                SceneManager.LoadScene(0);
                onRestart = false;
            }
        }

        private void OnDestroy()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }
    }
}
