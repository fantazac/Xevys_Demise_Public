using UnityEngine;
using System.Collections;

public class XevyAction: MonoBehaviour
{
    private int _sameAttackCount;

    [SerializeField]
    private GameObject _tornado;

    [SerializeField]
    private GameObject _thunderBall;

    [SerializeField]
    private GameObject _stalactites;

    [SerializeField]
    private float _tornadoSpeed = 15;

    [SerializeField]
    private float _horizontalAxisModifier = 3.5f;

    [SerializeField]
    private float _thunderBallSpeed = 2;

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
        GetComponent<Health>().Heal(2);
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
        ((GameObject)fireBall).GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalForce * _thunderBallSpeed, (verticalForce * _thunderBallSpeed) + (horizontalForce / _horizontalAxisModifier ));
        return XevyAttackType.FIRE;
    }

    public XevyAttackType AirAttack()
    {
        var airSpike = Instantiate(_tornado, transform.position, transform.rotation);
        ((GameObject)airSpike).SetActive(true);
        ((GameObject)airSpike).GetComponent<Rigidbody2D>().velocity = new Vector2(_tornadoSpeed * _bossOrientation.Orientation, 0);
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
