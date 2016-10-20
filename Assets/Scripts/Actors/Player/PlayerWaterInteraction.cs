using UnityEngine;
using System.Collections;

public class PlayerWaterInteraction : MonoBehaviour
{

    [SerializeField]
    private GameObject _player;

    public void OnWaterEnter(Collider2D collider)
    {
        if (collider is CircleCollider2D)
        {
            _player.GetComponent<PlayerMovement>().FeetTouchWater = true;
            _player.GetComponent<PlayerMovement>().IsFloating = false;
        }
    }

    public void OnWaterExit(Collider2D collider)
    {
        if (collider is BoxCollider2D)
        {
            _player.GetComponent<PlayerMovement>().FeetTouchWater = false;
            _player.GetComponent<PlayerMovement>().IsFloating = false;
            GameObject.Find("CharacterTouchesGround").GetComponent<BoxCollider2D>().enabled = true;
        }

        if (collider is CircleCollider2D)
        {
            _player.GetComponent<PlayerMovement>().IsFloating = true;
        }  
    }
}
