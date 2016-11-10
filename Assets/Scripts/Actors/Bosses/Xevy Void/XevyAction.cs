using UnityEngine;
using System.Collections;

public class XevyAction: MonoBehaviour
{
    public enum XevyStatus
    {
        IDLE,
        VULNERABLE,
        BLOCKING,
        DEAD,
    }
    [SerializeField]
    GameObject _airSpike;
    [SerializeField]
    GameObject _fireBall;
    [SerializeField]
    GameObject _earthThorn;
    BoxCollider2D clawHitbox; //Check with Alex if it has changed (GameObject or BoxCollider2D)
    PolygonCollider2D _xevyHitbox;


	private void Start()
    {
	
	}

    private void Block()
    {

    }

    private void FireAttack()
    {

    }

    private void AirAttack()
    {

    }

    private void Heal()
    {

    }

    private void EarthAttack()
    {

    }

    private void ClawAttack()
    {

    }
}
