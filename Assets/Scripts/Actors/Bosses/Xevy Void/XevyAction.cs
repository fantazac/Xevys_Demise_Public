using UnityEngine;
using System.Collections;

public class XevyAction: MonoBehaviour
{
    private int _sameAttackCount;

    [SerializeField]
    private GameObject _airSpike;
    [SerializeField]
    private GameObject _fireBall;
    [SerializeField]
    private GameObject _earthThorns;

    private FlipBoss _flipBoss;
    private GameObject _clawHitbox;
    private PolygonCollider2D _xevyHitbox;


	private void Start()
    {
        _xevyHitbox = GetComponent<PolygonCollider2D>();
        _flipBoss = GetComponent<FlipBoss>();
        _clawHitbox = transform.FindChild("Claw").gameObject;//.GetComponent<BoxCollider2D>();
    }

    public void Block()
    {
        GetComponent<SpriteRenderer>().color = Color.black;
        _xevyHitbox.enabled = false;
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

    public void FireAttack(float horizontalForce, float verticalForce)
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        var fireBall = Instantiate(_fireBall, transform.position, transform.rotation);
        ((GameObject)fireBall).SetActive(true);
        ((GameObject)fireBall).GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalForce * 2, (verticalForce + 1) * 1.5f);
    }

    public void AirAttack()
    {
        GetComponent<SpriteRenderer>().color = Color.grey;
        var airSpike = Instantiate(_airSpike, transform.position, transform.rotation);
        ((GameObject)airSpike).SetActive(true);
        ((GameObject)airSpike).GetComponent<Rigidbody2D>().velocity = new Vector2(15f * _flipBoss.Orientation, 0f);
    }

    public void EarthAttack()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        var earthThorns = Instantiate(_earthThorns, new Vector2(transform.position.x + _flipBoss.Orientation, transform.position.y - _earthThorns.transform.localScale.y), transform.rotation);
        ((GameObject)earthThorns).transform.localScale = new Vector2(_flipBoss.Orientation * ((GameObject)earthThorns).transform.localScale.x, ((GameObject)earthThorns).transform.localScale.y);
        ((GameObject)earthThorns).SetActive(true);
    }

    public void NeutralAttack()
    {
        GetComponent<SpriteRenderer>().color = Color.yellow;
        _clawHitbox.SetActive(true);
    }

    public void RetreatClaws()
    {
        _clawHitbox.SetActive(false);
    }
}
