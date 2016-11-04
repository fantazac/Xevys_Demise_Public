using UnityEngine;
using System.Collections;

/* BEN_CORRECTION
 * 
 * Humm...Au début, j'étais super content de constater que vous aviez fait un script générique
 * d'attaque simple pour tous les acteurs du jeu, ce que le nom laissait présumer. Puis, j'ai
 * trouvé le composant "InputManager", qui est destiné au player, ce qui veut donc dire que ce
 * composant est pour le joueur uniquement et devrait donc se trouver dans le dossier "Player"
 * (et être nommé différemment).
 */
public class ActorBasicAttack : MonoBehaviour
{
    /* BEN_CORRECTION
     * 
     * Le fait que ce soit "const" le rend impossible à modifier dans l'éditeur. On ne peut pas
     * "désérialiser" une constante.
     */
    [SerializeField]
    private const float ATTACK_SPEED = 0.5f;

    private InputManager _inputManager;
    private GameObject _attackHitBox;
    private AudioSourcePlayer _soundPlayer;
    private Animator _anim;

    private bool _isAttacking = false;
    private float _attackCooldown;

    private void Start()
    {
        _inputManager = GetComponentInChildren<InputManager>();
        _attackHitBox = GameObject.Find("CharacterBasicAttackBox");
        _anim = GameObject.Find("CharacterSprite").GetComponent<Animator>();
        /* BEN_CORRECTION
         * 
         * Dans la review, j'avais demandé de sortir le déclanchement de l'audio dans un
         * autre component. Il y a plusieurs façon de le faire, mais l'un des meilleur était de
         * créer des évènements et de faire que les composants de son s'y abonnent.
         */
        _soundPlayer = GetComponent<AudioSourcePlayer>();
        _attackCooldown = ATTACK_SPEED;

        _inputManager.OnBasicAttack += OnBasicAttack;
    }

    private void Update()
    {
        /* BEN_CORRECTION
         * 
         * À remplacer par une coroutine.
         */
        _anim.SetBool("IsAttacking", _isAttacking);
        _attackCooldown += Time.deltaTime;

        if (_attackCooldown >= ATTACK_SPEED / 2)
        {
            _attackHitBox.GetComponent<BoxCollider2D>().enabled = false;
            _isAttacking = false;
        }
    }

    private void OnBasicAttack()
    {
        /* BEN_CORRECTION
         * 
         * Déclancher la coroutine ici.
         */
        if (_attackCooldown >= ATTACK_SPEED)
        {
            _isAttacking = true;
            _attackCooldown = 0;
        }
    }

    public void OnBasicAttackEffective()
    {
        _soundPlayer.Play(0);
        _attackHitBox.GetComponent<BoxCollider2D>().enabled = true;
    }
}
