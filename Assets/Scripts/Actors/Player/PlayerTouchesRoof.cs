using UnityEngine;
using System.Collections;

public class PlayerTouchesRoof : MonoBehaviour
{

    public bool TouchesRoof { get; set; }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        SetTouchesRoof(collider, true);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        SetTouchesRoof(collider, false);
    }

    private void SetTouchesRoof(Collider2D collider, bool touchesRoof)
    {
        if (collider.gameObject.tag == StaticObjects.GetObjectTags().Wall)
        {
            TouchesRoof = touchesRoof;
        }
    }
}
