using UnityEngine;
using System.Collections;

public class EnemyType : MonoBehaviour
{
    [SerializeField]
    private bool _isABoss = false;

    public bool IsABoss { get { return _isABoss; } }

}
