using System;
using System.CodeDom;
using UnityEngine;
using XInputDotNetPure;

public class InputManager : MonoBehaviour
{
    public delegate void OnMoveHandler(Vector3 movement, bool goesRight);
    public event OnMoveHandler OnMove;

    public delegate void OnJumpHandler();
    public event OnJumpHandler OnJump;

    public delegate void OnJumpDownHandler();
    public event OnJumpDownHandler OnJumpDown;

    public delegate void OnUnderwaterControlHandler(bool goesDown);
    public event OnUnderwaterControlHandler OnUnderwaterControl;

    public delegate void OnBootsEquipHandler();
    public event OnBootsEquipHandler OnIronBootsEquip;

    public delegate void OnStopHandler();
    public event OnStopHandler OnStop;

    public delegate void OnBasicAttackHandler();
    public event OnBasicAttackHandler OnBasicAttack;

    public delegate void OnThrowAttackHandler();
    public event OnThrowAttackHandler OnThrowAttack;

    public delegate void OnThowAttackChangeButtonPressedHandler();
    public event OnThowAttackChangeButtonPressedHandler OnThowAttackChangeButtonPressed;

    private float _joysticksXAxisDeadZone = 0.1f;
    private float _joysticksYAxisDeadZone = 1f;

    private bool _leftShoulderReady = true;
    private bool _rightShoulderReady = true;
    private bool _xButtonReady = true;
    private bool _yButtonReady = true;
    private bool _aButtonReady = true;

    /* BEN_REVIEW
     * 
     * À séparer en petites méthodes pour chaque event (aka Input).
     * 
     * Pour votre input "CHEAT", faire un autre composant à mettre dans le dossier "Debug" (voir mes diapos). Il
     * ne faut pas poluer les scripts de Gameplay de "Cheats", car vous risquez de les oublier.
     */
    private void Update()
    {
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            OnMove(Vector3.left, false);
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            OnMove(Vector3.right, true);
        }
        else
        {
            OnStop();
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.Space))
        {
            OnJumpDown();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJump();
        }

        if (Input.GetKey(KeyCode.S))
        {
            OnUnderwaterControl(true);
        }

        if (Input.GetKey(KeyCode.W))
        {
            OnUnderwaterControl(false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            OnIronBootsEquip();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            OnBasicAttack();
        }

        if (Input.GetKey(KeyCode.L))
        {
            OnThrowAttack();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnThowAttackChangeButtonPressed();
        }

        GamePadInputs();
    }

    /* BEN_REVIEW
     * 
     * Vous devriez avoir deux "sous-composants" : l'un pour la manette et l'autre pour le clavier. Ensuite,
     * le "InputManager" s'enregistre auprès de ces deux composants pour exposer des évènements conjoints.
     * 
     * Le plus simple serait d'avoir une classe de base pour les trois. N'oubliez pas que la méthode "GetComponents"
     * peut obtenir les objets héritant d'une classe (ou implémentant une interface) aussi.
     */
    private void GamePadInputs()
    {
        /* BEN_REVIEW
         * 
         * OK, car jeu a un seul joueur, donc toutes les manettes branchées sont pour le joueur 1 (avec ce que cela implique
         * comme hack potentiels).
         */
        foreach (PlayerIndex player in Enum.GetValues(typeof(PlayerIndex)))
        {
            /* BEN_REVIEW
             * 
             * Obvious comment is obvious.
             */
            //Obtention de l'état du gamepad
            GamePadState state = GamePad.GetState(player);

            /* BEN_REVIEW
             * 
             * Obvious comment is obvious.
             */
            //Tester si la manette est connectée
            if (state.IsConnected)
            {
                /* BEN_REVIEW
                 * 
                 * Obvious comment is obvious + Copier/Coller ?
                 */
                //Déplacement gauche à droite du joueur (utilisez les events)
                if (Math.Abs(state.ThumbSticks.Left.X) > _joysticksXAxisDeadZone)
                {
                    if (state.ThumbSticks.Left.X < 0)
                    {
                        OnMove(Vector3.left, false);
                    }
                    else
                    {
                        OnMove(Vector3.right, true);
                    }
                }

                /* BEN_REVIEW
                 * 
                 * Hummm...Pas sûr que ça marche cette condition là...
                 */
                if (Math.Abs(state.ThumbSticks.Left.Y) == _joysticksYAxisDeadZone)
                {
                    if (state.Buttons.A == ButtonState.Pressed && state.ThumbSticks.Left.Y < 0)
                    {
                        OnJumpDown();
                    }

                    if (state.ThumbSticks.Left.Y < 0)
                    {
                        OnUnderwaterControl(true);
                    }

                    if (state.ThumbSticks.Left.Y > 0)
                    {
                        OnUnderwaterControl(false);
                    }
                }

                if (state.Buttons.A == ButtonState.Pressed && _aButtonReady && state.ThumbSticks.Left.Y != -_joysticksYAxisDeadZone)
                {
                    _aButtonReady = false;
                    OnJump();
                }
                if (state.Buttons.A == ButtonState.Released && !_aButtonReady)
                {
                    _aButtonReady = true;
                }

                #region Cheat

                if (state.Buttons.LeftStick == ButtonState.Pressed)
                {
                    GameObject.Find("Character").GetComponent<PlayerThrowingWeaponsMunitions>().KnifeMunition++;
                    GameObject.Find("Character").GetComponent<PlayerThrowingWeaponsMunitions>().AxeMunition++;
                }

                #endregion


                if (state.Buttons.Y == ButtonState.Pressed && _yButtonReady)
                {
                    _yButtonReady = false;
                    OnIronBootsEquip();
                }
                if (state.Buttons.Y == ButtonState.Released && !_yButtonReady)
                {
                    _yButtonReady = true;
                }

                if (state.Buttons.X == ButtonState.Pressed && _xButtonReady)
                {
                    _xButtonReady = false;
                    OnBasicAttack();
                }
                if (state.Buttons.X == ButtonState.Released && !_xButtonReady)
                {
                    _xButtonReady = true;
                }


                if (state.Buttons.B == ButtonState.Pressed)
                {
                    if (OnThrowAttack != null)
                    {
                        OnThrowAttack();
                    }
                }

                if (state.Buttons.LeftShoulder == ButtonState.Pressed && _leftShoulderReady)
                {
                    _leftShoulderReady = false;
                    OnThowAttackChangeButtonPressed();
                }

                if (state.Buttons.LeftShoulder == ButtonState.Released && !_leftShoulderReady)
                {
                    _leftShoulderReady = true;
                }

                if (state.Buttons.RightShoulder == ButtonState.Pressed && _rightShoulderReady)
                {
                    _rightShoulderReady = false;
                    OnThowAttackChangeButtonPressed();
                }
                if (state.Buttons.RightShoulder == ButtonState.Released && !_rightShoulderReady)
                {
                    _rightShoulderReady = true;
                }
            }
        }
    }
}
