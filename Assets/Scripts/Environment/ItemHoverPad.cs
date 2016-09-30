using UnityEngine;
using System.Collections;

public class ItemHoverPad : MonoBehaviour
{
    [SerializeField]
    private float _hoverForce = 8f;

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "BaseKnifeItem")
        {
            collider.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * _hoverForce);
        }
    }
}
