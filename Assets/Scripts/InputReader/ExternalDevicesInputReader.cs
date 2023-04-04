using UnityEngine;
using System;
using InputReader.Interfaces;
using Core.Services.Updater;

namespace InputReader
{
    public class ExternalDevicesInputReader : IEntityInputSource, IDisposable
    {
        public float HorizontalDirection => Input.GetAxisRaw("Horizontal");
        public float VerticalDirection => Input.GetAxisRaw("Vertical");

        public ExternalDevicesInputReader()
        {
            ProjectUpdater.Instance.UpdateCalled += OnUpdate;
        }

        private void OnUpdate()
        {
        }

        public void Dispose() => ProjectUpdater.Instance.UpdateCalled -= OnUpdate;
    }
}
