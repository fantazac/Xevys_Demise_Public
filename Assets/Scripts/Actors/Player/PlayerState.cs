using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour
{
    private InvincibilityAfterBeingHit _invincibility;

    public static bool IsInvincible { get; private set; }

    private void Start()
    {
        _invincibility = StaticObjects.GetPlayer().GetComponent<InvincibilityAfterBeingHit>();
        _invincibility.OnInvincibilityEnabled += EnableInvincibility;
        _invincibility.OnInvincibilityFinished += DisableInvincibility;
    }

    private void EnableInvincibility()
    {
        IsInvincible = true;
    }

    private void DisableInvincibility()
    {
        IsInvincible = false;
    }
}
