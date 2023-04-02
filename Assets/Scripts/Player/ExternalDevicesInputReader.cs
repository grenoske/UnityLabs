using UnityEngine;
using Player.Interfaces;

namespace Player
{
    internal class ExternalDevicesInputReader : IEntityInputSource
    {
        public float HorizontalDirection => Input.GetAxisRaw("Horizontal");
        public float VerticalDirection => Input.GetAxisRaw("Vertical");
/*        public bool Attack { get; private set; }

        public void OnUpdate()
        {
            if (Input.GetButton("Attack"))
                Attack = true;
        }

        public void ResetOneTimeActions()
        {
            Attack = false;
        }*/
    }
}
