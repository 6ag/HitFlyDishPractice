using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReset : MonoBehaviour {

	private GameManager mGameManager;

	void Start()
	{
		mGameManager = GameObject.Find("UI").GetComponent<GameManager>();
	}

	void OnMouseDown()
	{
		// 重新开始游戏
		mGameManager.ChangeGameState(GameManager.GameState.START);
	}
}
