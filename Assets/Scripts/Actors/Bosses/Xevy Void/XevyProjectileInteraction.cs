using UnityEngine;
using System.Collections.Generic;

public class XevyProjectileInteraction : MonoBehaviour
{
    List<GameObject> _knives;
    List<GameObject> _axes;
    ActorThrowAttack _throwAttack;

    //Implement timer for each item to make some sort of reaction time

	private void Start()
    {
        _knives = new List<GameObject>();
        _axes = new List<GameObject>();
        _throwAttack = StaticObjects.GetPlayer().GetComponent<ActorThrowAttack>();
        _throwAttack.OnKnifeThrown += OnKnifeThrown;
        _throwAttack.OnAxeThrown += OnAxeThrown;
	}

    public bool CheckKnivesDistance()
    {
        foreach (GameObject knife in _knives)
        {
            if (Vector2.Distance(knife.transform.position, transform.position) < 0 && Mathf.Abs(transform.position.x - knife.transform.position.x) < 0)
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckAxesDistance()
    {
        foreach (GameObject axe in _axes)
        {
            if (Vector2.Distance(axe.transform.position, transform.position) < 100 && Mathf.Abs(transform.position.y - axe.transform.position.y) < 100)
            {
                return true;
            }
        }
        return false;
    }

    public void OnKnifeThrown(GameObject knife)
    {
        _knives.Add(knife);
        knife.GetComponent<DestroyProjectile>().OnProjectileDestroyed += OnKnifeDestroyed;
    }

    public void OnAxeThrown(GameObject axe)
    {
        _axes.Add(axe);
        axe.GetComponent<DestroyProjectile>().OnProjectileDestroyed += OnAxeDestroyed;
    }

    public void OnKnifeDestroyed(GameObject knife)
    {
        knife.GetComponent<DestroyProjectile>().OnProjectileDestroyed -= OnKnifeDestroyed;
        _knives.Remove(knife);
    }

    public void OnAxeDestroyed(GameObject axe)
    {
        _axes.Remove(axe);
        axe.GetComponent<DestroyProjectile>().OnProjectileDestroyed -= OnAxeDestroyed;
    }
}
