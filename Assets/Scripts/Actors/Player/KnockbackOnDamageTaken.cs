using UnityEngine;
using System.Collections;

public class KnockbackOnDamageTaken : MonoBehaviour
{

    public void KnockbackPlayer(Vector2 positionEnemy)
    {
        if(transform.position.x < positionEnemy.x)
        {
            //Debug.Log(1);
            GetComponent<Rigidbody2D>().velocity = new Vector2(10, GetComponent<Rigidbody2D>().velocity.y);
        }
        else if(transform.position.x > positionEnemy.x)
        {
            //Debug.Log(2);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-10, GetComponent<Rigidbody2D>().velocity.y);
        }

        if (GetComponent<PlayerMovement>().IsJumping())
        {
            //Debug.Log(3);
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, GetComponent<PlayerMovement>().TerminalSpeed);
        }
    }

}
