using UnityEngine;
using System.Collections;

public class ElevateFlyingPlatform : MonoBehaviour
{

    private const float ELEVATION_AMOUNT = 2.75f;
    private const float ELEVATION_SPEED = 0.06125f;

    private bool _elevate = false;
    private float _elevationCount = 0;

    public bool Elevate { set { _elevate = value; } }

    private void Update()
    {
        if (_elevate)
        {
            if (_elevationCount < ELEVATION_AMOUNT)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + ELEVATION_SPEED, transform.position.z);
                _elevationCount += ELEVATION_SPEED;
            }
            else
            {
                _elevate = false;
            }
        }
    }

}
