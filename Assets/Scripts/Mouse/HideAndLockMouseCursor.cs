using UnityEngine;

public class HideAndLockMouseCursor : MonoBehaviour
{

    public delegate void OnApplicationLosesFocusHandler(bool isDead);
    public event OnApplicationLosesFocusHandler OnApplicationLosesFocus;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            Start();
        }
        else
        {
            OnApplicationLosesFocus(false);
        }
    }
}
