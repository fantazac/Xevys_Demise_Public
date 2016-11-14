using UnityEngine;
using System.Collections;

public class XevyPlayerInteraction : MonoBehaviour
{

    public delegate void OnBossFlippedHandler();
    public event OnBossFlippedHandler OnBossFlipped;
    FlipBoss _flipBoss;

    public bool IsFocusedOnPlayer { get; set; }

    private void Start()
    {
        IsFocusedOnPlayer = true;
        _flipBoss = GetComponent<FlipBoss>();
    }

    private void Update()
    {
        if (IsFocusedOnPlayer)
        {
            if (_flipBoss.FlipTowardsPlayer())
            {
                OnBossFlipped();
            }
        }
    }

    public bool CheckPlayerDistance()
    {
        return (Vector2.Distance(StaticObjects.GetPlayer().transform.position, transform.position) <= 5);
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
        return (Mathf.Abs(transform.position.y - StaticObjects.GetPlayer().transform.position.y) < 2.5f);
    }
}
