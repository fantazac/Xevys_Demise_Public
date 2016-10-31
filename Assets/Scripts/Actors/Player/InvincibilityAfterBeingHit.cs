using UnityEngine;
using System.Collections;

public class InvincibilityAfterBeingHit : MonoBehaviour
{

    [SerializeField]
    private const float INVINCIBILITY_TIME = 120;
    [SerializeField]
    private const float FLICKER_INTERVAL = 5;

    private float _invincibilityCount = 0;
    private bool _flickerSprite = false;

    public float InvincibilityTime { get { return INVINCIBILITY_TIME; } }
    public bool IsFlickering { get { return _flickerSprite; } }

    private void FixedUpdate()
    {
        if (_flickerSprite && _invincibilityCount == INVINCIBILITY_TIME - (FLICKER_INTERVAL * 2))
        {
            _flickerSprite = false;
            GetComponentInChildren<SpriteRenderer>().color = Color.white;
            _invincibilityCount = 0;
        }
        else if (_flickerSprite)
        {
            StartFlicker();
        }
    }

    private IEnumerator Flicker()
    {

        if (_invincibilityCount % (FLICKER_INTERVAL * 2) < FLICKER_INTERVAL)
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.gray;
        }
        _invincibilityCount++;

        return null;
    }

    public void StartFlicker()
    {
        _flickerSprite = true;
        StartCoroutine("Flicker");
    }
}
