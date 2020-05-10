using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    CharacterKeyboardInput keyboard;
    PlayerSettings playerSettings;

    void Start()
    {
        anim = GetComponent<Animator>();
        keyboard = transform.parent.GetComponentInParent<CharacterKeyboardInput>();
        playerSettings = transform.parent.GetComponentInParent<PlayerSettings>();
    }

    void Update()
    {
        anim.SetBool("IsCrouching", playerSettings.crounhing);

        float val = 0;
        if(Mathf.Abs(keyboard.GetHorizontalMovementInput()) > 0 || Mathf.Abs(keyboard.GetVerticalMovementInput()) > 0)
        {
            val = 1;
        }
        anim.SetFloat("IsMoving", val);

    }
}