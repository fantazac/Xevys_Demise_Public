using UnityEngine;
using System.Collections;

public class LoadEnemiesInRoom : MonoBehaviour
{
    public void UnloadRoom()
    {
        foreach (Transform enemy in transform)
        {
            if (enemy != transform && enemy.gameObject.activeSelf)
            {
                Destroy(enemy.gameObject);
            }
            else if (!enemy.gameObject.activeSelf)
            {
                enemy.gameObject.SetActive(true);
            }
        }
        gameObject.SetActive(false);
    }
}
