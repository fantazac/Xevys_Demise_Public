using UnityEngine;
using System.Collections;
using System.ComponentModel;

public class ActorKnifeAttack : MonoBehaviour
{

    [SerializeField]
    private GameObject _knife;

    private InputManager _inputManager;

    private const float KNIFE_SPAWN_DISTANCE_FROM_PLAYER = 0.7f;
    private const float KNIFE_SPEED = 9f;

    void Start()
    {
        _inputManager = GetComponent<InputManager>();

        _inputManager.OnKnifeAttack += OnKnifeAttack;
    }

    void OnKnifeAttack()
    {
        GameObject newKnife;

        if (GetComponent<PlayerMovement>().FacingRight)
        {
            newKnife = (GameObject)Instantiate(_knife, new Vector3(transform.position.x + KNIFE_SPAWN_DISTANCE_FROM_PLAYER, transform.position.y, transform.position.z), transform.rotation);
            newKnife.GetComponent<Rigidbody2D>().velocity = new Vector2(KNIFE_SPEED, 0);
        }
        else
        {
            newKnife = (GameObject)Instantiate(_knife, new Vector3(transform.position.x - KNIFE_SPAWN_DISTANCE_FROM_PLAYER, transform.position.y, transform.position.z), transform.rotation);
            newKnife.GetComponent<SpriteRenderer>().flipX = true;
            newKnife.GetComponent<Rigidbody2D>().velocity = new Vector2(-KNIFE_SPEED, 0);
        }
    }
}
