using UnityEngine;
using System.Collections;

public class OnBehemothAttackHit : OnAttackHit
{
    private BehemothAI _behemoth;

    public delegate void OnKnockbackHandler(Vector2 behemothPosition);
    public event OnKnockbackHandler OnKnockback;

    protected override void Start()
    {
        _behemoth = GetComponent<BehemothAI>();

        StaticObjects.GetPlayer().GetComponent<KnockbackPlayer>().EnableBehemothKnockback(gameObject);
        enabled = false;
        base.Start();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (CanHitEntity(collider) && CanAttackPlayer(collider))
        {
            OnKnockback(transform.position);
            if (enabled)
            {
                _behemoth.SetChargeStatus();
                _playerHealth.Hit(_baseDamage);
            }
        }
    }

    protected override void OnTriggerStay2D(Collider2D collider)
    {
        if (CanHitEntity(collider))
        {
            OnKnockback(transform.position);
        }
    }
}
