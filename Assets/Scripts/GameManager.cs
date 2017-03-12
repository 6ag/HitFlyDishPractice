using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// 游戏3种状态
	public enum GameState {
		START, // 开始状态
		GAME,  // 游戏进行状态
		END   // 游戏结束状态
	}

	private GameState mGameState; // 当前游戏状态
	private int mScore; // 游戏分数
	public const float GAMETIME = 40.0f; // 游戏时间
	private float mTime; // 剩余游戏时间
	private bool mStartTime = false; // 开始倒计时

	private GameObject mStartUI;
	private GameObject mGameUI;
	private GameObject mEndUI;
	private GUIText mGameScoreText; // 游戏分数文本组件
	private GUIText mGameTimeText; // 游戏时间文本组件
	private GUIText mGameTotalScore; // 游戏总共得分文本组件
	private AudioSource mBgAudioScoure; // 背景声音组件
	private Weapon mWeapon; // 武器脚本组件
	private FeiPanManager mFeiPanManager; // 飞盘管理组件

	void Start()
	{
		mStartUI = GameObject.Find("StartUI");
		mGameUI = GameObject.Find("GameUI");
		mEndUI = GameObject.Find("EndUI");
		
		// 游戏分数文本组件
		mGameScoreText = GameObject.Find("GameScore").GetComponent<GUIText>();

		// 剩余游戏时间文本组件
		mGameTimeText = GameObject.Find("GameTime").GetComponent<GUIText>();

		// 游戏总共得分文本组件
		mGameTotalScore = GameObject.Find("GameTotalScore").GetComponent<GUIText>();

		// 背景声音组件
		mBgAudioScoure = GameObject.Find("Main Camera").GetComponent<AudioSource>();

		// 武器脚本组件
		mWeapon = GameObject.Find("Weapon").GetComponent<Weapon>();

		// 飞盘管理组件
		mFeiPanManager = GameObject.Find("FeiPanParent").GetComponent<FeiPanManager>();

		// 游戏默认状态是开始状态
		ChangeGameState(GameState.START);
	}

	void Update()
	{
		if (mStartTime && mTime < 0) // 时间结束
		{
			ChangeGameState(GameState.END);
			mGameTotalScore.text = "总分：" + mScore + "分";
		}
		else if (mStartTime)  // 已经开始计时
		{
			mTime -= Time.deltaTime;
			mGameTimeText.text = "时间：" + Mathf.Round(mTime) + "秒";
		}
	}

	// 开始倒计时
	public void StartTime() 
	{
		mStartTime = true;
		mTime = GAMETIME;
	}

	// 停止倒计时
	public void StopTime() 
	{
		mStartTime = false;
		mTime = GAMETIME;

		// 分数清空
		mScore = 0;
		mGameScoreText.text = "分数：" + mScore + "分";
	}

	// 增加分数
	public void AddScore() 
	{
		mScore++;
		mGameScoreText.text = "分数：" + mScore + "分";
	}

	// 切换游戏状态
	public void ChangeGameState(GameState state) 
	{
		mGameState = state;

		if (mGameState == GameState.START) 
		{
			// UI切换
			mStartUI.SetActive(true);
			mGameUI.SetActive(false);
			mEndUI.SetActive(false);

			// 背景音乐切换
			mBgAudioScoure.Stop();

			// 武器控制切换
			mWeapon.ChangeCanMove(false);

		}
		else if (mGameState == GameState.GAME) 
		{
			// UI切换
			mStartUI.SetActive(false);
			mGameUI.SetActive(true);
			mEndUI.SetActive(false);

			// 背景音乐切换
			mBgAudioScoure.Play();

			// 武器控制切换
			mWeapon.ChangeCanMove(true);

			// 开始生成飞盘
			mFeiPanManager.StartCreateFeiPan();

			// 开始倒计时
			StartTime();
		} 
		else if (mGameState == GameState.END) 
		{
			// UI切换
			mStartUI.SetActive(false);
			mGameUI.SetActive(false);
			mEndUI.SetActive(true);

			// 背景音乐切换
			mBgAudioScoure.Stop();

			// 武器控制切换
			mWeapon.ChangeCanMove(false);

			// 停止生成飞盘
			mFeiPanManager.StopCreateFeiPan();

			// 移除所有已经存在的飞盘
			mFeiPanManager.RemoveFeiPan();
			
			// 停止倒计时
			StopTime();
		}

	}

}
