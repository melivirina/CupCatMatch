using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager2 : MonoBehaviour
{
	public static GUIManager2 instance;

	
	public GameObject gameOverPanel;
	public GameObject WinPanel;
	public GameObject StartPanel;
	public Text yourScoreTxt;
	public Text highScoreTxt;

	public Text scoreTxt;
	public Text moveCounterTxt;

	public Text moneyText;

	private int score, moveCounter;
	public IEnumerator Wait()
	{
		yield return new WaitForSeconds(3);
		GameManager.instance.gameOver = false;
		StartPanel.SetActive(false);
	}

	void Awake()
	{
		instance = GetComponent<GUIManager2>();
		moveCounter = 15;
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
			scoreTxt.text = "Bombs: " + score.ToString() + "/3";
			if (score >= 3)
			{
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
			int Score = score;
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
		yield return new WaitUntil(() => !BoardManager2.instance.IsShifting);
		yield return new WaitForSeconds(.05f);
		if (score >= 3)
			GameOver();
		else
			Win();
	}

}
