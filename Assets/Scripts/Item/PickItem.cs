using UnityEngine;
using System.Collections;

public class PickItem : MonoBehaviour
{
    /* BEN_REVIEW
     * 
     * Cela devrait être le drop qui indique la quantité rammassé et non pas ce composant. Autrement dit, vous demandez
     * au drop combien il contient de l'élément à rammasser.
     */
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
        /* BEN_REVIEW
         * 
         * Vous savez que vous pouvez paramêtrer les collisions entre les layers? Par exemple, le layer "Player" ne peux
         * collider qu'avec le layer "Ennemies" et "Drops".
         */
        if (collider.gameObject.tag == "Player")
        {
            /* BEN_REVIEW
             * 
             * Vous vous fiez au TAG pour savoir ce qui est rammassé ?
             * 
             * Ouache...
             * 
             * Je pense que c'est le "Drop" qui devrait détecter la collision et avertir le player ce qu'il a rammassé.
             */
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
            else if (gameObject.tag == "IronBootsItem")
            {
                GetComponent<ActivateHoverRetract>().ActivateRetract();
                collider.GetComponentInChildren<InventoryManager>().EnableIronBoots();
                _showItems.OnIronBootsEnabled();
            }
            
            /* BEN_REVIEW
             * 
             * Mettre le son dans un autre composant.
             */
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
