﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTriggersOnRoomCleaned : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _triggersToActivate;

    [SerializeField]
    private List<GameObject> _triggersToDeactivate;

    [SerializeField]
    private GameObject _bossToKill;

    [SerializeField]
    private GameObject _sword;

    public void ReactivateTriggers()
    {
        if (_triggersToActivate != null)
        {          
            foreach (GameObject trigger in _triggersToActivate)
            {
                if(trigger.GetComponent<PlayCinematicOnTrigger>() != null)
                {
                    trigger.GetComponent<PlayCinematicOnTrigger>().DisableCinematicOnReload();
                }
                if (trigger.GetComponent<ActivateMultipleTriggers>() != null)
                {
                    trigger.GetComponent<ActivateTrigger>().MultipleTriggersActivated();
                }
                else
                {
                    trigger.GetComponent<ActivateTrigger>().DisableTrigger();
                }
                if (trigger.GetComponent<PauseMenuAudioSettingListener>() != null)
                {
                    trigger.GetComponent<PauseMenuAudioSettingListener>().SetVolume(false, 0);
                }
            }
        }

        if (_triggersToDeactivate != null)
        {
            foreach (GameObject trigger in _triggersToDeactivate)
            {
                trigger.SetActive(false);
            }
        }

        if (_bossToKill != null)
        {
            // SPAGAT
            _bossToKill.GetComponent<ChangeMusicZoneOnDeath>().ChangeMusicZone();
            GameObject.Find("XevyTooltip").SetActive(false);
            GameObject.Find("XboxAttackTooltip").SetActive(false);
            GameObject.Find("KeyboardAttackTooltip").SetActive(false);

            GameObject.Find("Xevy Spell").SetActive(false);
            GameObject.Find("Xevy Hub").SetActive(false);
            StartCoroutine(WaitForNextFrameToActivateSword());

            if (StaticObjects.GetPlayer().GetComponent<InventoryManager>().KnifeEnabled)
            {
                GameObject.Find("XboxKnifeTooltip").SetActive(false);
                GameObject.Find("KeyboardKnifeTooltip").SetActive(false);
            }

            if (StaticObjects.GetPlayer().GetComponent<InventoryManager>().AxeEnabled)
            {
                GameObject.Find("XboxAxeTooltip").SetActive(false);
                GameObject.Find("KeyboardAxeTooltip").SetActive(false);
            }

            if (StaticObjects.GetPlayer().GetComponent<InventoryManager>().KnifeEnabled &&
                StaticObjects.GetPlayer().GetComponent<InventoryManager>().AxeEnabled)
            {
                GameObject.Find("XboxChangeWeaponTooltip").SetActive(false);
                GameObject.Find("KeyboardChangeWeaponTooltip").SetActive(false);
                GameObject.Find("XboxChangeWeaponTooltip").SetActive(false);
                GameObject.Find("KeyboardChangeWeaponTooltip").SetActive(false);
            }

            if (StaticObjects.GetPlayer().GetComponent<InventoryManager>().FeatherEnabled)
            {
                GameObject.Find("KeyboardFeatherTooltip").SetActive(false);
                GameObject.Find("XboxFeatherTooltip").SetActive(false);
            }

            if (StaticObjects.GetPlayer().GetComponent<InventoryManager>().IronBootsEnabled)
            {
                GameObject.Find("KeyboardIronBootsTooltip").SetActive(false);
                GameObject.Find("XboxIronBootsTooltip").SetActive(false);
            }

            if (StaticObjects.GetPlayer().GetComponent<InventoryManager>().BubbleEnabled)
            {
                GameObject.Find("BubbleTooltip").SetActive(false);
            }

            if (StaticObjects.GetPlayer().GetComponent<InventoryManager>().FireProofArmorEnabled)
            {
                GameObject.Find("FireArmorTooltip").SetActive(false);
            }
        }
    }

    private IEnumerator WaitForNextFrameToActivateSword()
    {
        yield return null;
        
        _sword.SetActive(true);
        _sword.GetComponentInChildren<AudioSource>().enabled = false;
    }
}
