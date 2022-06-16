using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GUIManager4 : MonoBehaviour
{
	public static GUIManager4 instance;

	public GameObject gameOverPanel;
	public GameObject WinPanel;
	public GameObject StartPanel;
	public Text yourScoreTxt;
	public Text highScoreTxt;

	public Text scoreTxt;
	public Text scoreTxt2;
	public Text moveCounterTxt;

	public Text moneyText;

	private int score, score2, moveCounter, i, i2;

	public IEnumerator Wait()
	{
		yield return new WaitForSeconds(3);
		GameManager.instance.gameOver = false;
		StartPanel.SetActive(false);
	}

	void Awake()
	{
		instance = GetComponent<GUIManager4>();
		moveCounter = 12;
		GameManager.instance.gameOver = true;
		StartPanel.SetActive(true);

		StartCoroutine(Wait());
	}

	public void GameOver()
	{
		
		GameManager.instance.gameOver = true;
		gameOverPanel.SetActive(true);
		Money.i = Money.i + moveCounter;
		moneyText.text = Money.i.ToString();
		
		if (score > PlayerPrefs.GetInt("HighScore"))
		{
			PlayerPrefs.SetInt("HighScore", score);
			highScoreTxt.text = "New Best: " + PlayerPrefs.GetInt("HighScore").ToString();
		}
		else
		{
			highScoreTxt.text = "Best: " + PlayerPrefs.GetInt("HighScore").ToString();
		}

		yourScoreTxt.text = "You're WIN";

	}
	
	public void Win()
	{
		GameManager.instance.gameOver = true;
		WinPanel.SetActive(true);
	}

	public int Score
	{
		get
		{
			return score;
		}

		set
		{
			score = value;
			scoreTxt.text = "Cats: " + score.ToString() + "/5";
			if (score >= 5)
			{
				i2 = 1;
				Debug.Log("SCORE2  " + score2);
				if (score2 >= 900)
                {
					Debug.Log("SCORE222  " + score2);
					GameOver();
				}
				
			}
		}
	}
	public int Score2
	{
		get
		{
			return score2;
		}

		set
		{
			if (score2 < 900 && score2 >= 0)
			{
				score2 = value;
				scoreTxt2.text = score2.ToString() + "/900";
			}
			if (score2 >= 1000)
			{
				i = 1;
				score2 = -1000;
				scoreTxt2.text = "900/900";
				Debug.Log("SCORE2  " + score2);
				if (score >= 5)
				GameOver();
			}
		}
	}


	public int MoveCounter
	{
		get
		{
			return moveCounter;
		}

		set
		{
			moveCounter = value;
			if (moveCounter <= 0)
			{
				moveCounter = 0;
				StartCoroutine(WaitForShifting());
				moveCounterTxt.text = moveCounter.ToString();
			}
			moveCounterTxt.text = moveCounter.ToString();
		}
	}

	private IEnumerator WaitForShifting()
	{
		yield return new WaitUntil(() => !BoardManager4.instance.IsShifting);
		yield return new WaitForSeconds(.05f);
		if (score >= 5 && score2 >= 900)
			GameOver();
		else
			Win();
	}

}
