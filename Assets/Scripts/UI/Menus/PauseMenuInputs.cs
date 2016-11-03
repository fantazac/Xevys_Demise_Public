using UnityEngine;
using System.Collections;

public class PauseMenuInputs : MonoBehaviour
{

    public delegate void PauseMenuOntriggerHandler();
    public event PauseMenuOntriggerHandler TriggerAnimations;

    private InputManager _inputManager;
    
    private void Start()
    {
        _inputManager = GameObject.Find("Character").GetComponentInChildren<InputManager>();
        _inputManager.OnPause += PauseMenuTriggered;
    }
    private void PauseMenuTriggered()
    {
        TriggerAnimations();
    }
}
