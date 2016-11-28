using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoundCtrl : MonoBehaviour {

	public GameObject[] players; 
	public Transform[] originPositions;
	List<string> playerDead;
	bool roundFinished;
	int roundCount;
	// Use this for initialization
	void Start () {
		playerDead = new List<string>();
		roundCount = 0;
	}
	
	// Update is called once per frame
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
				} else
				{
					Debug.Log ("Round winner: " + CalculateRoundWinner());
					NewRoundStart ();
				}
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
		
	}
}