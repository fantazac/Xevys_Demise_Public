using UnityEngine;
using System.Collections;

public class PlayerFloatingInteraction : MonoBehaviour
{
    private GameObject _player;

    private void Start()
    {
        _player = StaticObjects.GetPlayer();
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Water")
        {
            _player.GetComponent<PlayerWaterMovement>().enabled = true;
            _player.GetComponent<PlayerGroundMovement>().enabled = false;

            _player.GetComponent<PlayerWaterMovement>().IsFloating = false;
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Water" && collider.transform.position.y < GetComponentInParent<Transform>().position.y)
        {
            _player.GetComponent<PlayerWaterMovement>().IsFloating = true;
        }
    }
}
