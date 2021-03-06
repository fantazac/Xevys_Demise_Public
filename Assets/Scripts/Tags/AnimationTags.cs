﻿using UnityEngine;

public class AnimationTags : MonoBehaviour
{

    public string IsGoingForward { get; private set; }
    public string IsDying { get; private set; }
    public string IsJumping { get; private set; }
    public string IsMoving { get; private set; }
    public string IsFloating { get; private set; }
    public string IsDamaged { get; private set; }
    public string IsAttacking { get; private set; }
    public string Character_Attack { get; private set; }
    public string Character_Attack_NoSword { get; private set; }
    public string Character_Crouch_Attack { get; private set; }
    public string Character_Crouch_Attack_NoSword { get; private set; }
    public string IsCrouching { get; private set; }
    public string IsFalling { get; private set; }
    public string IsFlying { get; private set; }
    public string IsDead { get; private set; }
    public string ItemPickedUp { get; private set; }
    public string FadeOut { get; private set; }
    public string FadeIn { get; private set; }
    public string PauseMenuButtonsGroupActiveAnimation { get; private set; }
    public string State { get; private set; }
    public string IsHit { get; private set; }

    private void Start()
    {
        IsGoingForward = "IsGoingForward";
        IsDying = "IsDying";
        IsJumping = "IsJumping";
        IsDamaged = "IsDamaged";
        IsAttacking = "IsAttacking";
        Character_Attack = "Character_Attack";
        Character_Crouch_Attack = "Character_Crouch_Attack";
        Character_Attack_NoSword = "Character_Attack_NoSword";
        Character_Crouch_Attack = "Character_Crouch_Attack";
        Character_Crouch_Attack_NoSword = "Character_Crouch_Attack_NoSword";
        IsCrouching = "IsCrouching";
        IsFloating = "IsFloating";
        IsMoving = "IsMoving";
        IsFalling = "IsFalling";
        IsFlying = "IsFlying";
        IsDead = "IsDead";
        ItemPickedUp = "ItemPickedUp";
        FadeOut = "FadeOut";
        FadeIn = "FadeIn";
        PauseMenuButtonsGroupActiveAnimation = "PauseMenuButtonsGroupActiveAnimation";
        State = "State";
        IsHit = "IsHit";
    }
}
