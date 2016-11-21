using UnityEngine;
using System.Linq;

public class DestroyBossProjectile : MonoBehaviour
{
    [SerializeField]
    private string[] _tags;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_tags.Contains(collider.gameObject.tag))
        {
            Destroy(gameObject);
        }
    }
}
