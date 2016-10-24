using UnityEngine;
using System.Collections;

/* BEN_REVIEW
 * 
 * Pour les couteaux de lancer et les haches ? Ce sont des "munitions" n'est-ce pas ? Pourquoi pas nommer en conséquence.
 */
public class DestroyWeapon : MonoBehaviour
{
    /* BEN_REVIEW
     * 
     * À modifier dans l'éditeur.
     */
    private const float DESTROY_DELAY_AFTER_HIT_WALL = 0.833f;

    private bool _touchesGround;
    private bool _destroyNow;

    public bool TouchesGround { get { return _touchesGround; } set { _touchesGround = value; } }
    public bool DestroyNow { set { _destroyNow = value; } }

    void Start()
    {
        if (gameObject.tag != "Knife")
        {
            _touchesGround = GetComponentInChildren<AxeHandleHitWall>().TouchesGround;
        }

        _destroyNow = false;
    }

    void Update()
    {
        if (_touchesGround)
        {
            Destroy(gameObject, DESTROY_DELAY_AFTER_HIT_WALL);
        }
        else if (_destroyNow)
        {
            Destroy(gameObject);
        }
    }
}
