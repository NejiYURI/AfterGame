using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class TutorialStateMachine : MonoBehaviour
    {
        protected GameState State;
        public void SetState(GameState _state)
        {
            State = _state;
            State.StateStart();
        }
    }
}
