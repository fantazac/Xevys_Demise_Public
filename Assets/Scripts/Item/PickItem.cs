﻿using UnityEngine;
using System.Collections;

public class PickItem : MonoBehaviour
{
    private const int BASE_AXE_AMOUNT_ON_PICKUP = 10;
    private const int BASE_KNIFE_AMOUNT_ON_PICKUP = 10;

    private InventoryManager _inventoryManager;

    private bool _soundPlayed;

    private void Start()
    {
        _inventoryManager = StaticObjects.GetPlayer().GetComponent<InventoryManager>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (gameObject.tag == "FeatherItem")
            {
                collider.GetComponentInChildren<InventoryManager>().EnableFeather();
                _inventoryManager.EnableFeather();
            }
            else if (gameObject.tag == "IronBootsItem")
            {
                collider.GetComponentInChildren<InventoryManager>().EnableIronBoots();
                _inventoryManager.EnableIronBoots();
            }
            else if (gameObject.tag == "BubbleItem")
            {
                collider.GetComponentInChildren<InventoryManager>().EnableBubble();
                _inventoryManager.EnableBubble();
            }
            else if (gameObject.tag == "FireProofArmorItem")
            {
                collider.GetComponentInChildren<InventoryManager>().EnableFireProofArmor();
                _inventoryManager.EnableFireProofArmor();
            }
            else if (gameObject.tag == "EarthArtefactItem")
            {
                collider.GetComponentInChildren<InventoryManager>().EnableEarthArtefact();
                _inventoryManager.EnableEarthArtefact();
            }
            else if (gameObject.tag == "AirArtefactItem")
            {
                collider.GetComponentInChildren<InventoryManager>().EnableAirArtefact();
                _inventoryManager.EnableAirArtefact();
            }
            else if (gameObject.tag == "WaterArtefactItem")
            {
                collider.GetComponentInChildren<InventoryManager>().EnableWaterArtefact();
                _inventoryManager.EnableWaterArtefact();
            }
            else if (gameObject.tag == "FireArtefactItem")
            {
                collider.GetComponentInChildren<InventoryManager>().EnableFireArtefact();
                _inventoryManager.EnableFireArtefact();
            }

            GetComponent<AudioSource>().Play();
            gameObject.transform.position = new Vector3(-1000, -1000, 0);
            _soundPlayed = true;
        }
    }

    void FixedUpdate()
    {
        if (_soundPlayed && !GetComponent<AudioSource>().isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
