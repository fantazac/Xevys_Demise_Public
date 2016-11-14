using UnityEngine;
using System.Collections;

public class InvincibilityAfterBeingHit : MonoBehaviour
{

    [SerializeField]
    private float _invincibilityTime = 2f;

    [SerializeField]
    private float _flickerInterval = 0.12f;

    private float _coroutineInvincibilityTime = 0;
    private float _invincibilityTimeCount = 0;

    private Health _health;

    private WaitForSeconds _coroutineWait;

    private WaitForSeconds _finishInvincibilityDelay;

    public delegate void OnInvincibilityFinishedHandler();
    public event OnInvincibilityFinishedHandler OnInvincibilityFinished;

    public delegate void OnInvincibilityEnabledHandler();
    public event OnInvincibilityEnabledHandler OnInvincibilityStarted;

    private void Start()
    {
        _coroutineInvincibilityTime = _invincibilityTime - (_flickerInterval * 2);

        _health = StaticObjects.GetPlayer().GetComponent<Health>();
        _health.OnDamageTaken += StartFlicker;

        _coroutineWait = new WaitForSeconds(_flickerInterval);

        _finishInvincibilityDelay = new WaitForSeconds(_flickerInterval * 2);
    }

    private IEnumerator Flicker()
    {
        while (_invincibilityTimeCount < _coroutineInvincibilityTime)
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.gray;
            yield return _coroutineWait;

            _invincibilityTimeCount += Time.deltaTime + _flickerInterval;

            GetComponentInChildren<SpriteRenderer>().color = Color.white;
            yield return _coroutineWait;

            _invincibilityTimeCount += Time.deltaTime + _flickerInterval;
        }

        yield return _finishInvincibilityDelay;

        _invincibilityTimeCount = 0;
        OnInvincibilityFinished();
    }

    public void StartFlicker(int hitPoints)
    {
        OnInvincibilityStarted();
        StartCoroutine("Flicker");
    }
}
