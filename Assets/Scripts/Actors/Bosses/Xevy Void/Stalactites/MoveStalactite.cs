using UnityEngine;
using System.Collections;

public class MoveStalactite : MonoBehaviour
{
    [SerializeField]
    public float _speed = 2.5f;

    private float maximumHeight;
    private float initialHeight;

    private Rigidbody2D _rigidBody;

	private void Start ()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        initialHeight = transform.position.y;
        maximumHeight = initialHeight + transform.parent.transform.localScale.y;
        _rigidBody.velocity = Vector2.up * _speed;
        StartCoroutine(UpdateStalactite());
	}
	
    private IEnumerator UpdateStalactite()
    {
        while (transform.position.y >= initialHeight)
        {
            yield return null;
            if (transform.position.y >= maximumHeight)
            {
                _rigidBody.velocity = Vector2.down * _speed;
            }
        }
        Destroy(gameObject);
    }
}
