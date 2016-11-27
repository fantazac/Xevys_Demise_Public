using UnityEngine;

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
        var thunderBall = Instantiate(_thunderBall, transform.position, transform.rotation);
        ((GameObject)thunderBall).SetActive(true);
        ((GameObject)thunderBall).GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalForce * _thunderBallSpeed,
            (verticalForce * _thunderBallSpeed) + (horizontalForce / _horizontalAxisModifier ));
        return XevyAttackType.FIRE;
    }

    public XevyAttackType AirAttack()
    {
        var tornado = Instantiate(_tornado, transform.position, transform.rotation);
        ((GameObject)tornado).transform.localScale = new Vector2(_bossOrientation.Orientation * ((GameObject)tornado).transform.localScale.x,
            ((GameObject)tornado).transform.localScale.y);
        ((GameObject)tornado).SetActive(true);
        ((GameObject)tornado).GetComponent<Rigidbody2D>().velocity = new Vector2(_tornadoSpeed * _bossOrientation.Orientation, 0);
        return XevyAttackType.AIR;
    }

    public XevyAttackType EarthAttack()
    {
        var stalactites = Instantiate(_stalactites, new Vector2(transform.position.x + _bossOrientation.Orientation + _bossOrientation.Orientation,
            transform.position.y - _stalactites.transform.localScale.y), transform.rotation);
        ((GameObject)stalactites).transform.localScale = new Vector2(_bossOrientation.Orientation * ((GameObject)stalactites).transform.localScale.x,
            ((GameObject)stalactites).transform.localScale.y);
        ((GameObject)stalactites).SetActive(true);
        return XevyAttackType.EARTH;
    }

    public XevyAttackType NeutralAttack()
    {
        _clawHitbox.SetActive(true);
        return XevyAttackType.NEUTRAL;
    }
}
