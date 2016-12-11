using UnityEngine;

public class ContainerAnimationListener : MonoBehaviour
{
    public void DisableAfterFadeOut()
    {
        gameObject.SetActive(false);
    }
}
