using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour
{
    private InvincibilityAfterBeingHit _invincibility;

    public static bool IsInvincible { get; private set; }
    public static bool IsJumping { get; private set; }
    public static bool IsFalling { get; private set; }
    public static bool IsMoving { get; private set; }
    public static bool IsFloating { get; private set; }
    public static bool IsCroutching { get; private set; }
    public static bool IsAttacking { get; private set; }
    public static bool IsKnockedBack { get; private set; }

    private static float _xSpeed = 0;

    public delegate void OnChangedJumpingHandler();
    public static event OnChangedJumpingHandler OnChangedJumping;

    public delegate void OnChangedFallingHandler();
    public static event OnChangedFallingHandler OnChangedFalling;

    public delegate void OnChangedMovingHandler();
    public static event OnChangedMovingHandler OnChangedMoving;

    public delegate void OnChangedFloatingHandler();
    public static event OnChangedFloatingHandler OnChangedFloating;

    public delegate void OnChangedCroutchingHandler();
    public static event OnChangedCroutchingHandler OnChangedCroutching;

    public delegate void OnChangedAttackingHandler(float animSpeed);
    public static event OnChangedAttackingHandler OnChangedAttacking;

    public delegate void OnChangedKnockbackHandler();
    public static event OnChangedKnockbackHandler OnChangedKnockback;

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

    public static void SetJumping(bool isJumping)
    {
        IsJumping = isJumping;
        OnChangedJumping();
    }

    public static void SetFalling(bool isFalling)
    {
        IsFalling = isFalling;
        OnChangedFalling();
    }

    public static void SetMoving(float xSpeed)
    {
        if (xSpeed != _xSpeed)
        {
            _xSpeed = xSpeed;
            IsMoving = xSpeed != 0;
            OnChangedMoving();
        }
    }

    public static void SetImmobile()
    {
        SetMoving(0);
    }

    public static void EnableFloating()
    {
        IsFloating = true;
        OnChangedFloating();
    }

    public static void DisableFloating()
    {
        IsFloating = false;
        OnChangedFloating();
    }

    public static void SetCroutching(bool enable)
    {
        IsCroutching = enable;
        OnChangedCroutching();
    }

    public static void SetAttacking(bool enable, float animSpeed)
    {
        IsAttacking = enable;
        OnChangedAttacking(animSpeed);
    }

    public static void SetKnockedBack(bool enable)
    {
        IsKnockedBack = enable;
        OnChangedKnockback();
    }
}
