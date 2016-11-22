using UnityEngine;
using System.Collections;

public class AnimationTags : MonoBehaviour
{

    public string IsDying { get; private set; }

    private void Start()
    {
        IsDying = "IsDying";
    }

}
