using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RythmGame
{
    public class P_DashState : PlayerState
    {

        Coroutine dashCoro;
        Vector2 startPos;
        Vector2 endPos;
        public P_DashState(MainCharacterScript characterScript, Vector2 _start, Vector2 _end) : base(characterScript)
        {
            startPos = _start;
            endPos = _end;
        }

        public override void StateStart()
        {
            characterScript.AimLine.enabled = false;
            characterScript.Dash();
            dashCoro = characterScript.StartCoroutine(DashCoroutine());
        }

        public IEnumerator DashCoroutine()
        {
            yield return new WaitForSeconds(0.05f);
            characterScript.SpawnSlashLine(startPos, endPos);
            characterScript.SetState(new P_ActionState(characterScript));
        }

        public override void StateEnd()
        {
            if (dashCoro != null) characterScript.StopCoroutine(dashCoro);
        }
    }
}
