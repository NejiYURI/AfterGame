using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_DashState: PlayerState
{
    public P_DashState(MainCharacterScript characterScript) : base(characterScript)
    {

    }

    public override void StateStart()
    {
        characterScript.AimLine.enabled = false;
        characterScript.Dash();
        characterScript.StartCoroutine(DashCoroutine());
    }

    public IEnumerator DashCoroutine()
    {
        yield return new WaitForSeconds(0.05f);
        characterScript.AimLine.enabled = true;
        characterScript.SetState(new P_ActionState(characterScript));
        
    }
}
