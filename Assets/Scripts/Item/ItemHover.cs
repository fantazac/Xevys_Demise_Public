using UnityEngine;
using System.Collections;

/* BEN_REVIEW
 * 
 * Ce genre de chose aurait pu être fait avec une animation.
 */
public class ItemHover : MonoBehaviour
{
    [SerializeField]
    private float _speed = 0.11f;

    private const float WAVE_LENGTH = 0.08f;

    private float _sinCount = 0;

    private float _initialYPosition;

    private void Start()
    {
        _initialYPosition = transform.position.y;
    }

    private void Update()
    {
        transform.position = new Vector2(transform.position.x, _initialYPosition + WAVE_LENGTH * Mathf.Sin(_sinCount));
        /* BEN_REVIEW
         * 
         * Ou est le deltaTime là dedans ? Update n'est pas nécessairement appellé à la même vitesse sur chaque poste.
         */
        _sinCount += _speed;
    }
}
