using UnityEngine;
using System.Collections;

/// <summary>
/// Cette classe fait apparaître des balles qui tombent dans l'eau.
/// </summary>
public class ObjectSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject _ball;

    [SerializeField]
    private int SPAWNER_CD = 50;
    private int _spawnerCoolDown;

    private void Start()
    {
        _spawnerCoolDown = SPAWNER_CD/2;
    }

    private void Update ()
    {
        _spawnerCoolDown++;

        if (_spawnerCoolDown >= SPAWNER_CD)
        {
            GameObject newBall;

            newBall = (GameObject)Instantiate(_ball, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);

            _spawnerCoolDown = 0;
        }
    }
}
