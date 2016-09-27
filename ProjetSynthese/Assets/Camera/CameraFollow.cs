using UnityEngine;
using System.Collections;

// Source: https://www.youtube.com/watch?v=3qfbJ-JSrOc
public class CameraFollow : MonoBehaviour
{

    private BoxCollider2D cameraBox;
    private Transform player;

	void Start ()
	{
	    cameraBox = GetComponent<BoxCollider2D>();
	    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

	void Update ()
	{
	    AspectRatioBoxChange();
	    FollowPlayer();
	}

    private void FollowPlayer()
    {
        if (GameObject.Find("Boundary"))
        {
            Vector3 bounds = new Vector3
                (Mathf.Clamp(player.position.x,
                    GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.min.x + cameraBox.size.x / 2,
                    GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.max.x - cameraBox.size.x / 2),

                Mathf.Clamp(player.position.y,
                    GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.min.y + cameraBox.size.y / 2,
                    GameObject.Find("Boundary").GetComponent<BoxCollider2D>().bounds.max.y - cameraBox.size.y / 2),

                transform.position.z);

            transform.position = bounds;
        }
    }

    private void AspectRatioBoxChange()
    {
        //3.2
        if (Camera.main.aspect >= (1.5f) && Camera.main.aspect < 1.6f)
        {
            cameraBox.size = new Vector2(15.1f, 10f);
        }

        // On peut ajouter des conditions ici pour supporter divers aspect ratio
    }
}
