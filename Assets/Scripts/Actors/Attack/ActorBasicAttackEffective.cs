using UnityEngine;
using System.Collections;

public class ActorBasicAttackEffective : MonoBehaviour
{
    /* BEN_REVIEW
     * 
     * J'ai rien trouvé qui appelle cette méthode dans le code, alors j'ai pensé à un évènement Unity, 
     * mais encore une fois, rien de tel dans la documentation.
     * 
     * Faudra me montrer où c'est appellé, mais si c'est pas appelé nul part, à supprimer.
     */
    private void AnimationEnded()
    {
        GetComponentInParent<ActorBasicAttack>().OnBasicAttackEffective();
    }
}
