using UnityEngine;
using System.Collections;

/* BEN_CORRECTION
 * 
 * Quand je parlais de singleton, je pensais plutôt à une classe contenant une panoplie de méthodes pour obtenir
 * différents GameObjects de manière statique. Autrement dit, pas faire une classe par GameObject qui doit
 * être une singleton, mais une classe pour tous les GameObjects qui doivent être des singletons.
 */
public class Player : MonoBehaviour
{

    private static GameObject _player;

    private void Start()
    {
        _player = gameObject;
    }

    public static GameObject GetPlayer()
    {
        return _player;
    }
}
