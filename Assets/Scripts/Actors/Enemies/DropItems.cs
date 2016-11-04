using UnityEngine;
using System.Collections;

public class DropItems : MonoBehaviour
{
    /* BEN_CORRECTION
     * 
     * Défaut architectural. Ici, on associe le "dropRate" à un item via l'index du tableau. Devrait
     * plutôt avoir une seule list de "DropableItem" qui lui contient un lien vers le "GameObject" et
     * son "dropRate".
     * 
     * Savez vous que l'inspecteur Unity supporte les hierachies de classes ?
     * Voir : http://answers.unity3d.com/storage/temp/29533-after.png
     * 
     * Voir aussi : http://catlikecoding.com/unity/tutorials/editor/custom-data/
     */
    [SerializeField]
    private GameObject[] _items;

    [SerializeField]
    private int[] _dropRates;

    private GameObject[] _itemsToDrop;
    private int _itemsDroppedCount = 0;

    public void Drop()
    {
        /* BEN_CORRECTION
         * 
         * Ceci devrait être une variable, pas un attribut.
         */
        _itemsToDrop = new GameObject[_items.Length];

        for (int i = 0; i < _items.Length; i++)
        {
            if (Random.Range(0, 100) < _dropRates[i])
            {
                /* BEN_CORRECTION
                 * 
                 * Il doit y avoir un moyen de faire mieux que cela ? Ici, vous "hardcodez" quel drop
                 * il peut y avoir dans votre jeu. C'est loin d'être flexible.
                 * 
                 * Ce sont en quelques sortes des conditions de drop ? Il y a certainement un moyen
                 * de faire du polymorphisme.
                 */
                if ((_items[i].gameObject.tag != "AxeDrop" && _items[i].gameObject.tag != "KnifeDrop") ||
                    (_items[i].gameObject.tag == "AxeDrop" && Player.GetPlayer().GetComponent<InventoryManager>().AxeEnabled) ||
                    (_items[i].gameObject.tag == "KnifeDrop" && Player.GetPlayer().GetComponent<InventoryManager>().KnifeEnabled))
                {
                    _itemsToDrop[_itemsDroppedCount++] = _items[i];
                }
            }
        }

        for (int j = 0; j < _itemsToDrop.Length; j++)
        {
            if (_itemsToDrop[j] == null)
            {
                break;
            }
            GameObject item = (GameObject)Instantiate(_itemsToDrop[j], transform.position, new Quaternion());
            item.GetComponent<OnItemDrop>().Initialise(_itemsDroppedCount, j, GetComponent<Collider2D>());
        }
    }
}
