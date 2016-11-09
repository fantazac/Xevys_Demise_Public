using UnityEngine;
using System.Collections;

public class KnockbackOnDamageTaken : MonoBehaviour
{
    private const float KNOCKBACK_SPEED = 5;
    private const float TIME_DAMAGE_ANIMATION_PLAYS = 0.5f;

    private Animator _anim;

    private void Start()
    {
        _anim = StaticObjects.GetPlayer().GetComponentInChildren<Animator>();
    }

    public void KnockbackPlayer(Vector2 positionEnemy)
    {
        _anim.SetBool("IsDamaged", true);
        StartCoroutine("DamageAnimationCoroutine");
        GetComponent<PlayerGroundMovement>().IsKnockedBack = true;

        if (transform.position.x < positionEnemy.x)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-KNOCKBACK_SPEED, GetComponent<Rigidbody2D>().velocity.y);
        }
        else if (transform.position.x > positionEnemy.x)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(KNOCKBACK_SPEED, GetComponent<Rigidbody2D>().velocity.y);
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, KNOCKBACK_SPEED);
    }

    private IEnumerator DamageAnimationCoroutine()
    {
        float counter = 0;
        while (counter < TIME_DAMAGE_ANIMATION_PLAYS)
        {
            counter += Time.deltaTime;
            yield return null;
        }
        _anim.SetBool("IsDamaged", false);
    }
}
