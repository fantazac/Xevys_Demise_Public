﻿using UnityEngine;
using System.Collections;

public class RetractDoor : MonoBehaviour
{
    private const float RETRACT_AMOUNT = 4f;
    private const float RETRACT_SPEED = 0.12f;

    private bool _retract = false;
    private float _retractCount = 0;

    [SerializeField]
    private bool _retractHorizontally = false;

    public bool Retract { set { _retract = value; } }

    private void Update()
    {
        if (_retract)
        {
            if (_retractCount < RETRACT_AMOUNT)
            {
                if (_retractHorizontally)
                {
                    transform.position = new Vector3(transform.position.x + RETRACT_SPEED, transform.position.y, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + RETRACT_SPEED, transform.position.z);
                }
                _retractCount += RETRACT_SPEED;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
