using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	void Start ()
    {
        Game game = new Game();
        Global.Game = game;
    }
}
