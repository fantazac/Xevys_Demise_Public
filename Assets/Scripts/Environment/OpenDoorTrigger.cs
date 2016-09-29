using UnityEngine;
using System.Collections;

public class OpenDoorTrigger : MonoBehaviour
{

    [SerializeField]
    private GameObject _door;

    [SerializeField]
    private GameObject[] _wallsToActivate;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_door != null && collider.gameObject.tag == "Axe")
        {
            foreach (GameObject wall in _wallsToActivate)
                wall.GetComponent<BoxCollider2D>().enabled = true;

            Destroy(_door);
            Destroy(gameObject);
        }
    }
}
