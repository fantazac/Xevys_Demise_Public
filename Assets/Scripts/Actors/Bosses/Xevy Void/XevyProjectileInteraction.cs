using UnityEngine;
using System.Collections.Generic;

public class XevyProjectileInteraction : MonoBehaviour
{
    Dictionary<GameObject, float> _knivesDictionary;
    Dictionary<GameObject, float> _axesDictionary;
    ActorThrowAttack _throwAttack;

	private void Start()
    {
        _knivesDictionary = new Dictionary<GameObject, float>();
        _axesDictionary = new Dictionary<GameObject, float>();
        _throwAttack = StaticObjects.GetPlayer().GetComponent<ActorThrowAttack>();
        _throwAttack.OnKnifeThrown += OnKnifeThrown;
        _throwAttack.OnAxeThrown += OnAxeThrown;
	}

    private void Destroy()
    {
        _throwAttack.OnKnifeThrown -= OnKnifeThrown;
        _throwAttack.OnAxeThrown -= OnAxeThrown;
    }

    public bool CheckKnivesDistance()
    {
        foreach (KeyValuePair<GameObject, float> knife in _knivesDictionary)
        {
            if (knife.Value >= 1.0f)
            {
                //Mod this
                if (Vector2.Distance(knife.Key.transform.position, transform.position) < 2 && Mathf.Abs(knife.Key.transform.position.y - transform.position.y) < 3)
                {
                    return true;
                }
            }
            else
            {
                _knivesDictionary[knife.Key] += Time.fixedDeltaTime;
            }
        }
        return false;
    }

    public bool CheckAxesDistance()
    {
        foreach (KeyValuePair<GameObject, float> axe in _axesDictionary)
        {
            if (axe.Value >= 1.0f)
            {
                if (Vector2.Distance(axe.Key.transform.position, transform.position) < 3 && axe.Key.transform.position.y - transform.position.y > 0)
                {
                    return true;
                }
            }
            else
            {
                _axesDictionary[axe.Key] += Time.fixedDeltaTime;
            }
        }
        return false;
    }

    public void OnKnifeThrown(GameObject knife)
    {
        _knivesDictionary.Add(knife, 0.0f);
        knife.GetComponent<DestroyPlayerProjectile>().OnProjectileDestroyed += OnKnifeDestroyed;
    }

    public void OnAxeThrown(GameObject axe)
    {
        _axesDictionary.Add(axe, 0.0f);
        axe.GetComponent<DestroyPlayerProjectile>().OnProjectileDestroyed += OnAxeDestroyed;
    }

    public void OnKnifeDestroyed(GameObject knife)
    {
        knife.GetComponent<DestroyPlayerProjectile>().OnProjectileDestroyed -= OnKnifeDestroyed;
    }

    public void OnAxeDestroyed(GameObject axe)
    {
        _axesDictionary.Remove(axe);
        axe.GetComponent<DestroyPlayerProjectile>().OnProjectileDestroyed -= OnAxeDestroyed;
    }
}
