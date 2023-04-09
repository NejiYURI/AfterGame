using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageState : GameState
{
    public DealDamageState(MainGameManager gameManager) : base(gameManager)
    {

    }

    public override void StateStart()
    {
        if (GameEventManager.instance) GameEventManager.instance.DealDamage.Invoke();
        gameManager.StartCoroutine(TimeCounter());
    }

    IEnumerator TimeCounter()
    {
        yield return new WaitForSeconds(0.5f);
        gameManager.SetState(new StartState(gameManager));
    }
}
