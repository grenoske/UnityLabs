using Player;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    [SerializeField] private PlayerEntity _playerEntity;

    private float _horizontalDirection;
    private float _verticalDirection;

    private void Update()
    {
        _horizontalDirection = Input.GetAxisRaw("Horizontal");
        _verticalDirection = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // slowdown diagonally speed
        if (Mathf.Abs(_horizontalDirection) > 0.5f && Mathf.Abs(_verticalDirection) > 0.5)
        {
            _playerEntity.DiagonalMoveResolver(true);
        }
        else
        {
            _playerEntity.DiagonalMoveResolver(false);
        }

        _playerEntity.MoveHorizontally(_horizontalDirection);
        _playerEntity.MoveVertically(_verticalDirection);
    }
}
