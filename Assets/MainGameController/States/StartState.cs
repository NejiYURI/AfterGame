using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : GameState
{
    public StartState(MainGameManager gameManager) : base(gameManager)
    {
        
    }
    public override void StateStart()
    {
        gameManager.StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(1.5f);
        gameManager.SetState(new ActionState(gameManager));
    }

}
