using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour
{
    private InvincibilityAfterBeingHit _invincibility;

    public bool IsInvincible { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsFalling { get; private set; }
    public bool IsMoving { get; private set; }
    public bool IsFloating { get; private set; }
    public bool IsCroutching { get; private set; }
    public bool IsAttacking { get; private set; }
    public bool IsKnockedBack { get; private set; }

    private float _xSpeed = 0;

    public delegate void OnChangedJumpingHandler();
    public event OnChangedJumpingHandler OnChangedJumping;

    public delegate void OnChangedFallingHandler();
    public event OnChangedFallingHandler OnChangedFalling;

    public delegate void OnChangedMovingHandler();
    public event OnChangedMovingHandler OnChangedMoving;

    public delegate void OnChangedFloatingHandler();
    public event OnChangedFloatingHandler OnChangedFloating;

    public delegate void OnChangedCroutchingHandler();
    public event OnChangedCroutchingHandler OnChangedCroutching;

    public delegate void OnChangedAttackingHandler(float animSpeed);
    public event OnChangedAttackingHandler OnChangedAttacking;

    public delegate void OnChangedKnockbackHandler();
    public event OnChangedKnockbackHandler OnChangedKnockback;

    private void Start()
    {
        _invincibility = GetComponent<InvincibilityAfterBeingHit>();
        _invincibility.OnInvincibilityStarted += EnableInvincibility;
        _invincibility.OnInvincibilityFinished += DisableInvincibility;

        IsFloating = false;
    }

    private void EnableInvincibility(float invincibilityTime)
    {
        IsInvincible = true;
    }

    private void DisableInvincibility()
    {
        IsInvincible = false;
    }

    public void SetJumping()
    {
        IsJumping = !IsJumping;
        OnChangedJumping();
    }

    public void SetFalling()
    {
        IsFalling = !IsFalling;
        OnChangedFalling();
    }

    public void SetMoving(float xSpeed)
    {
        if (xSpeed != _xSpeed)
        {
            _xSpeed = xSpeed;
            IsMoving = xSpeed != 0;
            OnChangedMoving();
        }
    }

    public void SetImmobile()
    {
        SetMoving(0);
    }

    public void EnableFloating()
    {
        IsFloating = true;
        OnChangedFloating();
    }

    public void DisableFloating()
    {
        IsFloating = false;
        OnChangedFloating();
    }

    public void SetCroutching(bool enable)
    {
        IsCroutching = enable;
        OnChangedCroutching();
    }

    public void SetAttacking(bool enable, float animSpeed)
    {
        IsAttacking = enable;
        OnChangedAttacking(animSpeed);
    }

    public void SetKnockedBack(bool enable)
    {
        IsKnockedBack = enable;
        OnChangedKnockback();
    }
}
