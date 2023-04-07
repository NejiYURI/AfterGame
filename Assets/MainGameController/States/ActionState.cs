using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionState : GameState
{
    public ActionState(MainGameManager gameManager) : base(gameManager)
    {

    }

    public override void StateStart()
    {
        if (GameEventManager.instance) GameEventManager.instance.ActionStart.Invoke();
    }
}
