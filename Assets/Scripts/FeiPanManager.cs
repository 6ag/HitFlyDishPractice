using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeiPanManager : MonoBehaviour {

	public GameObject mPrefabFeiPan; // 飞盘预制体
	
	// 开始定时生成飞盘
	public void StartCreateFeiPan() 
	{
		// 第一次2秒后调用，后面每隔2秒调用一次
		InvokeRepeating("CreateFeiPan", 2.0f, 2.0f);
	}

	// 停止生成飞盘
	public void StopCreateFeiPan() 
	{
		CancelInvoke("CreateFeiPan");
	}

	void CreateFeiPan() 
	{
		for (int i = 0; i < 3; i++) 
		{
			// 飞盘的位置
			Vector3 position = new Vector3(Random.Range(-6.0f, 6.0f), Random.Range(0.5f, 3.0f), Random.Range(8.0f, 18.0f));
			// 生成飞盘
			GameObject feiPan = GameObject.Instantiate(mPrefabFeiPan, position, Quaternion.identity);
			// 让生成的飞盘成为当前游戏物体的子物体
			feiPan.transform.SetParent(transform);
		}
	}

	// 移除所有飞盘
	public void RemoveFeiPan() 
	{
		Transform[] feiPans = gameObject.GetComponentsInChildren<Transform>();
		// GetComponentsInChildren会把父组件也一起返回，所以从1开始。不然会移除当前游戏对象以至于造成空指针异常
		for (int i = 1; i < feiPans.Length; i++) 
		{
			GameObject.Destroy(feiPans[i].gameObject);
		}
	}
}
