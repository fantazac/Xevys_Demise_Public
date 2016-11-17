using UnityEngine;
using System.Collections;

public class XevyAction: MonoBehaviour
{
    private int _sameAttackCount;

    private const float AIR_SPIKE_SPEED = 15;
    private const float HORIZONTAL_AXIS_MODIFIER = 3.5f;
    private const float FIRE_BALL_SPEED = 2;

    [SerializeField]
    private GameObject _airSpike;
    [SerializeField]
    private GameObject _fireBall;
    [SerializeField]
    private GameObject _earthThorns;

    private BossOrientation _bossOrientation;
    private GameObject _clawHitbox;
    private PolygonCollider2D _xevyHitbox;

    public enum XevyAttackType
    {
        NONE,
        AIR,
        FIRE,
        EARTH,
        NEUTRAL,
    }

    private void Start()
    {
        _xevyHitbox = GetComponent<PolygonCollider2D>();
        _bossOrientation = GetComponent<BossOrientation>();
        _clawHitbox = transform.FindChild("Claw").gameObject;
    }

    public void LowerGuard()
    {
        _xevyHitbox.enabled = true;
    }

    public void RetreatClaws()
    {
        _clawHitbox.SetActive(false);
    }

    public void Heal()
    {
        //GetComponent<Health>().Heal(0);
    }

    public XevyAttackType Block()
    {
        _xevyHitbox.enabled = false;
        return XevyAttackType.NONE;
    }

    public XevyAttackType FireAttack(float horizontalForce, float verticalForce)
    {
        var fireBall = Instantiate(_fireBall, transform.position, transform.rotation);
        ((GameObject)fireBall).SetActive(true);
        ((GameObject)fireBall).GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalForce * FIRE_BALL_SPEED, (verticalForce * FIRE_BALL_SPEED) + (horizontalForce / HORIZONTAL_AXIS_MODIFIER ));
        return XevyAttackType.FIRE;
    }

    public XevyAttackType AirAttack()
    {
        var airSpike = Instantiate(_airSpike, transform.position, transform.rotation);
        ((GameObject)airSpike).SetActive(true);
        ((GameObject)airSpike).GetComponent<Rigidbody2D>().velocity = new Vector2(AIR_SPIKE_SPEED * _bossOrientation.Orientation, 0);
        return XevyAttackType.AIR;
    }

    public XevyAttackType EarthAttack()
    {
        var earthThorns = Instantiate(_earthThorns, new Vector2(transform.position.x + _bossOrientation.Orientation, transform.position.y - _earthThorns.transform.localScale.y), transform.rotation);
        ((GameObject)earthThorns).transform.localScale = new Vector2(_bossOrientation.Orientation * ((GameObject)earthThorns).transform.localScale.x, ((GameObject)earthThorns).transform.localScale.y);
        ((GameObject)earthThorns).SetActive(true);
        return XevyAttackType.EARTH;
    }

    public XevyAttackType NeutralAttack()
    {
        _clawHitbox.SetActive(true);
        return XevyAttackType.NEUTRAL;
    }
}
