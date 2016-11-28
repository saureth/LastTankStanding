using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class RoundCtrl : MonoBehaviour {

	public GameObject[] players; 
	public Transform[] originPositions;
	public int helpLife;
	List<string> playerDead;
	bool roundFinished;
	int roundCount;


	void Start () {
		playerDead = new List<string>();
		roundCount = 0;
	}

	void Update () {
		for (int i = 0; i < players.Length; i++) {
			if (!players[i].activeSelf&&!playerDead.Contains(players[i].name)) {
				playerDead.Add (players[i].name);
			}
			if (playerDead.Count == (players.Length-1)) {
				Debug.Log ("Round finished");
				roundCount++;
				roundFinished = true;
				if (roundCount > 2) {
					Debug.Log ("Game finished");
					FinishTheGame ();
				} else	{
					string winner = CalculateRoundWinner ();
					Debug.Log ("Round winner: " + winner );
					HelpTheLoser (winner);
					NewRoundStart ();
				}
			}
		}
	}

	public void HelpTheLoser(string winner){
		for (int i = 0; i < players.Length; i++) {
			if (!players [i].name.Equals (winner)) {
				players [i].GetComponent<LifeBarCtrl> ().maxLife += helpLife;
			} 
			else {
				players [i].GetComponent<LifeBarCtrl> ().maxLife -= helpLife;
			}
		}
	}


	public string CalculateRoundWinner(){
		string winner = "NONE";
		for (int i = 0; i < players.Length; i++) {
			if (players[i].activeSelf) {
				winner = players [i].name;
			}
		}
		return winner;
	}

	public void NewRoundStart(){
		playerDead = new List<string>();
		roundFinished = false;
		for (int i = 0; i < players.Length; i++) {
			MoveToOriginPosition (players[i], i );
			ResetLife (players[i]);
			ResetBullets (players[i]);
			ResetTimeStatistic(players[i]);
			players [i].SetActive (true);
		}

	}

	public void MoveToOriginPosition(GameObject player, int valueOnTheArray){
		player.transform.position = originPositions [valueOnTheArray].position;
	}

	public void ResetLife(GameObject player){
		player.GetComponent<LifeBarCtrl> ().ResetStat ();
	}

	public void ResetBullets(GameObject player){
		player.GetComponent<PlayerShoot> ().bullets = player.GetComponent<PlayerShoot> ().maxBullets;
	}

	public void FinishTheGame(){
		SceneManager.LoadScene ("World");
	}

	public void ResetTimeStatistic(GameObject player){
		GameObject statCtrl = GameObject.FindWithTag ("StatCtrl");
		for (int i = 0; i < statCtrl.GetComponent<StatCtrl> ().playerIsAlive.Length; i++) {
			statCtrl.GetComponent<StatCtrl> ().playerIsAlive [i] = true;
		}
	}
}