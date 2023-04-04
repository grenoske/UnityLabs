using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.Movement.Data
{
    [Serializable]
    public class DirectionalMovementData
    {
        [field: SerializeField] public float MoveSpeed;
        [field: SerializeField] public float DiagonalMoveModifier;
        [NonSerialized]public Vector2 LastMove;
        [NonSerialized] public float CurrentMoveSpeed;
        [NonSerialized] public bool IsMoving;
        [NonSerialized] public bool IsChangingAnim;
    }
}
