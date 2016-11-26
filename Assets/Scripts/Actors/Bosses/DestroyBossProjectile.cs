using UnityEngine;
using System.Linq;

public class DestroyBossProjectile : MonoBehaviour
{
    [SerializeField]
    private string[] _tags;

    [SerializeField]
    private GameObject[] _excludedObjects;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_excludedObjects.Contains(collider.gameObject))
        {
            return;
        }
        else if (_tags.Contains(collider.gameObject.tag))
        {
            Destroy(gameObject);
        }
    }
}
