﻿using UnityEngine;
using System.Collections;

public class WeaponHitWall : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall")
        {
           Destroy(gameObject);
        }
    }
}