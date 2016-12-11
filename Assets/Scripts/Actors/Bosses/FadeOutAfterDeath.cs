using UnityEngine;
using System.Collections;

public class FadeOutAfterDeath : MonoBehaviour
{
    [SerializeField]
    private float _timeBeforeDisappearance = 1;

    SpriteRenderer _spriteRenderer;

    private float _timeElapsed = 0;

	void Start ()
    {
        StartCoroutine(FadeOut());
        _spriteRenderer = GetComponent<SpriteRenderer>();
	}
	

    private IEnumerator FadeOut()
    {
        while (true)
        {
            yield return null;
            _timeElapsed += Time.deltaTime/_timeBeforeDisappearance;
            _spriteRenderer.color = new Color(255, 255, 255, 1.0f - _timeElapsed);
        }
    }
}
