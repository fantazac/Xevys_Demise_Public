using UnityEngine;
using System.Linq;

public class DestroyBossProjectile : MonoBehaviour
{
    [SerializeField]
    private string[] _tags;

    public GameObject[] ExcludedObjects { get; set; }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (ExcludedObjects.Contains(collider.gameObject))
        {
            return;
        }
        else if (_tags.Contains(collider.gameObject.tag))
        {
            Destroy(gameObject);
        }
    }
}
