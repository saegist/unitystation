﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_PreRoundWindow : MonoBehaviour
{
	[SerializeField]
	private Text currentGameMode;
	[SerializeField]
	private Text timer;
	[SerializeField]
	private Text playerCount;

	[SerializeField]
	private GameObject adminPanel;
	[SerializeField]
	private GameObject playerWaitPanel;
	[SerializeField]
	private GameObject countdownPanel;

	private bool doCountdown;
	private float countdownTime;

	void OnDisable()
	{
		doCountdown = false;
		adminPanel.SetActive(false);
	}

	void Update()
	{
		// TODO: remove once admin system is in
		if (Input.GetKeyDown(KeyCode.F7) && !BuildPreferences.isForRelease)
		{
			adminPanel.SetActive(true);
		}

		if (doCountdown)
		{
			countdownTime -= Time.deltaTime;
			if (countdownTime <= 0)
			{
				doCountdown = false;
				// Let the players choose their job after the countdown for now
				UIManager.Display.SetScreenForJobSelect();
			}
		}

		playerCount.text = PlayerList.Instance.ClientConnectedPlayers.Count.ToString();
		currentGameMode.text = GameManager.Instance.GetGameModeName();
		timer.text = TimeSpan.FromSeconds(this.countdownTime).ToString(@"mm\:ss");
	}

	public void StartNowButton()
	{
		GameManager.Instance.RoundStart();
	}

	public void SyncCountdown(bool started, float time)
	{
		Logger.Log($"SyncCountdown called with: started={started}, time={time}", Category.Round);
		countdownTime = time;
		doCountdown = started;
		countdownPanel.SetActive(started);
		playerWaitPanel.SetActive(!started);
	}
}
