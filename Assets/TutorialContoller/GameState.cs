using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Tutorial
{
    public class GameState
    {
        protected TutorialManager gameManager;
        public GameState(TutorialManager _gameManager)
        {
            this.gameManager = _gameManager;
        }

        public virtual void StateStart()
        {

        }

        public virtual void StateEnd()
        {

        }

        public virtual void BeatSuccess(bool IsSlash)
        {

        }
    }
}
