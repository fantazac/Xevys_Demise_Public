using UnityEngine;

public class HideAndLockMouseCursor : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
