using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Tutorial
{
    public class SlashTutorial : GameState
    {
        private int Counter;
        private bool StopSpawn;
        private bool BeatFlipFlop;
        public SlashTutorial(TutorialManager gameManager) : base(gameManager)
        {
            if (GameEventManager.instance)
            {
                GameEventManager.instance.BeatOver.AddListener(RespawnBeat);
                GameEventManager.instance.EnemyDead.AddListener(EnemyDead);
            }
        }

        public override void StateStart()
        {
            Counter = 3;
            gameManager.ResetCharacter();
            gameManager.SetDesc("Press RMB when red ring touch the center circle to attack enemies on slash tracks.");
            gameManager.SetCounter(Counter + " enemies left!");
            gameManager.SpawnEnemy(new Vector2(-1.5f,-1.5f),false);
            gameManager.SpawnEnemy(new Vector2(1.5f, -1.5f), false);
            gameManager.SpawnEnemy(new Vector2(0, 0.75f), false);
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
            if (GameEventManager.instance)
            {
                GameEventManager.instance.BeatOver.RemoveListener(RespawnBeat);
                GameEventManager.instance.EnemyDead.RemoveListener(EnemyDead);
            }
            gameManager.SetState(new EnemyTutorial(gameManager));
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
            StateEnd();
        }
    }
}
