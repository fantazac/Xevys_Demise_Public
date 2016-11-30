using UnityEngine;
using System.Collections;

public class RespawnEnemy : MonoBehaviour
{
    public void SpawnEnemy(GameObject enemy, WaitForSeconds delay)
    {
        StartCoroutine(EnableCopy(enemy, delay));
    }

    private IEnumerator EnableCopy(GameObject enemy, WaitForSeconds delay)
    {
        yield return delay;
        
        enemy.SetActive(true);
    }
}
