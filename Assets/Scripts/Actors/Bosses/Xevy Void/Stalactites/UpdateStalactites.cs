using UnityEngine;
using System.Collections;

public class UpdateStalactites : MonoBehaviour
{
    private const float DELAY_BETWEEN_STALACTITE_ACTIVATION = 0.25f;
    private float _stalactiteActivatorTimer = 0;

    private void Start()
    {
        StartCoroutine(MoveStalactites());
    }

    private IEnumerator MoveStalactites()
    {
        while (transform.childCount > 0)
        {
            yield return null;
            int index = 0;
            foreach (Transform child in transform)
            {
                if (!child.gameObject.activeSelf)
                {
                    if (_stalactiteActivatorTimer >= DELAY_BETWEEN_STALACTITE_ACTIVATION * index)
                    {
                        child.gameObject.SetActive(true);
                    }
                }
                index++;
            }
            _stalactiteActivatorTimer += Time.deltaTime;
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}