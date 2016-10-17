using UnityEngine;
using System.Collections;

public class EnableDoor : MonoBehaviour
{

    private const float DESCENT_AMOUNT = 4f;
    private const float DESCENT_SPEED = 0.2f;

    private bool _descent = false;
    private float _descentCount = 0;

    public bool Descent { set { _descent = value; } }

    private void Update()
    {
        if (_descent)
        {
            if (_descentCount < DESCENT_AMOUNT)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - DESCENT_SPEED, transform.position.z);
                _descentCount += DESCENT_SPEED;
            }
            else
            {
                _descent = false;
            }
        }
    }

}
