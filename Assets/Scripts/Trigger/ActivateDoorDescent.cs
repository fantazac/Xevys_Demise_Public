using UnityEngine;
using System.Collections;

public class ActivateDoorDescent : MonoBehaviour
{

    [SerializeField]
    private GameObject _door;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(_door != null && collider.gameObject.tag == "Player")
        {
            _door.GetComponent<EnableDoor>().IsActivated = true;
        }
    }

}
