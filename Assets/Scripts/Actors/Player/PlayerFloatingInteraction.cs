using UnityEngine;
using System.Collections;

public class PlayerFloatingInteraction : MonoBehaviour
{
    public delegate void OnPlayerUnderWaterHandler();
    public event OnPlayerUnderWaterHandler OnPlayerUnderWater;

    public delegate void OnPlayerOutOfWaterHandler();
    public event OnPlayerOutOfWaterHandler OnPlayerOutOfWater;

    private Rigidbody2D _rigidbody;
    private PlayerGroundMovement _playerGroundMovement;
    private PlayerWaterMovement _playerWaterMovement;
    private PlayerState _playerState;

    private void Start()
    {
        _playerState = StaticObjects.GetPlayerState();
        _rigidbody = StaticObjects.GetPlayer().GetComponent<Rigidbody2D>();
        _playerGroundMovement = StaticObjects.GetPlayer().GetComponent<PlayerGroundMovement>();
        _playerWaterMovement = StaticObjects.GetPlayer().GetComponent<PlayerWaterMovement>();

        OnPlayerUnderWater += StaticObjects.GetPlayerState().EnableFloating;

        StaticObjects.GetPlayer().GetComponent<Health>().OnDeath += ExitWater;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == StaticObjects.GetObjectTags().Water && collider.transform.position.y > transform.position.y && _rigidbody.velocity.y < 0)
        {
            EnterWater();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == StaticObjects.GetObjectTags().Water && collider.transform.position.y < transform.position.y)
        {
            ExitWater();
        }
    }

    private void EnterWater()
    {
        OnPlayerUnderWater();

        _playerWaterMovement.enabled = true;
        _playerGroundMovement.enabled = false;

        _playerState.DisableFloating();
    }

    private void ExitWater()
    {
        OnPlayerOutOfWater();

        _playerState.EnableFloating();
    }
}
