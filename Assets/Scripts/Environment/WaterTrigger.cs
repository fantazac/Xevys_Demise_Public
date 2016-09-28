using UnityEngine;

public class WaterTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "PlayerWaterHitbox")
            collider.GetComponent<PlayerWaterInteraction>().OnWaterEnter(collider);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.GetComponent<Transform>().tag == "PlayerWaterHitbox")
            collider.GetComponent<PlayerWaterInteraction>().OnWaterExit(collider);
    }
}
