using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Tutorial
{
    public class EnemyTutorial : GameState
    {
        private int Counter;
        private bool StopSpawn;
        private bool BeatFlipFlop;
        public EnemyTutorial(TutorialManager gameManager) : base(gameManager)
        {
            if (GameEventManager.instance)
            {
                GameEventManager.instance.BeatOver.AddListener(RespawnBeat);
                GameEventManager.instance.EnemyDead.AddListener(EnemyDead);
            }
        }

        public override void StateStart()
        {
            Counter = 4;
            gameManager.ResetCharacter();
            gameManager.SetDesc("Enemies will move after red ring beat.");
            gameManager.SetCounter(Counter + " enemies left!");
            gameManager.SpawnEnemy(new Vector2(-1.5f, -1.5f), true);
            gameManager.SpawnEnemy(new Vector2(1.5f, -1.5f), true);
            gameManager.SpawnEnemy(new Vector2(1.5f, 0.75f), true);
            gameManager.SpawnEnemy(new Vector2(-1.5f, 0.75f), true);
            RespawnBeat();
        }

        void RespawnBeat()
        {
            gameManager.StartCoroutine(SpawnTimer());
        }

        void EnemyDead()
        {
            Counter--;
            gameManager.SetCounter(Counter + " enemies left!");
            if (Counter <= 0)
            {
                gameManager.StartCoroutine(StateEndTimer());
            }
        }

        public override void StateEnd()
        {
            gameManager.ReturnToTitle();
        }

        IEnumerator SpawnTimer()
        {
            yield return new WaitForSeconds(1f);
            if (!StopSpawn)
            {
                if (BeatFlipFlop)
                    GameEventManager.instance.SpawnSlash.Invoke();
                else
                    GameEventManager.instance.SpawnBeat.Invoke();
                BeatFlipFlop = !BeatFlipFlop;
            }
        }

        IEnumerator StateEndTimer()
        {
            gameManager.SetDesc("Great!");
            StopSpawn = true;
            yield return new WaitForSeconds(0.5f);
            if (GameEventManager.instance) GameEventManager.instance.DamageFalied.Invoke();
            yield return new WaitForSeconds(2.5f);
            gameManager.SetDesc("Now, enjoy the game (=w=)~");
            yield return new WaitForSeconds(3f);
            StateEnd();
        }
    }
}
