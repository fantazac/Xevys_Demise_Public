using UnityEngine;
using System.Collections;

public class UpdateThorns : MonoBehaviour
{
    private float DELAY_BETWEEN_THORN_ACTIVATION = 0.25f;
    private float thornActivatorTimer = 0;

    private void Update()
    {
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            thornActivatorTimer += Time.fixedDeltaTime;
            int index = 0;
            foreach (Transform child in transform)
            {
                if (!child.gameObject.activeSelf)
                {
                    if (thornActivatorTimer >= DELAY_BETWEEN_THORN_ACTIVATION * index)
                    {
                        child.gameObject.SetActive(true);
                    }
                }
                else
                {
                    child.GetComponent<MoveEarthThorn>().UpdateEarthThorn();
                }
                index++;
            }
        }
    }
}