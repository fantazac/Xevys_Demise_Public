using UnityEngine;

public class UnlockTheCursor : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
