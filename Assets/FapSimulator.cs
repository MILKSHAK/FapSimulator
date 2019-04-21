using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FapSimulator : MonoBehaviour
{
	int totalXiaoming = 10000;
	int timePeriod = 20;
	int fapPercentage = 3;
	int fapCD = 720;
	int totalHealth = 100;
	int fapDamge = 1;
	int timeLast;
	int timeLastFromLastFap = 0;
	int currentHealth = 0;
	Dictionary<int, int> xiaomingsRecords;

	public Button startButton;

	public void StartFap()
	{
		StartCoroutine(FapCoroutine());
	}
	
	private IEnumerator FapCoroutine()
	{
		startButton.interactable = false;
		xiaomingsRecords = new Dictionary<int, int>();
		for (int i = 1; i <= totalXiaoming; i++)
		{
			fapDamge = 1;
			timeLast = 0;
			timeLastFromLastFap = 0;
			currentHealth = totalHealth;
			Debug.Log(string.Format("小明{0}号上线 ！", i));
			while (currentHealth > 0)
			{
				timeLast += timePeriod;
				timeLastFromLastFap += timePeriod;
				if (Random.Range(0, 100) < fapPercentage)
				{
					timeLastFromLastFap = 0;
					currentHealth = currentHealth - fapDamge;
					fapDamge = fapDamge + 1;
				}
				else if (timeLastFromLastFap >= fapCD)
				{
					currentHealth = totalHealth;
					fapDamge = 1;
				}
			}
			xiaomingsRecords.Add(i, timeLast);
			System.TimeSpan timeSpan = System.TimeSpan.FromMinutes(timeLast);
			Debug.Log(string.Format("小明{0}号手冲而亡 ！他活了{1}年{2}月{3}日{4}时{5}分", i, (int)(timeSpan.TotalDays / 365), (int)((timeSpan.TotalDays % 365) / 30), (int)(timeSpan.TotalDays % 30), timeSpan.Hours, timeSpan.Minutes));
			yield return null;
		}
		long timeTotal = 0;
		foreach (int t in xiaomingsRecords.Values)
		{
			timeTotal += t;
		}
		long timeAverage = timeTotal / xiaomingsRecords.Count;
		System.TimeSpan averagetimeSpan = System.TimeSpan.FromMinutes(timeAverage);
		Debug.Log(string.Format("小明最后平均寿命{0}年{1}月{2}日{3}时{4}分", (int)(averagetimeSpan.TotalDays / 365), (int)((averagetimeSpan.TotalDays % 365) / 30), (int)(averagetimeSpan.TotalDays % 30), averagetimeSpan.Hours, averagetimeSpan.Minutes));

		startButton.interactable = true;
	}
}
