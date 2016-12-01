using UnityEngine;
using System.Collections;

public class DisableBreakableItemOnHitSoundFinished : MonoBehaviour
{
    private Health _health;

    private void Start()
    {
        _health = GetComponent<Health>();

        GetComponent<PlaySoundOnHealthChanged>().OnHitSoundFinished += DisableItem;
    }

    private void DisableItem()
    {
        if (_health.IsDead())
        {
            gameObject.SetActive(false);
        }
    }
}
