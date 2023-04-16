using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Tutorial
{
    public class MoveTutorial : GameState
    {
        private int Counter;
        private bool StopSpawn;
        public MoveTutorial(TutorialManager gameManager) : base(gameManager)
        {
            if (GameEventManager.instance)
            {
                GameEventManager.instance.BeatOver.AddListener(RespawnBeat);
            }
        }

        public override void StateStart()
        {
            Counter = 3;
            gameManager.ResetCharacter();
            gameManager.SetDesc("Press LMB when white ring touch the center circle to move.");
            gameManager.SetCounter(Counter + " times left!");
            RespawnBeat();
        }
        public override void BeatSuccess(bool IsSlash)
        {
            Counter--;
            gameManager.SetCounter(Counter + " times left!");
            if (Counter <= 0)
            {
                gameManager.StartCoroutine(StateEndTimer());
            }
        }
        public override void StateEnd()
        {
            if (GameEventManager.instance) GameEventManager.instance.BeatOver.RemoveListener(RespawnBeat);
            gameManager.SetState(new SlashTutorial(gameManager));
        }

        void RespawnBeat()
        {
            gameManager.StartCoroutine(SpawnTimer());
        }

        IEnumerator SpawnTimer()
        {
            yield return new WaitForSeconds(1f);
            if (!StopSpawn)
                GameEventManager.instance.SpawnBeat.Invoke();
        }

        IEnumerator StateEndTimer()
        {
            gameManager.SetDesc("Great!");
            StopSpawn = true;
            yield return new WaitForSeconds(0.5f);
            if (GameEventManager.instance) GameEventManager.instance.DamageFalied.Invoke();
            yield return new WaitForSeconds(2.5f);
            StateEnd();
        }
    }
}
