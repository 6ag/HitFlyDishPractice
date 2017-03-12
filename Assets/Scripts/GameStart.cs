using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour {

	private GameManager mGameManager; // 游戏管理者脚本组件

	void Start() 
	{
		mGameManager = GameObject.Find("UI").GetComponent<GameManager>();
	}

	void OnMouseDown()
	{
		// 点击了开始游戏
		mGameManager.ChangeGameState(GameManager.GameState.GAME);
	}

}
