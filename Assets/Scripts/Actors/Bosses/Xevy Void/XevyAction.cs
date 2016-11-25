using UnityEngine;
using System.Collections;

public class XevyAction: MonoBehaviour
{
    private int _sameAttackCount;

    private const float TORNADO_SPEED = 15;
    private const float HORIZONTAL_AXIS_MODIFIER = 3.5f;
    private const float THUNDER_BALL_SPEED = 2;

    [SerializeField]
    private GameObject _tornado;
    [SerializeField]
    private GameObject _thunderBall;
    [SerializeField]
    private GameObject _stalactites;

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
        var fireBall = Instantiate(_thunderBall, transform.position, transform.rotation);
        ((GameObject)fireBall).SetActive(true);
        ((GameObject)fireBall).GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalForce * THUNDER_BALL_SPEED, (verticalForce * THUNDER_BALL_SPEED) + (horizontalForce / HORIZONTAL_AXIS_MODIFIER ));
        return XevyAttackType.FIRE;
    }

    public XevyAttackType AirAttack()
    {
        var airSpike = Instantiate(_tornado, transform.position, transform.rotation);
        ((GameObject)airSpike).SetActive(true);
        ((GameObject)airSpike).GetComponent<Rigidbody2D>().velocity = new Vector2(TORNADO_SPEED * _bossOrientation.Orientation, 0);
        return XevyAttackType.AIR;
    }

    public XevyAttackType EarthAttack()
    {
        var earthThorns = Instantiate(_stalactites, new Vector2(transform.position.x + _bossOrientation.Orientation + _bossOrientation.Orientation, transform.position.y - _stalactites.transform.localScale.y), transform.rotation);
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
