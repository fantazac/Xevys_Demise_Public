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

    private ShowEquippedWeapons _showEquippedWeapons;

    private bool _soundPlayed;

    private void Start()
    {
        _showEquippedWeapons = GameObject.Find("SelectedWeaponCanvas").GetComponent<ShowEquippedWeapons>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (gameObject.tag == "BaseKnifeItem")
            {
                collider.GetComponent<PlayerThrowingWeaponsMunitions>().KnifeMunition += BASE_KNIFE_AMOUNT_ON_PICKUP;
                GetComponent<ActivateHoverRetract>().ActivateRetract();
                _showEquippedWeapons.OnKnifeAmmoChanged(collider.GetComponent<PlayerThrowingWeaponsMunitions>().KnifeMunition);
                collider.GetComponent<InventoryManager>().EnableKnife();
            }
            else if (gameObject.tag == "BaseAxeItem")
            {
                collider.GetComponent<PlayerThrowingWeaponsMunitions>().AxeMunition += BASE_AXE_AMOUNT_ON_PICKUP;
                GetComponent<ActivateHoverRetract>().ActivateRetract();
                _showEquippedWeapons.OnAxeAmmoChanged(collider.GetComponent<PlayerThrowingWeaponsMunitions>().AxeMunition);
                collider.GetComponent<InventoryManager>().EnableAxe();
            }
            else if (gameObject.tag == "KnifePickableItem")
            {
                collider.GetComponent<PlayerThrowingWeaponsMunitions>().KnifeMunition += KNIFE_AMOUNT_ON_PICKUP;
                _showEquippedWeapons.OnKnifeAmmoChanged(collider.GetComponent<PlayerThrowingWeaponsMunitions>().KnifeMunition);
            }
            else if (gameObject.tag == "AxePickableItem")
            {
                collider.GetComponent<PlayerThrowingWeaponsMunitions>().AxeMunition += AXE_AMOUNT_ON_PICKUP;
                _showEquippedWeapons.OnAxeAmmoChanged(collider.GetComponent<PlayerThrowingWeaponsMunitions>().AxeMunition);
            }
            else if (gameObject.tag == "FeatherItem")
            {
                GetComponent<ActivateHoverRetract>().ActivateRetract();
                collider.GetComponentInChildren<InventoryManager>().EnableFeather();
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
