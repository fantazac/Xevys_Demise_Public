using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour
{
    void Update()
    {
        if (GetComponentInChildren<PolygonCollider2D>().isTrigger && GetComponentInChildren<PolygonCollider2D>().isTrigger)
        {
            if (!GetComponent<SpriteRenderer>().flipY)
            {
                transform.Rotate(new Vector3(0, 0, -5f));
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, 5f));
            }
        }
    }
}
