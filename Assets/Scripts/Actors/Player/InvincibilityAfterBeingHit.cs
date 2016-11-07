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

    public delegate void OnInvincibilityFinishedHandler();
    public event OnInvincibilityFinishedHandler OnInvincibilityFinished;

    public delegate void OnInvincibilityEnabledHandler();
    public event OnInvincibilityEnabledHandler OnInvincibilityEnabled;

    private void Start()
    {
        _coroutineInvincibilityTime = _invincibilityTime - (_flickerInterval * 2);

        _health = StaticObjects.GetPlayer().GetComponent<Health>();
        _health.OnDamageTaken += StartFlicker;

        _coroutineWait = new WaitForSeconds(_flickerInterval);
    }

    private void InvincibilityFinished()
    {
        _invincibilityTimeCount = 0;
        OnInvincibilityFinished();
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
        Invoke("InvincibilityFinished", _flickerInterval * 2);
    }

    public void StartFlicker(int hitPoints)
    {
        OnInvincibilityEnabled();
        StartCoroutine("Flicker");
    }
}
