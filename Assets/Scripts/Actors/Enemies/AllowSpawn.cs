using UnityEngine;
using System.Collections;

public class AllowSpawn : MonoBehaviour
{
    private bool _canSpawn = false;

    public bool CanSpawn { get { return _canSpawn; } set { _canSpawn = value; } }
}
