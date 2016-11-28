using UnityEngine;
using System.Collections;

public class StatCtrl : MonoBehaviour {

	public GameObject[] players;
	public GameStatistics [] playerStat;
	public bool[] playerIsAlive; 
	float time;

	void Start () {
		playerStat = new GameStatistics[players.Length];
		playerIsAlive = new bool[players.Length];
		for (int i = 0; i < playerStat.Length; i++) {
			playerStat [i] = new GameStatistics ();
			playerStat [i].setPlayerName (players[i].name);
			playerIsAlive [i] = true;
			Debug.Log (""+playerStat [i].getPlayerName());
			time = 0.0f;
		}
	}

	void Update () {
		for (int i = 0; i < playerStat.Length; i++) {
			if (playerIsAlive[i]) {
				time = playerStat [i].getTotalTimeAlive ();
				time += Time.deltaTime;
				playerStat [i].setTotalTimeAlive (time);
			}
		}
	}
}