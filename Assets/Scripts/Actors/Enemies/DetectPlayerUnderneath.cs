using UnityEngine;
using System.Collections;

public class DetectPlayerUnderneath : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(!GetComponentInParent<MoveBatInDelimitedArea>().IsPlayerUnderneath)
        {
            if (collider.gameObject.tag == "Player" && collider is BoxCollider2D)
            {
                GetComponentInParent<MoveBatInDelimitedArea>().IsPlayerUnderneath = true;
            }
        }
    }
}
