using UnityEngine;
using System.Collections;

public class ElevateStairsTrigger : MonoBehaviour
{

    [SerializeField]
    private GameObject[] _stairsToElevate;

    [SerializeField]
    private AudioSource _audio;

    private const float ELEVATION_AMOUNT = 2.7f;
    private const float ELEVATION_SPEED = 0.05f;

    private bool _elevateStairs = false;
    private float _elevationCount = 0;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_stairsToElevate.Length > 0 && collider.gameObject.tag == "Knife")
        {
            _audio.Play();
            _elevateStairs = true;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void Update()
    {
        if (_elevateStairs)
        {
            if (_elevationCount < ELEVATION_AMOUNT)
                foreach (GameObject stair in _stairsToElevate)
                {
                    stair.transform.localScale = new Vector2(stair.transform.localScale.x, stair.transform.localScale.y + ELEVATION_SPEED);
                    stair.transform.position = new Vector2(stair.transform.position.x, stair.transform.position.y + ELEVATION_SPEED / 2);
                    _elevationCount += ELEVATION_SPEED;
                }
            else
                Destroy(gameObject);
        }
    }
}
