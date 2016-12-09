using UnityEngine;
using System.Collections;

public class OnItemDrop : MonoBehaviour
{
    private const float TERMINAL_SPEED = -10;
    private const float INITIAL_SPEED = 5.5f;
    private const float ROTATE_SPEED = 420;
    private const float DISTANCE_BETWEEN_ITEMS = 0.6f;
    private const float FLYING_PLATFORM_MARGIN = 0.15f;

    private Quaternion _currentAngles;

    private Vector3 _target = Vector3.zero;

    private Rigidbody2D _rigidbody;

    private int _itemId = 0;
    private int _amountofItemsDropped = 0;
    private Collider2D _enemyKilled;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _rigidbody.velocity = new Vector2(((_amountofItemsDropped - 1) * (-DISTANCE_BETWEEN_ITEMS / 2) +
            (_itemId * DISTANCE_BETWEEN_ITEMS)), 0);
        if (!EnemyMustDropItemsStraightDown(_enemyKilled))
        {
            _rigidbody.velocity += Vector2.up * INITIAL_SPEED;
        }

        StartCoroutine(DropToTheGround());
    }

    public void Initialise(int amountToDrop, int id, Collider2D collider)
    {
        _amountofItemsDropped = amountToDrop;
        _itemId = id;
        _enemyKilled = collider;
    }

    private bool EnemyMustDropItemsStraightDown(Collider2D collider)
    {
        return (collider.gameObject.tag == StaticObjects.GetObjectTags().Bat && collider.GetComponent<BatMovement>().IsCloseToTop())
            || (collider.gameObject.tag == StaticObjects.GetObjectTags().Scarab && collider.GetComponent<ScarabMovement>().IsNotOnTopOfPlatform());
    }

    private IEnumerator DropToTheGround()
    {
        while(_target == Vector3.zero)
        {
            transform.Rotate(Vector3.back * ROTATE_SPEED * Time.deltaTime);
            _currentAngles = transform.rotation;

            if (_rigidbody.velocity.y < TERMINAL_SPEED)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, TERMINAL_SPEED);
            }

            yield return null;
        }

        while (true)
        {
            if (_target == transform.position && transform.rotation.eulerAngles.z == 0)
            {
                _animator.enabled = true;
                enabled = false;
            }
            else
            {
                if (!RotationIsCloseToZero())
                {
                    transform.Rotate(Vector3.back * ROTATE_SPEED * Time.deltaTime);
                }
                else if (transform.rotation.eulerAngles.z != 0)
                {
                    transform.rotation = new Quaternion();
                }
                transform.position = Vector3.MoveTowards(transform.position, _target, 1 * Time.deltaTime);
            }

            yield return null;
        }
    }

    private bool RotationIsCloseToZero()
    {
        return transform.rotation.eulerAngles.z > -ROTATE_SPEED * Time.deltaTime
                    && transform.rotation.eulerAngles.z < ROTATE_SPEED * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_target == Vector3.zero && ((collider.gameObject.tag == StaticObjects.GetObjectTags().Wall && transform.position.y > collider.transform.position.y) || collider.gameObject.tag == StaticObjects.GetObjectTags().Spike ||
            (collider.gameObject.tag == StaticObjects.GetObjectTags().FlyingPlatform && collider.transform.position.y + FLYING_PLATFORM_MARGIN < transform.position.y)))
        {
            Destroy(_rigidbody);
            _target = new Vector3(transform.position.x, transform.position.y + DISTANCE_BETWEEN_ITEMS, transform.position.z);
            transform.rotation = _currentAngles;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
