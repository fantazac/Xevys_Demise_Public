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
        GetComponent<Health>().OnDamageTaken += StartInvincibility;

        _invincibilityDelay = new WaitForSeconds(_invincibilityTime);
    }

    private IEnumerator DisableInvincibility()
    {
        yield return _invincibilityDelay;

        if(OnInvincibilityFinished != null)
        {
            OnInvincibilityFinished();
        } 
    }

    public void StartInvincibility(int hitPoints)
    {
        if (OnInvincibilityStarted != null)
        {
            OnInvincibilityStarted(_invincibilityTime);
        }
        
        StartCoroutine(DisableInvincibility());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
