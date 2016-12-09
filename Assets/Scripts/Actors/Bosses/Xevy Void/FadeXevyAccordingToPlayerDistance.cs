using UnityEngine;
using System.Collections;

public class FadeXevyAccordingToPlayerDistance : MonoBehaviour
{
    [SerializeField]
    private float _distanceToTakeEffect = 10;

    private const float BYTE_LIMIT = 255;

    SpriteRenderer _spriteRenderer;
    GameObject _player;


	private void Start ()
    {
        _distanceToTakeEffect = Mathf.Max(0, _distanceToTakeEffect);

        _player = StaticObjects.GetPlayer();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(FadeAccordingToPlayerDistance());
	}
	
	private IEnumerator FadeAccordingToPlayerDistance()
    {
        while (true)
        {
            float _distanceWithPlayer = Vector2.Distance(transform.position, _player.transform.position);
            if (_distanceWithPlayer <= _distanceToTakeEffect)
            {
                _spriteRenderer.color = new Color(BYTE_LIMIT, BYTE_LIMIT, BYTE_LIMIT, _distanceWithPlayer/_distanceToTakeEffect);
            }
            yield return null;

        }
    }
}
