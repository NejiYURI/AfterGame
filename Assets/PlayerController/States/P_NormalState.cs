using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_NormalState : PlayerState
{
    public P_NormalState(MainCharacterScript characterScript) : base(characterScript)
    {

    }

    public override void StateStart()
    {
        if (GameEventManager.instance) GameEventManager.instance.ActionStart.AddListener(StateOver);
    }

    void StateOver()
    {
        if (GameEventManager.instance) GameEventManager.instance.ActionStart.RemoveListener(StateOver);
        characterScript.SetState(new P_ActionState(characterScript));
    }
}
