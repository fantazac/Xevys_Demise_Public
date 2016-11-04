using UnityEngine;
using System.Collections;

/* BEN_CORRECTION
 * 
 * Peut-être utilisé pour toutes sortes de choses, pas juste pour le player. À déplacer et à renommer.
 */
public class FollowPlayerPosition : MonoBehaviour
{

    [SerializeField]
    private GameObject _player;

    void Update()
    {
        if (_player != null)
        {
            GetComponent<Transform>().position = _player.GetComponent<Transform>().position;
        }
    }
}
