using UnityEngine;
using System.Collections;

public class PortalManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _endPortal;

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.GetComponent<PlayerPortalManager>().IsInPortal = true;
            collider.GetComponent<PlayerPortalManager>().Portal = _endPortal;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.GetComponent<PlayerPortalManager>().IsInPortal = false;
            collider.GetComponent<PlayerPortalManager>().Portal = null;
        }
    }
}
