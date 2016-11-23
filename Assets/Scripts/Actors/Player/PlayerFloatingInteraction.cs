﻿using UnityEngine;
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

    private void Start()
    {
        _rigidbody = StaticObjects.GetPlayer().GetComponent<Rigidbody2D>();
        _playerGroundMovement = StaticObjects.GetPlayer().GetComponent<PlayerGroundMovement>();
        _playerWaterMovement = StaticObjects.GetPlayer().GetComponent<PlayerWaterMovement>();

        OnPlayerUnderWater += StaticObjects.GetPlayerState().EnableFloating;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == StaticObjects.GetUnityTags().Water && collider.transform.position.y > transform.position.y && _rigidbody.velocity.y < 0)
        {
            OnPlayerUnderWater();

            _playerWaterMovement.enabled = true;
            _playerGroundMovement.enabled = false;

            _playerWaterMovement.IsFloating = false;
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == StaticObjects.GetUnityTags().Water && collider.transform.position.y < transform.position.y)
        {
            OnPlayerOutOfWater();

            _playerWaterMovement.IsFloating = true;
        }
    }
}
