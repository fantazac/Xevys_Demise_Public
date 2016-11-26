using UnityEngine;
using System.Collections.Generic;

public class XevyProjectileInteraction : MonoBehaviour
{
    [SerializeField]
    private float _reactionTime = 0.25f;

    [SerializeField]
    private float _knifeVerticalBlockDetectionDistance = 2.5f;

    [SerializeField]
    private float _knifeHorizontalBlockDetectionDistance = 5;

    [SerializeField]
    private float _axeHorizontalBlockDetectionDistance = 3;

    private Dictionary<GameObject, float> _knivesDictionary;
    private Dictionary<GameObject, float> _axesDictionary;
    private ThrowKnife _throwKnifeAttack;
    private ThrowAxe _throwAxeAttack;

    private void Start()
    {
        _knivesDictionary = new Dictionary<GameObject, float>();
        _axesDictionary = new Dictionary<GameObject, float>();

        _throwKnifeAttack = StaticObjects.GetPlayer().GetComponent<ThrowKnife>();
        _throwAxeAttack = StaticObjects.GetPlayer().GetComponent<ThrowAxe>();

        _throwKnifeAttack.OnKnifeThrown += OnKnifeThrown;
        _throwAxeAttack.OnAxeThrown += OnAxeThrown;
	}

    private void Destroy()
    {
        _throwKnifeAttack.OnKnifeThrown -= OnKnifeThrown;
        _throwAxeAttack.OnAxeThrown -= OnAxeThrown;
    }

    public bool CheckKnivesDistance()
    {
        List<GameObject> knifeDictionaryKeys = new List<GameObject>(_knivesDictionary.Keys);

        foreach (GameObject knife in knifeDictionaryKeys)
        {
            if (_knivesDictionary[knife] >= _reactionTime)
            {
                if (Vector2.Distance(knife.transform.position, transform.position) < _knifeHorizontalBlockDetectionDistance && Mathf.Abs(knife.transform.position.y - transform.position.y) < _knifeVerticalBlockDetectionDistance)
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
            if (_axesDictionary[axe] >= _reactionTime)
            {
                if (Vector2.Distance(axe.transform.position, transform.position) < _axeHorizontalBlockDetectionDistance)
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
        _knivesDictionary.Add(knife, 0);
        knife.GetComponent<DestroyPlayerProjectile>().OnProjectileDestroyed += OnKnifeDestroyed;
    }

    private void OnAxeThrown(GameObject axe)
    {
        _axesDictionary.Add(axe, 0);
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
