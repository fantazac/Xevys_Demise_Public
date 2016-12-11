using UnityEngine;
using System.Collections;

public class MoveSoulTowardsPortal : MonoBehaviour
{
    [SerializeField]
    private GameObject _portal;

    [SerializeField]
    private float _speed = 2;

	private void Start()
    {
        StartCoroutine(MoveTowardsPortal());
	}

    private IEnumerator MoveTowardsPortal()
    {
        while (Vector2.Distance(_portal.transform.position, transform.position) == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, _portal.transform.position, _speed * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}
