//Source : https://github.com/tutsplus/unity-2d-water-effect/
//Ce code est basé sur cette source, mais je suis entrain de l'adapter pour nous. Il est sujet à changer.

using UnityEngine;

namespace Assets.Scripts
{
    public class WaterDetector : MonoBehaviour
    {
        private const float DEFAULT_DAMPING_REDUCTION = 60f;

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.GetComponent<Rigidbody2D>() != null)
            {
                transform.parent.GetComponent<Water>().Splash(transform.position.x, collider.GetComponent<Rigidbody2D>().velocity.y * collider.GetComponent<Rigidbody2D>().mass / (DEFAULT_DAMPING_REDUCTION * 2));
            }
        }
    }
}
