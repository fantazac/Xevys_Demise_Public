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

        if (StaticObjects.GetPlayer().GetComponent<InventoryManager>().EarthArtefactEnabled)
        {
            GameObject.Find("Green").GetComponent<SpriteRenderer>().enabled = true;
        }

        if (StaticObjects.GetPlayer().GetComponent<InventoryManager>().AirArtefactEnabled)
        {
            GameObject.Find("Yellow").GetComponent<SpriteRenderer>().enabled = true;
        }

        if (StaticObjects.GetPlayer().GetComponent<InventoryManager>().WaterArtefactEnabled)
        {
            GameObject.Find("Blue").GetComponent<SpriteRenderer>().enabled = true;
        }

        if (StaticObjects.GetPlayer().GetComponent<InventoryManager>().FireArtefactEnabled)
        {
            GameObject.Find("Red").GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
