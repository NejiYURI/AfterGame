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
        if (GameEventManager.instance) GameEventManager.instance.ActionStart.Invoke(true);
        gameManager.StartCoroutine(TimeCounter());
    }

    IEnumerator TimeCounter()
    {
        yield return new WaitForSeconds(3f);
        if (GameEventManager.instance) GameEventManager.instance.ActionStart.Invoke(false);
        gameManager.SetState(new DealDamageState(gameManager));
    }
}
