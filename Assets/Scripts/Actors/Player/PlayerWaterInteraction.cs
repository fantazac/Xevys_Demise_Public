using UnityEngine;
using System.Collections;

public class PlayerWaterInteraction : MonoBehaviour
{

    [SerializeField]
    private GameObject _player;

    public void OnWaterEnter(Collider2D collider)
    {
        if(collider is BoxCollider2D)
        {
            _player.GetComponent<PlayerWaterMovement>().enabled = true;
            _player.GetComponent<PlayerGroundMovement>().enabled = false;
        }

        if (collider is CircleCollider2D)
        {
            _player.GetComponent<PlayerWaterMovement>().FeetTouchWater = true;
            _player.GetComponent<PlayerWaterMovement>().IsFloating = false;
        }
    }

    public void OnWaterExit(Collider2D collider)
    {
        if (collider is BoxCollider2D)
        {
            _player.GetComponent<PlayerWaterMovement>().enabled = false;
            _player.GetComponent<PlayerGroundMovement>().enabled = true;
        }

        if (collider is CircleCollider2D)
        {
            _player.GetComponent<PlayerWaterMovement>().IsFloating = true;
        }  
    }
}
