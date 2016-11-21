using UnityEngine;
using System.Collections;

public class XevyPlayerInteraction : MonoBehaviour
{
    private const float PLAYER_DETECTION_DISTANCE = 5;
    private const float PLAYER_ALIGNMENT_MARGIN = 2.5f;

    BossOrientation _bossOrientation;

    public bool IsFocusedOnPlayer { get; set; }

    private void Start()
    {
        IsFocusedOnPlayer = true;
        _bossOrientation = GetComponent<BossOrientation>();
    }

    public void UpdatePlayerInteraction()
    {
        if (IsFocusedOnPlayer)
        {
            _bossOrientation.FlipTowardsPlayer();
        }
    }

    public bool CheckPlayerDistance()
    {
        return (Vector2.Distance(StaticObjects.GetPlayer().transform.position, transform.position) <= PLAYER_DETECTION_DISTANCE);
    }

    public float GetPlayerHorizontalDistance()
    {
        return StaticObjects.GetPlayer().transform.position.x - transform.position.x;
    }

    public float GetPlayerVerticalDistance()
    {
        return StaticObjects.GetPlayer().transform.position.y - transform.position.y;

    }
    public bool CheckAlignmentWithPlayer()
    {
        return (Mathf.Abs(transform.position.y - StaticObjects.GetPlayer().transform.position.y) < PLAYER_ALIGNMENT_MARGIN);
    }
}
