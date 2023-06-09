using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MainGameNormal
{
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
            yield return new WaitForSeconds(0.5f);
            gameManager.SetState(new ActionState(gameManager));
        }

    }
}
