using UnityEngine;
using System.Collections;

public class AxeWaterInteraction : MonoBehaviour
{
    public delegate void OnEnterWaterHandler();
    public event OnEnterWaterHandler OnEnterWater;

    public delegate void OnExitWaterHandler();
    public event OnExitWaterHandler OnExitWater;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if(StaticObjects.GetPlayer().GetComponent<PlayerWaterMovement>().enabled &&
            !StaticObjects.GetPlayerState().IsFloating)
        {
            OnEnterWater();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == StaticObjects.GetUnityTags().Water && 
            collider.transform.position.y > transform.position.y && _rigidbody.velocity.y < 0)
        {
            OnEnterWater();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == StaticObjects.GetUnityTags().Water && collider.transform.position.y < transform.position.y)
        {
            OnExitWater();
        }
    }
}
