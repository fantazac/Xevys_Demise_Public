using UnityEngine;
using System.Collections;

public class PickItem : MonoBehaviour
{
    [SerializeField]
    private const int AXE_AMOUNT_ON_PICKUP = 5;

    [SerializeField]
    private const int KNIFE_AMOUNT_ON_PICKUP = 5;

    private const int BASE_AXE_AMOUNT_ON_PICKUP = 10;
    private const int BASE_KNIFE_AMOUNT_ON_PICKUP = 10;

    private ShowItems _showItems;

    private bool _soundPlayed;

    private void Start()
    {
        _showItems = GameObject.Find("SelectedWeaponCanvas").GetComponent<ShowItems>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (gameObject.tag == "BaseKnifeItem")
            {
                collider.GetComponent<PlayerThrowingWeaponsMunitions>().KnifeMunition += BASE_KNIFE_AMOUNT_ON_PICKUP;
                GetComponent<ActivateHoverRetract>().ActivateRetract();
                _showItems.OnKnifeAmmoChanged(collider.GetComponent<PlayerThrowingWeaponsMunitions>().KnifeMunition);
            }
            else if (gameObject.tag == "BaseAxeItem")
            {
                collider.GetComponent<PlayerThrowingWeaponsMunitions>().AxeMunition += BASE_AXE_AMOUNT_ON_PICKUP;
                GetComponent<ActivateHoverRetract>().ActivateRetract();
                _showItems.OnAxeAmmoChanged(collider.GetComponent<PlayerThrowingWeaponsMunitions>().AxeMunition);
            }
            else if (gameObject.tag == "KnifePickableItem")
            {
                collider.GetComponent<PlayerThrowingWeaponsMunitions>().KnifeMunition += KNIFE_AMOUNT_ON_PICKUP;
                _showItems.OnKnifeAmmoChanged(collider.GetComponent<PlayerThrowingWeaponsMunitions>().KnifeMunition);
            }
            else if (gameObject.tag == "AxePickableItem")
            {
                collider.GetComponent<PlayerThrowingWeaponsMunitions>().AxeMunition += AXE_AMOUNT_ON_PICKUP;
                _showItems.OnAxeAmmoChanged(collider.GetComponent<PlayerThrowingWeaponsMunitions>().AxeMunition);
            }
            else if (gameObject.tag == "FeatherItem")
            {
                GetComponent<ActivateHoverRetract>().ActivateRetract();
                collider.GetComponentInChildren<InventoryManager>().EnableFeather();
                _showItems.OnFeatherEnabled();
            }
            else if (gameObject.tag == "EarthArtefactItem")
            {
                GetComponent<ActivateHoverRetract>().ActivateRetract();
                collider.GetComponentInChildren<InventoryManager>().EnableEarthArtefact();
            }
            else if (gameObject.tag == "AirArtefactItem")
            {
                GetComponent<ActivateHoverRetract>().ActivateRetract();
                collider.GetComponentInChildren<InventoryManager>().EnableAirArtefact();
            }
            else if (gameObject.tag == "WaterArtefactItem")
            {
                GetComponent<ActivateHoverRetract>().ActivateRetract();
                collider.GetComponentInChildren<InventoryManager>().EnableWaterArtefact();
            }
            else if (gameObject.tag == "FireArtefactItem")
            {
                GetComponent<ActivateHoverRetract>().ActivateRetract();
                collider.GetComponentInChildren<InventoryManager>().EnableFireArtefact();
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
