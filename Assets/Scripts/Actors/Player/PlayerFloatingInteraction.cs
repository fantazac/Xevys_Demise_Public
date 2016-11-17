using UnityEngine;
using System.Collections;

public class PlayerFloatingInteraction : MonoBehaviour
{
    public delegate void OnPlayerUnderWaterHandler();
    public event OnPlayerUnderWaterHandler OnPlayerUnderWater;

    public delegate void OnPlayerOutOfWaterHandler();
    public event OnPlayerOutOfWaterHandler OnPlayerOutOfWater;

    private GameObject _player;
    private PlayerGroundMovement _playerGroundMovement;
    private PlayerWaterMovement _playerWaterMovement;

    private void Start()
    {
        _player = StaticObjects.GetPlayer();

        _playerGroundMovement = _player.GetComponent<PlayerGroundMovement>();
        _playerWaterMovement = _player.GetComponent<PlayerWaterMovement>();
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Water" && collider.transform.position.y > transform.position.y)
        {
            OnPlayerUnderWater();

            _player.GetComponent<PlayerWaterMovement>().enabled = true;
            _player.GetComponent<PlayerGroundMovement>().enabled = false;

            _player.GetComponent<PlayerWaterMovement>().IsFloating = false;
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Water" && collider.transform.position.y < transform.position.y)
        {
            OnPlayerOutOfWater();

            _player.GetComponent<PlayerWaterMovement>().IsFloating = true;
        }
    }
}
