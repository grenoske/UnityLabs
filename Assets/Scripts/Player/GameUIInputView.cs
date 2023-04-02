using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Player.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    internal class GameUIInputView : MonoBehaviour, IEntityInputSource
    {
        [SerializeField] private Joystick _joystick;
        // [SerializeField] private Button _attackButton;

        public float HorizontalDirection => _joystick.Horizontal;
        public float VerticalDirection => _joystick.Vertical;

/*        public bool Attack { get; private set; }

        private void Awake()
        {
            _attackButton.onClick.AddListener(() => Attack = true);
        }

        private void OnDestroy()
        {
            _attackButton.onClick.RemoveAllListeners(); 
        }*/
    }
}
