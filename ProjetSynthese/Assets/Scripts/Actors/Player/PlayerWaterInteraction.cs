using UnityEngine;
using System.Collections;

public class PlayerWaterInteraction : MonoBehaviour
{

    [SerializeField]
    private GameObject _player;

    private float _speedReductionFactor = 0.4f;

    public void OnWaterEnter(Collider2D collider)
    {
        if (collider.GetType() == typeof(BoxCollider2D))
        {
            _player.GetComponent<PlayerMovement>().Speed *= _speedReductionFactor;
        }


        if (collider.GetType() == typeof(CircleCollider2D))
        {
            _player.GetComponent<PlayerMovement>().FeetTouchWater = true;
            _player.GetComponent<PlayerMovement>().IsFloating = false;
        }
    }

    public void OnWaterExit(Collider2D collider)
    {
        if (collider.GetType() == typeof(BoxCollider2D))
        {
            _player.GetComponent<PlayerMovement>().Speed /= _speedReductionFactor;
            _player.GetComponent<PlayerMovement>().FeetTouchWater = false;
            _player.GetComponent<PlayerMovement>().IsFloating = false;
            GameObject.Find("CharacterTouchesGround").GetComponent<BoxCollider2D>().enabled = true;
        }

        if (collider.GetType() == typeof(CircleCollider2D))
            _player.GetComponent<PlayerMovement>().IsFloating = true;
    }
}
