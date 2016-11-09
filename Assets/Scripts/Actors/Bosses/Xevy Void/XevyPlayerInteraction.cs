using UnityEngine;
using System.Collections;

public class XevyPlayerInteraction : MonoBehaviour
{
    FlipBoss _flipBoss;

    public bool IsFocusedOnPlayer { get; set;}

    private void Start()
    {
        IsFocusedOnPlayer = true;
        _flipBoss = GetComponent<FlipBoss>();
	}

    private void Update()
    {
        if (IsFocusedOnPlayer)
        {
            _flipBoss.FlipTowardsPlayer();
        }
    }

    public bool CheckPlayerProximity()
    {
        return (Vector2.Distance(StaticObjects.GetPlayer().transform.position, transform.position) < 3);
    }

    public bool CheckAlignmentWithPlayer()
    {
        return (Mathf.Abs(transform.position.y - StaticObjects.GetPlayer().transform.position.y) < 2.5f);
    }
}
