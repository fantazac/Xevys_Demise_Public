using UnityEngine;
using System.Collections;

public class RemoveDoorOnPlayerTouch : MonoBehaviour
{

    [SerializeField]
    private GameObject _door;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_door != null && collider.gameObject.tag == "Player")
        {
            _door.GetComponent<RetractDoor>().Retract = true;
        }
    }

}
