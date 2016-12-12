using UnityEngine;
using System.Collections;

public class MainMenuAfterAnimation : MonoBehaviour
{
    public void OnAnimEnded()
    {
        GameObject.Find("Transition").GetComponent<ShowMainMenu>().ShowMenu();
    }
}
