using UnityEngine;
using System.Collections;

public class PickThrowingWeaponsMunitions : MonoBehaviour
{
    public delegate void OnKnifePickedHandler(int knifeAmount);
    public event OnKnifePickedHandler OnKnifePicked;

    public delegate void OnAxePickedHandler(int axeAmount);
    public event OnAxePickedHandler OnAxePicked;

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
                OnKnifePicked(KNIFE_AMOUNT_ON_PICKUP);
            else if (gameObject.tag == "BaseKnifeItem")
                OnKnifePicked(BASE_KNIFE_AMOUNT_ON_PICKUP);
            else if (gameObject.tag == "AxePickableItem")
                OnAxePicked(AXE_AMOUNT_ON_PICKUP);
            else if (gameObject.tag == "BaseAxeItem")
                OnAxePicked(BASE_AXE_AMOUNT_ON_PICKUP);
        }
    }
}
