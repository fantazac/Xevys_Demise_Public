using UnityEngine;
using System.Collections;

public class PickItem : MonoBehaviour
{
    [SerializeField]
    private const int AXE_AMOUNT_ON_PICKUP = 2;

    [SerializeField]
    private const int KNIFE_AMOUNT_ON_PICKUP = 2;

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
                GetComponent<ActivateHoverPlatform>().ActivateRetract();
                _showItems.KnifeAmmoChange(collider.GetComponent<PlayerThrowingWeaponsMunitions>().KnifeMunition);
            }
            else if (gameObject.tag == "BaseAxeItem")
            {
                collider.GetComponent<PlayerThrowingWeaponsMunitions>().AxeMunition += BASE_AXE_AMOUNT_ON_PICKUP;
                GetComponent<ActivateHoverPlatform>().ActivateRetract();
                _showItems.AxeAmmoChange(collider.GetComponent<PlayerThrowingWeaponsMunitions>().AxeMunition);
            }
            else if (gameObject.tag == "KnifePickableItem")
            {
                collider.GetComponent<PlayerThrowingWeaponsMunitions>().KnifeMunition += KNIFE_AMOUNT_ON_PICKUP;
                _showItems.KnifeAmmoChange(collider.GetComponent<PlayerThrowingWeaponsMunitions>().KnifeMunition);
            }
            else if (gameObject.tag == "AxePickableItem")
            {
                collider.GetComponent<PlayerThrowingWeaponsMunitions>().AxeMunition += AXE_AMOUNT_ON_PICKUP;
                _showItems.AxeAmmoChange(collider.GetComponent<PlayerThrowingWeaponsMunitions>().AxeMunition);
            }
            else if (gameObject.tag == "FeatherItem")
            {
                GetComponent<ActivateHoverPlatform>().ActivateRetract();
                collider.GetComponentInChildren<InventoryManager>().EnableFeather();
                _showItems.FeatherEnable();
            }
            else if (gameObject.tag == "IronBootsItem")
            {
                GetComponent<ActivateHoverPlatform>().ActivateRetract();
                collider.GetComponentInChildren<InventoryManager>().EnableIronBoots();
                _showItems.IronBootsEnable();
            }
            else if (gameObject.tag == "BubbleItem")
            {
                GetComponent<ActivateHoverPlatform>().ActivateRetract();
                collider.GetComponentInChildren<InventoryManager>().EnableBubble();
                _showItems.BubbleEnable();
            }
            else if (gameObject.tag == "FireProofArmorItem")
            {
                GetComponent<ActivateHoverPlatform>().ActivateRetract();
                collider.GetComponentInChildren<InventoryManager>().EnableFireProofArmor();
                _showItems.FireProofArmorEnable();
            }
            else if (gameObject.tag == "EarthArtefactItem")
            {
                GetComponent<ActivateHoverPlatform>().ActivateRetract();
                collider.GetComponentInChildren<InventoryManager>().EnableEarthArtefact();
                _showItems.EarthArtefactEnable();
            }
            else if (gameObject.tag == "AirArtefactItem")
            {
                GetComponent<ActivateHoverPlatform>().ActivateRetract();
                collider.GetComponentInChildren<InventoryManager>().EnableAirArtefact();
                _showItems.AirArtefactEnable();
            }
            else if (gameObject.tag == "WaterArtefactItem")
            {
                GetComponent<ActivateHoverPlatform>().ActivateRetract();
                collider.GetComponentInChildren<InventoryManager>().EnableWaterArtefact();
                _showItems.WaterArtefactEnable();
            }
            else if (gameObject.tag == "FireArtefactItem")
            {
                GetComponent<ActivateHoverPlatform>().ActivateRetract();
                collider.GetComponentInChildren<InventoryManager>().EnableFireArtefact();
                _showItems.FireArtefactEnable();
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
