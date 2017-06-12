using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	void Awake ()
    {
        HCI_Project.Library.Game game = new HCI_Project.Library.Game();
        Global.Game = game;
    }
}
