using System.Collections;
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
            GameObject.Find("XevyTooltip").SetActive(false);
            GameObject.Find("XboxAttackTooltip").SetActive(false);
            GameObject.Find("KeyboardAttackTooltip").SetActive(false);

            GameObject.Find("Xevy Spell").SetActive(false);
            GameObject.Find("Xevy Hub").SetActive(false);
            StartCoroutine(SpagCoroutine());
        }
    }

    private IEnumerator SpagCoroutine()
    {
        yield return new WaitForEndOfFrame();
        _sword.SetActive(true);
        _sword.GetComponent<PauseMenuAudioSettingListener>().SetVolume(false, 0);
    }
}
