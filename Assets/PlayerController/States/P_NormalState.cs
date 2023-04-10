using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NormalGame
{
    public class P_NormalState : PlayerState
    {
        public P_NormalState(MainCharacterScript characterScript) : base(characterScript)
        {

        }

        public override void StateStart()
        {
            characterScript.AimLine.enabled = false;
        }
    }
}
