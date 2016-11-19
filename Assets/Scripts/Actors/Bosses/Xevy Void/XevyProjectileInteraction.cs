using UnityEngine;
using System.Collections.Generic;

public class XevyProjectileInteraction : MonoBehaviour
{
    private const float REACTION_TIME = 0.25f;
    private const float KNIFE_VERTICAL_BLOCK_MARGIN = 2.5f;
    private const float KNIFE_HORIZONTAL_BLOCK_MARGIN = 5;
    private const float AXE_HORIZONTAL_BLOCK_MARGIN = 3;

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
        List<GameObject> knifeDictionaryKeys = new List<GameObject>(_knivesDictionary.Keys);

        foreach (GameObject knife in knifeDictionaryKeys)
        {
            if (_knivesDictionary[knife] >= REACTION_TIME)
            {
                if (Vector2.Distance(knife.transform.position, transform.position) < KNIFE_HORIZONTAL_BLOCK_MARGIN && Mathf.Abs(knife.transform.position.y - transform.position.y) < KNIFE_VERTICAL_BLOCK_MARGIN)
                {
                    return true;
                }
            }
            else
            {
                _knivesDictionary[knife] += Time.fixedDeltaTime;
            }
        }
        return false;
    }

    public bool CheckAxesDistance()
    {
        List<GameObject> axeDictionaryKeys = new List<GameObject>(_axesDictionary.Keys);

        foreach (GameObject axe in axeDictionaryKeys)
        {
            if (_axesDictionary[axe] >= REACTION_TIME)
            {
                if (Vector2.Distance(axe.transform.position, transform.position) < AXE_HORIZONTAL_BLOCK_MARGIN)
                {
                    return true;
                }
            }
            else
            {
                _axesDictionary[axe] += Time.fixedDeltaTime;
            }
        }
        return false;
    }

    private void OnKnifeThrown(GameObject knife)
    {
        _knivesDictionary.Add(knife, 0.0f);
        knife.GetComponent<DestroyPlayerProjectile>().OnProjectileDestroyed += OnKnifeDestroyed;
    }

    private void OnAxeThrown(GameObject axe)
    {
        _axesDictionary.Add(axe, 0.0f);
        axe.GetComponent<DestroyPlayerProjectile>().OnProjectileDestroyed += OnAxeDestroyed;
    }

    private void OnKnifeDestroyed(GameObject knife)
    {
        _knivesDictionary.Remove(knife);
        knife.GetComponent<DestroyPlayerProjectile>().OnProjectileDestroyed -= OnKnifeDestroyed;
    }

    private void OnAxeDestroyed(GameObject axe)
    {
        _axesDictionary.Remove(axe);
        axe.GetComponent<DestroyPlayerProjectile>().OnProjectileDestroyed -= OnAxeDestroyed;
    }
}
