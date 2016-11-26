using UnityEngine;
using System.Collections;

public class XevyPlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private float _playerDetectionDistance = 5;

    [SerializeField]
    private float _playerAlignmentVerticalMargin = 2.5f;

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
        return (Vector2.Distance(StaticObjects.GetPlayer().transform.position, transform.position) <= _playerDetectionDistance);
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
        return (Mathf.Abs(transform.position.y - StaticObjects.GetPlayer().transform.position.y) < _playerAlignmentVerticalMargin);
    }
}
