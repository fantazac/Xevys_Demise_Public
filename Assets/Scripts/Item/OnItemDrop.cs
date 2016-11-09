using UnityEngine;
using System.Collections;

public class OnItemDrop : MonoBehaviour
{

    [SerializeField]
    private GameObject _pickableItem;

    private const float TERMINAL_SPEED = -10;
    private const float INITIAL_SPEED = 5.5f;
    private const float ROTATE_SPEED = 10;
    private const float DISTANCE_BETWEEN_ITEMS = 0.6f;

    private Vector3 _target = Vector3.zero;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Initialise(float amountToDrop, int id, Collider2D collider)
    {
        if ((collider.gameObject.tag == "Bat" && collider.GetComponent<BatMovement>().CloseToTop()) || (collider.gameObject.tag == "Scarab" && collider.GetComponent<ScarabMovementWithPoints>().OnBottomOfPlatform()))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(((amountToDrop - 1) * (-DISTANCE_BETWEEN_ITEMS / 2) + (id * DISTANCE_BETWEEN_ITEMS)), 0);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(((amountToDrop - 1) * (-DISTANCE_BETWEEN_ITEMS / 2) + (id * DISTANCE_BETWEEN_ITEMS)), INITIAL_SPEED);
        }
    }

    private void Update()
    {
        if (_target == Vector3.zero && _rigidbody.velocity.y < TERMINAL_SPEED)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, TERMINAL_SPEED);
        }

        if (_target != Vector3.zero && _target == transform.position)
        {
            GameObject.Instantiate(_pickableItem, transform.position, new Quaternion());
            Destroy(gameObject);
        }

        if (_target != Vector3.zero)
        {
            if (!(transform.rotation.eulerAngles.z > -1 && transform.rotation.eulerAngles.z < 1))
            {
                transform.Rotate(Vector3.back * ROTATE_SPEED);
            }
            else if (transform.rotation.eulerAngles.z != 0)
            {
                transform.rotation = new Quaternion();
            }
            transform.position = Vector3.MoveTowards(transform.position, _target, 1 * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.back * ROTATE_SPEED);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_target == Vector3.zero && (collider.gameObject.tag == "Wall" ||
            (collider.gameObject.tag == "FlyingPlatform" && _rigidbody.velocity.y < -2)))
        {
            _target = new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z);
            Destroy(_rigidbody);
        }
    }
}
