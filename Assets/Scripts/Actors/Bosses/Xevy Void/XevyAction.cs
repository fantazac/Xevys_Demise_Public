using UnityEngine;
using System.Collections;

public class XevyAction: MonoBehaviour
{
    private int _sameAttackCount;
    private bool _isPlayerStillOnSameLine;

    [SerializeField]
    private GameObject _airSpike;
    [SerializeField]
    private GameObject _fireBall;
    [SerializeField]
    private GameObject _earthThorns;

    private FlipBoss _flipBoss;
    private BoxCollider2D _clawHitbox; //Check with Alex if it has changed (GameObject or BoxCollider2D)
    private PolygonCollider2D _xevyHitbox;


	private void Start()
    {
        _isPlayerStillOnSameLine = true;
        _flipBoss = GetComponent<FlipBoss>();
    }

    public void Block()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        //_clawHitbox.enabled = false;
        //_xevyHitbox.enabled = false;
    }

    public void LowerGuard()
    {
        //_xevyHitbox.enabled = true;
        GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void Heal()
    {
        GetComponent<Health>().Heal(0);
    }

    public void FireAttack(float horizontalForce, float verticalForce)
    {
        var fireBall = Instantiate(_fireBall, transform.position, transform.rotation);
        ((GameObject)fireBall).SetActive(true);
        ((GameObject)fireBall).GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalForce * 2, (verticalForce + 1) * 1.5f);
    }

    public void AirAttack()
    {
        var airSpike = Instantiate(_airSpike, transform.position, transform.rotation);
        ((GameObject)airSpike).SetActive(true);
        ((GameObject)airSpike).GetComponent<Rigidbody2D>().velocity = new Vector2(15f * _flipBoss.Orientation, 0f);
    }

    public void EarthAttack()
    {
        var earthThorns = Instantiate(_earthThorns, new Vector2(transform.position.x + 1 * _flipBoss.Orientation, transform.position.y - _earthThorns.transform.localScale.y), transform.rotation);
        ((GameObject)earthThorns).transform.localScale = ((GameObject)earthThorns).transform.localScale * _flipBoss.Orientation;
        ((GameObject)earthThorns).SetActive(true);
    }

    public void NeutralAttack()
    {
        _clawHitbox.enabled = true;
        //Flee
    }
}
