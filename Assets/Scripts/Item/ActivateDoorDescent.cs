using UnityEngine;
using System.Collections;

public class ActivateDoorDescent : MonoBehaviour
{

    [SerializeField]
    private GameObject _door;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(_door != null)
        {
            _door.GetComponent<EnableDoor>().Descent = true;
        }
    }

}
