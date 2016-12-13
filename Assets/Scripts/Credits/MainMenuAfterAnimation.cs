using UnityEngine;
using System.Collections;

public class MainMenuAfterAnimation : MonoBehaviour
{
    private ShowMainMenu _showMainMenu;

    private void Start()
    {
        _showMainMenu = GetComponent<ShowMainMenu>();
        StartCoroutine(OnAnimEnded());
    }

    private IEnumerator OnAnimEnded()
    {
        yield return new WaitForSeconds(30f);
        _showMainMenu.ShowMenu();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
