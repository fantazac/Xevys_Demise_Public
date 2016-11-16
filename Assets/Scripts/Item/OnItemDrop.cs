using UnityEngine;
using System.Collections;

public class OnItemDrop : MonoBehaviour
{

    [SerializeField]
    private GameObject _pickableItem;

    private const float TERMINAL_SPEED = -10;
    private const float INITIAL_SPEED = 5.5f;
    private const float ROTATE_SPEED = 420;
    private const float DISTANCE_BETWEEN_ITEMS = 0.6f;

    private Quaternion _currentAngles;

    private Vector3 _target = Vector3.zero;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Initialise(float amountToDrop, int id, Collider2D collider)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(((amountToDrop - 1) * (-DISTANCE_BETWEEN_ITEMS / 2) + (id * DISTANCE_BETWEEN_ITEMS)), 0);
        if (!EnemyMustDropItemsStraightDown(collider))
        {
            GetComponent<Rigidbody2D>().velocity += Vector2.up * INITIAL_SPEED;
        }
    }

    private bool EnemyMustDropItemsStraightDown(Collider2D collider)
    {
        return (collider.gameObject.tag == "Bat" && collider.GetComponent<BatMovement>().CloseToTop())
            || (collider.gameObject.tag == "Scarab" && collider.GetComponent<ScarabMovement>().IsNotOnTopOfPlatform());
    }

    private void Update()
    {
        if (_target == Vector3.zero && _rigidbody.velocity.y < TERMINAL_SPEED)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, TERMINAL_SPEED);
        }

        if (_target != Vector3.zero && _target == transform.position && transform.rotation.eulerAngles.z == 0)
        {
            GameObject.Instantiate(_pickableItem, transform.position, new Quaternion());
            Destroy(gameObject);
        }

        if (_target != Vector3.zero)
        {
            if (!(transform.rotation.eulerAngles.z > -ROTATE_SPEED * Time.deltaTime 
                && transform.rotation.eulerAngles.z < ROTATE_SPEED * Time.deltaTime))
            {
                transform.Rotate(Vector3.back * ROTATE_SPEED * Time.deltaTime);
            }
            else
            {
                transform.rotation = new Quaternion();
            }
            transform.position = Vector3.MoveTowards(transform.position, _target, 1 * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.back * ROTATE_SPEED * Time.deltaTime);
        }
        _currentAngles = transform.rotation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (_target == Vector3.zero && (collision.gameObject.tag == "Wall" ||
            (collision.gameObject.tag == "FlyingPlatform" && collision.transform.position.y + 0.25f < transform.position.y)))
        {
            Destroy(_rigidbody);
            _target = new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z);
            transform.rotation = _currentAngles;
        }
    }
}
