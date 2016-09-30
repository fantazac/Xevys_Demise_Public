using UnityEngine;
using System.Collections;

public class PickThrowingWeaponsMunitions : MonoBehaviour
{
    [SerializeField]
    private const int AXE_AMOUNT_ON_PICKUP = 5;
    private const int BASE_AXE_AMOUNT_ON_PICKUP = 10;
    [SerializeField]
    private const int KNIFE_AMOUNT_ON_PICKUP = 5;
    private const int BASE_KNIFE_AMOUNT_ON_PICKUP = 10;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (gameObject.tag == "KnifePickableItem")
                collider.GetComponent<PlayerThrowingWeaponsMunitions>().KnifeMunition = KNIFE_AMOUNT_ON_PICKUP;
            else if (gameObject.tag == "BaseKnifeItem")
                collider.GetComponent<PlayerThrowingWeaponsMunitions>().KnifeMunition = BASE_KNIFE_AMOUNT_ON_PICKUP;
            else if (gameObject.tag == "AxePickableItem")
                collider.GetComponent<PlayerThrowingWeaponsMunitions>().AxeMunition = AXE_AMOUNT_ON_PICKUP;
            else if (gameObject.tag == "BaseAxeItem")
                collider.GetComponent<PlayerThrowingWeaponsMunitions>().AxeMunition = BASE_AXE_AMOUNT_ON_PICKUP;

            Destroy(gameObject);
        }
    }
}
