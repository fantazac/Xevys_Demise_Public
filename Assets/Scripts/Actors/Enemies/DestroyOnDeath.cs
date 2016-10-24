using UnityEngine;
using System.Collections;

public class DestroyOnDeath : MonoBehaviour
{
    private Health _health;
    private Animator _animator;

    private void Start()
    {
        _health = GetComponent<Health>();
        _animator = GetComponent<Animator>();

    }

    private void Update()
    {
        if (_health.HealthPoint <= 0)
        {
            _animator.SetBool("IsDying", true);
        }
    }

    /* BEN_REVIEW
     * 
     * C'est pas appellé nulle part il semble...
     */
    protected void Destroy()
    {
        GetComponent<DropItems>().Drop();
        Destroy(gameObject);
    }
}
