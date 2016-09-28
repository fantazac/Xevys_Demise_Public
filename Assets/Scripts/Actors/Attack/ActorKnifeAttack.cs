﻿using UnityEngine;
using System.Collections;

public class ActorKnifeAttack : MonoBehaviour {

    private InputManager _inputManager;
    private Rigidbody2D _rigidbody2D;

    void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _inputManager.OnKnifeAttack += OnKnifeAttack;
    }

    void OnKnifeAttack()
    {

    }
}