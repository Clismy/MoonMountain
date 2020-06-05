using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;

public class PlayerAnimation : MonoBehaviour
{
    Animator               anim;
    CharacterKeyboardInput keyboard;
    PlayerSettings         playerSettings;


    private const string animationBoolKey_Crounching = "IsCrouching";
    //har lagt denna dirkt i dig pga snabbaste lösningen
    //private const string animationBoolKey_Diging     = "IsDiging";
    private const string animationFloatKey_Moving    = "IsMoving";

    void Start()
    {
        anim           = GetComponent<Animator>();
        keyboard       = transform.parent.GetComponentInParent<CharacterKeyboardInput>();
        playerSettings = transform.parent.GetComponentInParent<PlayerSettings>();
    }

    void Update()
    {
        anim.SetBool(animationBoolKey_Crounching, playerSettings.crounhing);

        float val = 0;
        if(Mathf.Abs(keyboard.GetHorizontalMovementInput()) > 0 || Mathf.Abs(keyboard.GetVerticalMovementInput()) > 0)
        {
            val = 1;
        }
        anim.SetFloat(animationFloatKey_Moving, val);

    }
}