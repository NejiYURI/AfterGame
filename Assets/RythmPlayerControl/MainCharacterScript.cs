using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace RythmGame
{

    public class MainCharacterScript : PlayerStateMachine
    {
        public LineRenderer AimLine;

        public GameObject SlashLine;

        public float minDistance;
        public float MaxDistance;

        public SpriteRenderer CharacterSprite;
        private Animator animator;

        public PlayerInput inputControl;

        public List<AudioClip> MoveSound;
        public AudioClip SlashSound;
        public AudioClip MissSound;
        public AudioClip KillSound;

        [SerializeField]
        private Vector2 MousePos;

        private void Start()
        {
            AimLine = this.GetComponent<LineRenderer>();
            animator = this.GetComponent<Animator>();
            if (GameEventManager.instance)
            {
                GameEventManager.instance.GameOver.AddListener(GameOverFunc);
                GameEventManager.instance.DamageAction.AddListener(SlashEndAction);
                GameEventManager.instance.BeatMiss.AddListener(PlayMissSound);
                GameEventManager.instance.BeatMiss.AddListener(PlayResetAni);
            }
            SetState(new P_ActionState(this));
        }

        private void Update()
        {
            State.UpdateFunc();
        }

        public Vector2 GetMousePos()
        {
            return MousePos;
        }

        public void GetPos(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                MousePos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
                if (CharacterSprite) CharacterSprite.flipX = MousePos.x > this.transform.position.x;
            }
        }

        public void MouseClick(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                State.MouseClick();
            }
        }

        public void SlashClick(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                State.SlashClick();
            }
        }

        public void Dash()
        {
            this.transform.LeanMove(AimLine.GetPosition(1), 0.05f);
        }

        public void SpawnSlashLine(Vector2 start, Vector2 end)
        {
            GameObject obj = Instantiate(SlashLine, start, Quaternion.identity);
            if (obj.GetComponent<SlahLineScript>()) obj.GetComponent<SlahLineScript>().SetLineRender(start, end);
        }

        void GameOverFunc()
        {
            inputControl.enabled = false;
            SetState(new P_NormalState(this));
        }

        public void PlayMoveAni()
        {
            if (animator) animator.SetTrigger("Move");
        }

        void SlashEndAction(bool EnemyKilled)
        {
            if (EnemyKilled)
            {
                StartCoroutine(playkillsound());
                PlayDealDamageAni();
            }
            else PlayEndSlashAni();
        }

        public void PlayEndSlashAni()
        {
            if (animator) animator.SetTrigger("EndSlash");
        }

        public void PlayDealDamageAni()
        {
            if (animator) animator.SetTrigger("DealDamage");
        }

        public void PlayResetAni()
        {
            if (animator) animator.SetTrigger("Reset");
        }

        public void PlayMoveSound()
        {
            if (AudioController.instance && MoveSound.Count > 0) AudioController.instance.PlaySound(MoveSound[Random.Range(0, MoveSound.Count)]);
        }

        public void PlaySlashSound()
        {
            if (AudioController.instance) AudioController.instance.PlaySound(SlashSound);
        }

        public void PlayMissSound()
        {
            if (AudioController.instance) AudioController.instance.PlaySound(MissSound);
        }

        IEnumerator playkillsound()
        {
            yield return new WaitForSeconds(0.1f);
            AudioController.instance.PlaySound(KillSound);
            yield return new WaitForSeconds(0.2f);
            if (RythmGameManager.instance) RythmGameManager.instance.SpawnEnemy();
        }

    }
}
