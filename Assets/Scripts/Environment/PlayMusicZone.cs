using UnityEngine;
using System.Collections;

public class PlayMusicZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            GetComponent<AudioSource>().Stop();
        }
    }
}
