using UnityEngine;
using System.Collections;

public class PlayBatAnimation : MonoBehaviour
{

    private Animator _animator;
    private BatMovement _batMovement;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _batMovement = GetComponent<BatMovement>();

        _batMovement.OnBatMovement += BeginBatMovementAnimation;
        _batMovement.OnBatReachedTarget += StopBatMovementAnimation;
    }

    private void BeginBatMovementAnimation()
    {
        _animator.SetBool("IsFlying", true);
    }

    private void StopBatMovementAnimation()
    {
        _animator.SetBool("IsFlying", false);
    }

}
