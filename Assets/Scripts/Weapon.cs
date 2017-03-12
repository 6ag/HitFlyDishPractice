using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	private Ray mRay; // 射线
	private RaycastHit mHit; // 射线碰撞点信息
	private Transform mGunTransform; // 枪口位置
	private LineRenderer mGunLineRenderer; // 枪口下的线渲染器
	private AudioSource mGunAudioSource; // 枪的声音源组件
	private bool mCanMove = false; // 是否可以控制移动
	private GameManager mGameManager; // 游戏管理者组件

	void Start() 
	{
		mGunTransform = transform.FindChild("Gun");
		mGunLineRenderer = mGunTransform.gameObject.GetComponent<LineRenderer>();
		mGunAudioSource = gameObject.GetComponent<AudioSource>();
		mGameManager = GameObject.Find("UI").GetComponent<GameManager>();
	}
	
	void Update() 
	{
		// 武器监听
		WeaponListener();
	}

	// 改变武器的控制状态
	public void ChangeCanMove(bool state) 
	{
		mCanMove = state;
	}

	void WeaponListener()
	{
		// 是否能够控制武器
		if (mCanMove) 
		{
			// 向鼠标箭头位置发射射线
			mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			// 检测碰射线撞，并存储射线碰撞点信息
			if (Physics.Raycast(mRay, out mHit)) 
			{
				// 手臂指向碰撞点
				transform.LookAt(mHit.point);
				// 设置线渲染器开始和结束位置
				mGunLineRenderer.SetPosition(0, mGunTransform.position);
				mGunLineRenderer.SetPosition(1, mHit.point);

				// 按下鼠标左键，并且射线碰撞到的物体是飞盘。则将飞盘击碎
				if (mHit.collider.tag == "FeiPan" && Input.GetMouseButtonDown(0)) 
				{
					// 播放枪声
					mGunAudioSource.Play();

					// 增加游戏分数
					mGameManager.AddScore();

					// 获取到飞盘碎片父物体的transform组件
					Transform parent = mHit.collider.gameObject.GetComponent<Transform>().parent;

					// 获取被撞击的所有飞盘碎片的transform组件
					Transform[] feiPans = parent.GetComponentsInChildren<Transform>();
					for (int i = 0; i < feiPans.Length; i++) 
					{
						GameObject feipan = feiPans[i].gameObject;
						// 给每一个飞盘碎片添加一个刚体
						feipan.AddComponent<Rigidbody>();
					}
					
					// 2秒后销毁飞盘
					GameObject.Destroy(parent.gameObject, 2);
				}
			}
		}
	}
}
