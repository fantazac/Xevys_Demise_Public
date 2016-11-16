using UnityEngine;
using System.Collections;

public class PlayJumpingAnimation : MonoBehaviour
{

    private Animator _animator;
    private PlayerGroundMovement _playerGroundMovement;
    private PlayerWaterMovement _playerWaterMovement;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerGroundMovement = GetComponentInParent<PlayerGroundMovement>();
        _playerWaterMovement = GetComponentInParent<PlayerWaterMovement>();
    }

}
