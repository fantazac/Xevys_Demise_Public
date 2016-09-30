using UnityEngine;
using System.Collections;
using System.ComponentModel;

public class ActorThrowAttack : MonoBehaviour
{

    [SerializeField]
    private GameObject _knife;
    [SerializeField]
    private GameObject _axe;
    [SerializeField]
    private float WEAPON_SPAWN_DISTANCE_FROM_PLAYER = 0.7f;
    [SerializeField]
    private float AXE_THROWING_HEIGHT = 1f;
    [SerializeField]
    private float KNIFE_SPEED = 9f;
    [SerializeField]
    private float AXE_SPEED = 6f;
    [SerializeField]
    private float AXE_THROWING_ANGLE = 14.5f;
    [SerializeField]
    private float AXE_INITIAL_ANGLE = 90f;

    private InputManager _inputManager;
    private AudioSource[] _audioSources;

    void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _inputManager.OnThrowAttack += OnKnifeAttack;
        _inputManager.OnThrowAttack += OnAxeAttack;

        _audioSources = GetComponents<AudioSource>();
    }

    void OnKnifeAttack()
    {
        if (!GameObject.Find("Knife(Clone)"))
        {
            _audioSources[1].Play();
            GameObject newKnife;

            if (GetComponent<PlayerMovement>().FacingRight)
            {
                newKnife = (GameObject)Instantiate(_knife, new Vector3(transform.position.x + WEAPON_SPAWN_DISTANCE_FROM_PLAYER, transform.position.y, transform.position.z), transform.rotation);
                newKnife.GetComponent<Rigidbody2D>().velocity = new Vector2(KNIFE_SPEED, 0);
            }
            else
            {
                newKnife = (GameObject)Instantiate(_knife, new Vector3(transform.position.x - WEAPON_SPAWN_DISTANCE_FROM_PLAYER, transform.position.y, transform.position.z), transform.rotation);
                newKnife.GetComponent<SpriteRenderer>().flipX = true;
                newKnife.GetComponent<Rigidbody2D>().velocity = new Vector2(-KNIFE_SPEED, 0);
            }
        }
    }

    void OnAxeAttack()
    {
        if (!GameObject.Find("Axe(Clone)"))
        {
            _audioSources[2].Play();
            GameObject newAxe;

            if (GetComponent<PlayerMovement>().FacingRight)
            {
                newAxe = (GameObject)Instantiate(_axe, new Vector3(transform.position.x, transform.position.y + AXE_THROWING_HEIGHT, transform.position.z), transform.rotation);
                newAxe.GetComponent<Rigidbody2D>().rotation = AXE_INITIAL_ANGLE;
                newAxe.GetComponent<Rigidbody2D>().velocity = new Vector2(AXE_SPEED, AXE_THROWING_ANGLE);
            }
            else
            {
                newAxe = (GameObject)Instantiate(_axe, new Vector3(transform.position.x, transform.position.y + AXE_THROWING_HEIGHT, transform.position.z), transform.rotation);
                newAxe.GetComponent<Rigidbody2D>().rotation = AXE_INITIAL_ANGLE;
                newAxe.GetComponent<SpriteRenderer>().flipY = true;
                newAxe.GetComponent<Rigidbody2D>().velocity = new Vector2(-AXE_SPEED, AXE_THROWING_ANGLE);
            }
        }
    }
}
