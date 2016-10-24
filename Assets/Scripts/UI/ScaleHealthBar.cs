using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/* BEN_REVIEW
 * 
 * Cette classe fait deux choses : elle change la taille et la couleur. C'est donc deux composants
 * que vous devriez avoir, et non pas un seul.
 */
public class ScaleHealthBar : MonoBehaviour
{
    [SerializeField]
    public const int MAX_HEALTH = 1000;

    private float _initialRectangleX;
    private bool _healthBarIsScaling = false;
    private Transform _healthBar;
    private Image _healthBarImage;
    private Health _health; 

	private void Start ()
	{
        /* BEN_REVIEW
         * 
         * Vous devriez vous faire une classe "Singleton" pour le player. Dans les jeux, c'est commun. Cela vous
         * évitera de faire un "Find" à répétition pour obtenir, de toute façon, toujours le même "GameObject".
         * 
         * Faites vous une classe (genre "GlobalGameObjects") avec des propriétés (genre "Player") qui font un "Find"
         * dès la première demande, mais conservent la réponse pour les demandes subséquentes.
         * 
         * Attention!!! Lorsque la partie se termine, il faudrait "Resetter" le Singleton.
         * 
         * Me voir si pas clair comment faire.
         */
	    _health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        /* BEN_REVIEW
         * 
         * Pour tous les objets où le nom doit être utilisé directement dans le code, vous devriez vous faire une
         * classe de constantes (genre GameObjectNames). Cependant, un "Singleton" est aussi souhaitable pour éviter
         * de faire plusieurs fois "Find" comme ici.
         */
        _healthBar = GameObject.Find("HealthBar").GetComponent<Transform>();
	    _healthBarImage = GameObject.Find("HealthBar").GetComponent<Image>();
	    _initialRectangleX = _healthBar.localScale.x;
        /* BEN_REVIEW
         * 
         * Couleur à choisir dans l'éditeur (color est éditable sous Unity en passant, donc directement un attribut
         * de type Color avec SerializeField).
         */
	    _healthBarImage.color = new Color(0, 1, 0, 1);

        _health.OnHealthChanged += OnHealthChanged;
	}

    private void FixedUpdate()
    {
        /* BEN_REVIEW
         * 
         * J'irais avec une cooroutine pour cela. Cela vous simplifierait la vie.
         */
        // Ajuste la taille de la barre de vie en fonction du temps
        if (_healthBarIsScaling)
        {
            Vector3 finalSize = new Vector3(_initialRectangleX - (100 - ((_health.HealthPoint - 10)*100)/MAX_HEALTH)*_initialRectangleX/100,
                    _healthBar.localScale.y, _healthBar.localScale.z);
            _healthBar.localScale = Vector3.Lerp(_healthBar.localScale, finalSize, Time.fixedDeltaTime);

            if (_healthBar.localScale == finalSize)
            {
                _healthBarIsScaling = false;
            }
        }
    }

    private void OnHealthChanged(int hitPoints)
    {
        if (_healthBar.localScale.x > 0)
        {
            _healthBarIsScaling = true;

            /* BEN_REVIEW
             * 
             * Les deux couleurs entre lesquel il y a interpolation en fonction des points de vie devrait être
             * paramêtrées dans l'éditeur.
             */

            // Change la couleur de la barre de vie en fonction du % de vie restant
            if (_health.HealthPoint - hitPoints >= 50f*MAX_HEALTH/100f)
            {
                /* BEN_REVIEW
                 *  
                 *            _______ _______ ______ _   _ _______ _____ ____  _   _ _ _ _ 
                 *         /\|__   __|__   __|  ____| \ | |__   __|_   _/ __ \| \ | | | | |
                 *        /  \  | |     | |  | |__  |  \| |  | |    | || |  | |  \| | | | |
                 *       / /\ \ | |     | |  |  __| | . ` |  | |    | || |  | | . ` | | | |
                 *      / ____ \| |     | |  | |____| |\  |  | |   _| || |__| | |\  |_|_|_|
                 *     /_/    \_\_|     |_|  |______|_| \_|  |_|  |_____\____/|_| \_(_|_|_)
                 *                                                                                                                                                  
                 *    
                 * ÉVITEZ À TOUT PRIX LES NEW!!!!!! SURTOUT DANS LES JEUX!!!!!!
                 * 
                 * Obtenez la couleur, modifiez la, et réassignez là.
                 * 
                 * EX :
                 *         Color interpolatedColor = _healthBarImage.color;
                 *         interpolatedColor.a = 4;
                 */
                _healthBarImage.color = new Color(_healthBarImage.color.r + (float)(hitPoints * 0.0020), 1, 0, 1);
            }
            else
            {
                /* BEN_REVIEW
                 * 
                 * Constantes magiques un peu partout là dedans.
                 */
                _healthBarImage.color = new Color(1, _healthBarImage.color.g - (float)(hitPoints * 0.0020), 0, 1);
            }
        }    
    }
}
