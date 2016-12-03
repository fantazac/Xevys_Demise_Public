using UnityEngine;
using System.Collections;

/* BEN_CORRECTION
 * 
 * OK...regarde ce que cela fait. Cela fait un "FadeIn" suivi d'un "FadeOut". Pourtant, cela s'appelle "UIAnimationManager".
 * 
 * Hum....
 * 
 * Pourquoi pas "FadeInFadeOutUIAnimation" ?
 * 
 * Aussi, je ne puis m'empêcher de constater que ce component, dont le but est de géré une animation, sait exactement par qui il doit être
 * déclanché. C'est pas des blagues, voir "Start". Vous avez un Controlleur pour votre "PauseMenu" (Controleur, Vue, tiens donc...) qui pourrait
 * déclancher ces animations très certainement (ce serait "PauseControlleur" qui serait abbonné aux évènements de "PauseMenuInputs" et qui, ensuite,
 * appèlerait "UIAnimationManager").
 * 
 * Enfin, je vois la classe "PauseMenuAnimationManager" qui, elle aussi, est une animation. N'est-il pas possible de faire de l'héritage à quelque part ?
 * Genre, une classe abstraite "UIAnimation" ?
 * 
 */
public class UIAnimationManager : MonoBehaviour
{
    private PauseMenuInputs _pauseMenuInputs;

    private Animator _animator;
    private bool _active;

    private void Start()
    {
        _pauseMenuInputs = StaticObjects.GetPauseMenuPanel().GetComponentInChildren<PauseMenuInputs>();
        _animator = GetComponent<Animator>();
        _pauseMenuInputs.TriggerAnimations += FadeUI;
        _active = true;
    }

    private void FadeUI()
    {
        if (!_active)
        {
            FadeIn();
        }
        else
        {
            FadeOut();
        }
    }

    private void FadeOut()
    {
        _animator.SetTrigger(StaticObjects.GetAnimationTags().FadeOut);
        _active = false;
    }

    private void FadeIn()
    {
        _animator.SetTrigger(StaticObjects.GetAnimationTags().FadeIn);
        _active = true;
    }
}
