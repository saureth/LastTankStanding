using UnityEngine;
using System.Collections;

public class GameStatistics {

	private string playerName;
	private int totalDamageDealt;
	private int amountOfDeaths;
	private int amountOfKills;
	private float totalSpaceTraveled;
	private float totalTimeAlive;
	private float totalAmountOfObjectsDestroyed;
	private float totalAmountOfBulletShooted;

	public GameStatistics(){
		playerName = "";
		totalDamageDealt=0;
		totalSpaceTraveled = 0.0f;
		totalTimeAlive = 0.0f;
		totalAmountOfObjectsDestroyed=0.0f;
		totalAmountOfBulletShooted = 0.0f;
	}


	public void DebugMyStatistics (){
		Debug.Log ("Statistics for: " + playerName);
		Debug.Log ("Total Damage Dealt: " + totalDamageDealt);
		Debug.Log ("Amount Of Deaths: " + amountOfDeaths);
		Debug.Log ("Amount Of Kills: " + amountOfKills);
		Debug.Log ("Total Time Alive: " + totalTimeAlive) ;
		Debug.Log ("Total Amount Of Objects Destroyed: " + totalAmountOfObjectsDestroyed) ;
		Debug.Log ("Total Amount Of Bullet Shooted: " + totalAmountOfBulletShooted) ;
	}

	public string getPlayerName(){
		return this.playerName;
	}

	public int getTotalDamageDealt(){
		return this.totalDamageDealt;
	}

	public int getAmountOfKills(){
		return this.amountOfKills;
	}

	public int getAmountOfDeaths(){
		return this.amountOfDeaths;
	}

	public float getTotalSpaceTraveled(){
		return this.totalSpaceTraveled;
	}

	public float getTotalTimeAlive(){
		return this.totalTimeAlive;
	}

	public float getTotalAmountOfObjectsDestroyed(){
		return this.totalAmountOfObjectsDestroyed;
	}

	public float getTotalAmountOfBulletShooted(){
		return this.totalAmountOfBulletShooted;
	}
		
	public void setPlayerName(string pn){
		this.playerName = pn;
	}

	public void setTotalDamageDealt(int tdd){
		this.totalDamageDealt = tdd;
	}

	public void setTotalSpaceTraveled(float tst){
		this.totalSpaceTraveled = tst;
	}

	public void setTotalTimeAlive(float tta){
		this.totalTimeAlive= tta;
	}

	public void setTotalAmountOfObjectsDestroyed(float taod){
		this.totalAmountOfObjectsDestroyed = taod;
	}

	public void setTotalAmountOfBulletShooted(float tabs){
		this.totalAmountOfBulletShooted = tabs;
	}

	public void setAmountOfDeaths(int aod){
		this.amountOfDeaths = aod;
	}

	public void setAmountOfKills(int aok){
		this.amountOfKills = aok;
	}
}