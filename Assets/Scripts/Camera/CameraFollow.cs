using UnityEngine;
using System.Collections;

// Source: https://www.youtube.com/watch?v=3qfbJ-JSrOc
public class CameraFollow : MonoBehaviour
{

    private BoxCollider2D cameraBox;
    private Transform player;
    private BoxCollider2D _boundaryHitbox;

	void Start ()
	{
	    cameraBox = GetComponent<BoxCollider2D>();
	    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _boundaryHitbox = GameObject.Find(StaticObjects.GetFindTags().Boundary).GetComponent<BoxCollider2D>();

    }

	void Update ()
	{
	    AspectRatioBoxChange();
	    FollowPlayer();
	}

    private void FollowPlayer()
    {
        if (_boundaryHitbox != null)
        {
            Vector3 bounds = new Vector3
                (Mathf.Clamp(player.position.x,
                    _boundaryHitbox.bounds.min.x + cameraBox.size.x / 2,
                    _boundaryHitbox.bounds.max.x - cameraBox.size.x / 2),

                Mathf.Clamp(player.position.y,
                    _boundaryHitbox.bounds.min.y + cameraBox.size.y / 2,
                    _boundaryHitbox.bounds.max.y - cameraBox.size.y / 2),

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
