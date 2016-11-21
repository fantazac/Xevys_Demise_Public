using UnityEngine;
using System.Collections;

public class InvincibilityAfterBeingHit : MonoBehaviour
{

    [SerializeField]
    private float _invincibilityTime = 2f;

    private WaitForSeconds _invincibilityDelay;

    public delegate void OnInvincibilityFinishedHandler();
    public event OnInvincibilityFinishedHandler OnInvincibilityFinished;

    public delegate void OnInvincibilityStartedHandler(float invincibilityTime);
    public event OnInvincibilityStartedHandler OnInvincibilityStarted;

    private void Start()
    {
        StaticObjects.GetPlayer().GetComponent<Health>().OnDamageTaken += StartInvincibility;

        _invincibilityDelay = new WaitForSeconds(_invincibilityTime);
    }

    private IEnumerator DisableInvincibility()
    {
        yield return _invincibilityDelay;

        OnInvincibilityFinished();
    }

    public void StartInvincibility(int hitPoints)
    {
        OnInvincibilityStarted(_invincibilityTime);
        StartCoroutine(DisableInvincibility());
    }
}
