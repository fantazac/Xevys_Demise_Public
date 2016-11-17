using UnityEngine;
using System.Collections;

/// <summary>
/// Cette classe sert à détruire un objet après un certain temps.
/// </summary>
public class ObjectDestroyer : MonoBehaviour
{
    private const float BASE_DESTROY_TIME = 2f;

    private void Start()
    {
        Destroy(gameObject, BASE_DESTROY_TIME);
    }
}
