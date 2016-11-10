using UnityEngine;
using System.Collections;

public class PauseMenuInputs : MonoBehaviour
{

    public delegate void PauseMenuOntriggerHandler();
    public event PauseMenuOntriggerHandler TriggerAnimations;

    private InputManager _inputManager;

    private bool _canSlide;

    public bool CanSlide { private get { return _canSlide; } set { _canSlide = value; } }

    private void Start()
    {
        _inputManager = GameObject.Find("Character").GetComponentInChildren<InputManager>();
        _inputManager.OnPause += PauseMenuTriggered;
        _canSlide = true;
    }

    private void PauseMenuTriggered()
    {
        if (CanSlide)
        {
            TriggerAnimations();
        }
    }
}
