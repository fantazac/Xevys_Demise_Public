using UnityEngine;
using System.Collections;

public class MultipleTriggerDoor : MonoBehaviour
{

    [SerializeField]
    private GameObject _door;

    [SerializeField]
    private GameObject[] _wallsToActivate;

    [SerializeField]
    private GameObject[] _triggers;

    private bool _soundPlayed = false;
    private int _aliveTriggers = 0;

    void FixedUpdate()
    {
        _aliveTriggers = 0;

        foreach(GameObject trigger in _triggers)
        {
            if(trigger != null)
            {
                _aliveTriggers++;
            }
        }

        if(_aliveTriggers == 0 && !_soundPlayed)
        {
            foreach (GameObject wall in _wallsToActivate)
            {
                wall.GetComponent<BoxCollider2D>().enabled = true;
            }

            GetComponent<AudioSource>().Play();
            _soundPlayed = true;

            _door.GetComponent<RetractDoor>().Retract = true;
        }

        if (_soundPlayed && !GetComponent<AudioSource>().isPlaying)
        {
            Destroy(gameObject);
        }
    }

}
