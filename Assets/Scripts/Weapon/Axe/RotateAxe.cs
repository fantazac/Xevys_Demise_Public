using UnityEngine;
using System.Collections;

public class RotateAxe : MonoBehaviour
{
    [SerializeField]
    private float _rotationByFrame = 5f;

    void Update()
    {
        if (GetComponentInChildren<PolygonCollider2D>().isTrigger && GetComponentInChildren<PolygonCollider2D>().isTrigger)
        {
            if (!GetComponent<SpriteRenderer>().flipY)
            {
                transform.Rotate(new Vector3(0, 0, -1 * _rotationByFrame));
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, _rotationByFrame));
            }
        }
    }
}
