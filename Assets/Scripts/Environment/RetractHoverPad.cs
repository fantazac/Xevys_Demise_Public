using UnityEngine;
using System.Collections;

/* BEN_REVIEW
 * 
 * Voir EnableDoor. 
 */
public class RetractHoverPad : MonoBehaviour
{

    private const float RETRACT_AMOUNT = 0.5f;
    private const float RETRACT_SPEED = 0.025f;

    private bool _retract = false;
    private float _retractCount = 0;

    public bool Retract { set { _retract = value; } }

    private void Update()
    {
        if (_retract)
        {
            if (_retractCount < RETRACT_AMOUNT)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - RETRACT_SPEED, transform.position.z);
                _retractCount += RETRACT_SPEED;
            }
            else
            {
                Destroy(gameObject);
            }  
        }
    }
}
