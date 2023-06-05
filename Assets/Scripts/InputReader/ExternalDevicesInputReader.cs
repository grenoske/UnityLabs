using UnityEngine;
using System;
using InputReader.Interfaces;
using Core.Services.Updater;
using UnityEngine.EventSystems;

namespace InputReader
{
    public class ExternalDevicesInputReader : IEntityInputSource, IDisposable
    {
        public float HorizontalDirection => Input.GetAxisRaw("Horizontal");
        public float VerticalDirection => Input.GetAxisRaw("Vertical");

        public bool Attack { get; private set; }

        public ExternalDevicesInputReader()
        {
            ProjectUpdater.Instance.UpdateCalled += OnUpdate;
        }

        private void OnUpdate()
        {
            if (!isPointerOverUi() && Input.GetButton("Fire1"))
            {
                Attack = true;
            }

        }

        private bool isPointerOverUi() => EventSystem.current.IsPointerOverGameObject();

        public void ResetOneTimeAction()
        {
            Attack = false;
        }

        public void Dispose() => ProjectUpdater.Instance.UpdateCalled -= OnUpdate;
    }
}
