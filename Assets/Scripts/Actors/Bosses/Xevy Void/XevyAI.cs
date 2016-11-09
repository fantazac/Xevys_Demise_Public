using UnityEngine;
using System.Collections;

public class XevyAI : MonoBehaviour
{
    Health _health;
    XevyAction _action;
    XevyMovement _movement;
    XevyPlayerIntaraction _playerInteraction;
    XevyProjectileInteraction _projectileInteraction;

	private void Start ()
    {
        _health = GetComponent<Health>();
        _action = GetComponent<XevyAction>();
        _movement = GetComponent<XevyMovement>();
        _playerInteraction = GetComponent<XevyPlayerIntaraction>();
        _projectileInteraction = GetComponent<XevyProjectileInteraction>();
	}
	
	private void Update ()
    {
	
	}
}
