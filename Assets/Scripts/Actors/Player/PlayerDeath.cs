using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour
{
    private Health _playerHealth;
    private Animator _anim;

	private void Start ()
	{
	    _playerHealth = StaticObjects.GetPlayer().GetComponent<Health>();
	    _anim = StaticObjects.GetPlayer().GetComponentInChildren<Animator>();
        _anim.SetBool("IsDead", false);

        _playerHealth.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        _anim.SetBool("IsDead", true);
    }
}
