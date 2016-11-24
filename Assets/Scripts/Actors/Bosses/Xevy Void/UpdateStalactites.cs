using UnityEngine;
using System.Collections;

public class UpdateStalactites : MonoBehaviour
{
    private const float DELAY_BETWEEN_STALACTITE_ACTIVATION = 0.25f;
    private float _stalactiteActivatorTimer = 0;

    private void Update()
    {
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            _stalactiteActivatorTimer += Time.fixedDeltaTime;
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
                else
                {
                    child.GetComponent<MoveStalactite>().UpdateEarthThorn();
                }
                index++;
            }
        }
    }
}