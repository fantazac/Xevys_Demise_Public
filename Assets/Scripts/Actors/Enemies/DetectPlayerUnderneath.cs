using UnityEngine;
using System.Collections;

public class DetectPlayerUnderneath : MonoBehaviour
{

    private Rigidbody2D _rigidbody;

    // Use this for initialization
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 2);
        }

    }
}
