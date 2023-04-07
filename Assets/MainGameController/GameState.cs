using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    protected MainGameManager gameManager;
    public GameState(MainGameManager _gameManager)
    {
        this.gameManager = _gameManager;
    }

    public virtual void StateStart()
    {

    }

    public virtual void StateEnd()
    {

    }
}
