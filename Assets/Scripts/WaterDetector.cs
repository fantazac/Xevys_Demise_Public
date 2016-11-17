//Source du code : https://github.com/tutsplus/unity-2d-water-effect/

using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Cette classe gère la détection des objets qui entre[nt] dans l'eau.
    /// </summary>
    public class WaterDetector : MonoBehaviour
    {
        private const float DEFAULT_DAMPING_REDUCTION = 120f;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.GetComponent<Rigidbody2D>() != null)
            {
                transform.parent.GetComponent<Water>().Splash(transform.position.x, collider.GetComponent<Rigidbody2D>().velocity.y * collider.GetComponent<Rigidbody2D>().mass / DEFAULT_DAMPING_REDUCTION);
            }
        }
    }
}
