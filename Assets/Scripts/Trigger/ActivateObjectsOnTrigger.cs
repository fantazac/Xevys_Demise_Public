using UnityEngine;
using System.Collections;

public class ActivateObjectsOnTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _objectsToActivate;

    private void Start()
    {
        GetComponent<ActivateTrigger>().OnTrigger += Activate;
    }

    private void Activate()
    {
        GameObject.Find("CharacterSprite").GetComponent<Animator>().SetBool("HasSword", true);
        foreach (GameObject objectToActivate in _objectsToActivate)
        {
            objectToActivate.SetActive(true);
        }
    }
}
