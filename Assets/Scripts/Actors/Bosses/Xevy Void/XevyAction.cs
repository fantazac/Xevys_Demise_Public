using UnityEngine;
using System.Collections;

public class XevyAction: MonoBehaviour
{
    private int _sameAttackCount;

    private const float HORIZONTAL_AXIS_MODIFIER = 3.5f;
    private const float FORCE_BASIC_MODIFIER = 2;

    [SerializeField]
    private GameObject _airSpike;
    [SerializeField]
    private GameObject _fireBall;
    [SerializeField]
    private GameObject _earthThorns;

    private FlipBoss _flipBoss;
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
        _flipBoss = GetComponent<FlipBoss>();
        _clawHitbox = transform.FindChild("Claw").gameObject;//.GetComponent<BoxCollider2D>();
    }

    public XevyAttackType Block()
    {
        GetComponent<SpriteRenderer>().color = Color.black;
        _xevyHitbox.enabled = false;
        return XevyAttackType.NONE;
    }

    public void LowerGuard()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        _xevyHitbox.enabled = true;
    }

    public void Heal()
    {
        GetComponent<SpriteRenderer>().color = Color.blue;
        //GetComponent<Health>().Heal(0);
    }

    public XevyAttackType FireAttack(float horizontalForce, float verticalForce)
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        var fireBall = Instantiate(_fireBall, transform.position, transform.rotation);
        ((GameObject)fireBall).SetActive(true);
        ((GameObject)fireBall).GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalForce * FORCE_BASIC_MODIFIER, (verticalForce * FORCE_BASIC_MODIFIER) + (horizontalForce / HORIZONTAL_AXIS_MODIFIER ));
        return XevyAttackType.FIRE;
    }

    public XevyAttackType AirAttack()
    {
        GetComponent<SpriteRenderer>().color = Color.grey;
        var airSpike = Instantiate(_airSpike, transform.position, transform.rotation);
        ((GameObject)airSpike).SetActive(true);
        ((GameObject)airSpike).GetComponent<Rigidbody2D>().velocity = new Vector2(15f * _flipBoss.Orientation, 0f);
        return XevyAttackType.AIR;
    }

    public XevyAttackType EarthAttack()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        var earthThorns = Instantiate(_earthThorns, new Vector2(transform.position.x + _flipBoss.Orientation, transform.position.y - _earthThorns.transform.localScale.y), transform.rotation);
        ((GameObject)earthThorns).transform.localScale = new Vector2(_flipBoss.Orientation * ((GameObject)earthThorns).transform.localScale.x, ((GameObject)earthThorns).transform.localScale.y);
        ((GameObject)earthThorns).SetActive(true);
        return XevyAttackType.EARTH;
    }

    public XevyAttackType NeutralAttack()
    {
        GetComponent<SpriteRenderer>().color = Color.yellow;
        _clawHitbox.SetActive(true);
        return XevyAttackType.NEUTRAL;
    }

    public void RetreatClaws()
    {
        _clawHitbox.SetActive(false);
    }
}
