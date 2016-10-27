using UnityEngine;
using System.Collections;

public class ActorBasicAttackEffective : MonoBehaviour
{
    private void AnimationEnded()
    {
        GetComponentInParent<ActorBasicAttack>().OnBasicAttackEffective();
    }
}
