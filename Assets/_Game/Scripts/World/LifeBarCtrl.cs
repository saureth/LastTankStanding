using UnityEngine;
using System.Collections;

public class LifeBarCtrl : MonoBehaviour {
	
	public int howManyShootsToDie;
	public int maxLife;
	public int currentLife;
	int hitCounter;
	float lifePercentaje;
	bool isThisTankDestroyed;

	public float getLifePercentaje(){
		return lifePercentaje;
	}

	public bool getTankDestroyed(){
		return isThisTankDestroyed;
	}

	public bool TakeDamage(float damage){
		int d = Mathf.RoundToInt (damage);
		hitCounter++;
		currentLife -= d;
		currentLife = Mathf.Clamp (currentLife,0, maxLife);
		float cl = currentLife, ml = maxLife;
		float cLife = cl / ml;
		lifePercentaje = cLife;
		if (lifePercentaje<=0.0f) {
			isThisTankDestroyed = true;
			this.gameObject.SetActive (false);
		}
		return isThisTankDestroyed;
	}

	// Use this for initialization
	void Start () {
		currentLife = maxLife;
		hitCounter = 0;
		lifePercentaje = currentLife / maxLife;
		howManyShootsToDie = maxLife / 1;
		isThisTankDestroyed = false;
	}

	public void ResetStat(){
		currentLife = maxLife;
		hitCounter = 0;
		lifePercentaje = currentLife / maxLife;
		howManyShootsToDie = maxLife / 1;
		isThisTankDestroyed = false;
		this.gameObject.SetActive (true);
	}

}