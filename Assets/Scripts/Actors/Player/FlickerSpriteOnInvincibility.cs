using UnityEngine;
using System.Collections;

public class FlickerSpriteOnInvincibility : MonoBehaviour
{
    [SerializeField]
    private float _flickerInterval = 0.12f;

    private WaitForSeconds _flickerDelay;

    private float _coroutineInvincibilityTime = 0;
    private float _invincibilityTimeCount = 0;

    private SpriteRenderer _sprite;

    private void Start()
    {
        GetComponent<InvincibilityAfterBeingHit>().OnInvincibilityStarted += StartFlicker;

        _sprite = GetComponentInChildren<SpriteRenderer>();

        _flickerDelay = new WaitForSeconds(_flickerInterval);
    }

    private void StartFlicker(float invincibilityTime)
    {
        _coroutineInvincibilityTime = invincibilityTime - (_flickerInterval * 2);

        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        _invincibilityTimeCount = 0;

        while (_invincibilityTimeCount < _coroutineInvincibilityTime)
        {
            _sprite.color = Color.gray;
            yield return _flickerDelay;

            _invincibilityTimeCount += Time.deltaTime + _flickerInterval;

            _sprite.color = Color.white;
            yield return _flickerDelay;

            _invincibilityTimeCount += Time.deltaTime + _flickerInterval;
        }
    }
}
