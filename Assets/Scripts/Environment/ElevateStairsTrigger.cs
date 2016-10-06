using UnityEngine;
using System.Collections;

public class ElevateStairsTrigger : MonoBehaviour
{

    [SerializeField]
    private GameObject[] _stairsToElevate;

    private const float ELEVATION_AMOUNT = 2.7f;
    private const float ELEVATION_SPEED = 0.05f;
    private const int MAIN_CAMERA_BASE_KNIFE_ITEM_AREA = 3;
    private const int BASE_KNIFE_AMOUNT_ON_PICKUP = 10;

    private bool _soundPlayed;

    private bool _elevateStairs = false;
    private float _elevationCount = 0;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_stairsToElevate.Length > 0 && collider.gameObject.tag == "Knife")
        {
            GetComponent<AudioSource>().Play();
            _soundPlayed = true;
            gameObject.transform.position = new Vector3(-1000, -1000, 0);
            _elevateStairs = true;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void Update()
    {
        if (_elevateStairs)
        {
            if (_elevationCount < ELEVATION_AMOUNT)
            {
                foreach (GameObject stair in _stairsToElevate)
                {
                    stair.transform.localScale = new Vector2(stair.transform.localScale.x, stair.transform.localScale.y + ELEVATION_SPEED);
                    stair.transform.position = new Vector3(stair.transform.position.x, stair.transform.position.y + ELEVATION_SPEED / 2, stair.transform.position.z);
                    _elevationCount += ELEVATION_SPEED;
                }
            }
            else
            {
                if (_soundPlayed && !GetComponent<AudioSource>().isPlaying)
                {
                    Destroy(gameObject);
                }
            }
        }
        else if (GameObject.Find("Main Camera").GetComponent<CameraManager>().CurrentArea == MAIN_CAMERA_BASE_KNIFE_ITEM_AREA && GameObject.Find("Character").GetComponent<PlayerThrowingWeaponsMunitions>().KnifeMunition < 1 && !GameObject.Find("Base Knife Item"))
        {
            GameObject.Find("Character").GetComponent<PlayerThrowingWeaponsMunitions>().KnifeMunition += BASE_KNIFE_AMOUNT_ON_PICKUP;
        }
    }
}
