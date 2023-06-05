using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InputReader.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace InputReader
{
    public class GameUIInputView : MonoBehaviour, IEntityInputSource
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Button _attackButton;
        public float HorizontalDirection => RoundJoystickValue(_joystick.Horizontal);
        public float VerticalDirection => RoundJoystickValue(_joystick.Vertical);

        public bool Attack { get; private set; }

        private void Awake()
        {
            _attackButton.onClick.AddListener(() => Attack = true);
        }

        private void OnDestroy()
        {
            _attackButton.onClick.RemoveAllListeners();
        }

        public void ResetOneTimeAction()
        {
            Attack = false;
        }

        private float RoundJoystickValue(float value)
        {
            const float threshold = 0.5f;
            if (value > threshold)
                return 1f;
            else if (value < -threshold)
                return -1f;
            else
                return 0f;
        }
    }
}

