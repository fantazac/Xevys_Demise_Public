using UnityEngine;

public class WaterTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "PlayerHitbox")
            collider.GetComponent<PlayerWaterInteraction>().OnWaterEnter(collider);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.GetComponent<Transform>().tag == "PlayerHitbox")
            collider.GetComponent<PlayerWaterInteraction>().OnWaterExit(collider);
    }
}
