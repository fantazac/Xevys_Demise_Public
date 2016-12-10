using UnityEngine;

public class LockAchievement : MonoBehaviour
{
    private void Start()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
