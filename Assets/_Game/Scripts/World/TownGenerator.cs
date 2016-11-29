using UnityEngine;
using System.Collections;

public class TownGenerator : MonoBehaviour {

	public int[,] town;
	[Header("Town Dimension")]
	public int xSize;
	public int ySize;
	[Header("Block Dimension")]
	public int minXSize;
	public int minYSize;
	[Header("Size for check if it could become a block")]
	public int maxXSizeForRandom;
	public int maxYSizeForRandom;
	public float notMakingBlockProbability;
	[Header("Map stuff")]
	public GameObject[] buildings;
	public float buildingSize;
	public Color[] colors;


	/* 0 = Nothing
	 * 1 = Road
	 * 2 = Building
	*/


	void Start () {
		town = new int[xSize, ySize];
		FillWithZero ();

		MakeRoadInBlock (10,10);

		MakeMap ();
	
	}

	// Params: Position of one cell in the block
	void MakeRoadInBlock (int xPos, int yPos) {
		int[] countX = CountX (xPos, yPos); 
		int[] countY = CountY (xPos, yPos);

		// Check for Block dimension
		if ((countX [0] + countX [1] + 1) <= minXSize || (countY [0] + countY [1] + 1) <= minYSize) {
			return;
		}
		// Check for Random block creation dimension
		if ((countX [0] + countX [1] + 1) <= maxXSizeForRandom || (countY [0] + countY [1] + 1) <= maxYSizeForRandom) {
			float random = Random.value;
			if (random < notMakingBlockProbability) {
				return;
			}
		}

		// Block Bounds
		int[] tlCornerPos = new int[2];
		tlCornerPos [0] = xPos - countX[1];
		tlCornerPos [1] = yPos - countY [1];
		int[] brCornerPos = new int[2];
		brCornerPos [0] = xPos + countX[0]; 
		brCornerPos [1] = yPos + countY[0];

		// Get the intersect position for the roads
		int[] point = RandomPositionInBounds (tlCornerPos, brCornerPos);
		// Create a road to all directions from that point
		MakeRoadFromPoint (point);

		// Recursive block creation
		MakeRoadInBlock (point[0] -1, point[1] +1);
		MakeRoadInBlock (point[0] +1, point[1] +1);
		MakeRoadInBlock (point[0] -1, point[1] -1);
		MakeRoadInBlock (point[0] +1, point[1] -1);

	}

	void MakeRoadFromPoint (int[] point) {
		for (int i = point[0] +1; i < xSize; i++) {
			if (town [i, point [1]] == 0)
				town [i, point [1]] = 1;
			else
				break;
		}
		for (int i = point[0] -1; i >= 0 ; i--) {
			if (town [i, point [1]] == 0)
				town [i, point [1]] = 1;
			else
				break;
		}
		for (int i = point[1] +1; i < ySize; i++) {
			if (town [point[0], i] == 0)
				town [point[0], i] = 1;
			else
				break;
		}
		for (int i = point[1] -1; i >= 0 ; i--) {
			if (town [point[0], i] == 0)
				town [point[0], i] = 1;
			else
				break;
		}
		town [point[0], point[1]] = 1;
	}

	int[] RandomPositionInBounds (int[] xCorner, int[] yCorner) {
		int[] pos = new int[2];
		pos [0] = Random.Range (xCorner[0] +1, yCorner[0] -1);
		pos [1] = Random.Range (xCorner[1] +1, yCorner[1] -1);
		return pos;
	}

	int[] CountX (int xPos, int yPos) {
		int[] count = new int[2];
//		if (xPos >= xSize || xPos < 0 || yPos >= ySize || yPos < 0)
//			return count;
		if (town [xPos, yPos] == 1)
			return count;
		// Count to the right
		if (xPos +1 <= xSize)
			for (int i = xPos +1; i < xSize; i++) {
				if (town [i, yPos] == 0)
					count[0]++;
			}
		// Count to the left
		if (xPos -1 >= 0)
			for (int i = xPos -1; i >= 0; i--) {
				if (town [i, yPos] == 0)
					count[1]++;
			}

		return count;
	}

	int[] CountY (int xPos, int yPos) {
		int[] count = new int[2];
//		if (xPos >= xSize || xPos < 0 || yPos >= ySize || yPos < 0)
//			return count;
		if (town [xPos, yPos] == 1)
			return count;
		// Count to the bottom
		if (yPos +1 <= ySize)
		for (int i = yPos +1; i < ySize; i++) {
			if (town [xPos, i] == 0)
				count[0]++;
		}
		// Count to the top
		if (yPos -1 >= 0)
			for (int i = yPos -1; i >= 0; i--) {
				if (town [xPos, i] == 0)
					count[1]++;
			}

		return count;
	}

	void FillWithZero () {
		for (int i = 0; i < xSize; i++) {
			for (int j = 0; j < ySize; j++) {
				town [i, j] = 0;
			}
		}
	}

	void MakeMap () {
		StartCoroutine (waitForSeconds());
		for (int i = 0; i < xSize; i++) {
			for (int j = 0; j < ySize; j++) {
				if (town [i, j] == 0) {
					Vector3 pos = new Vector3 (i * buildingSize, 0, j * buildingSize);
					GameObject b = Instantiate (buildings[Random.Range(0, buildings.Length)], pos, Quaternion.Euler(-90, 0, 0)) as GameObject;
					town [i, j] = 2;

					if (i+1 < xSize)
						if (town [i + 1, j] == 1) {
							b.transform.Rotate (0, 0, 90);
						} 
					if (i-1 > 0)
						if (town [i - 1, j] == 1) {
							b.transform.Rotate (0, 0, 270);
						}
					if (j-1 > 0)
						if (town [i, j - 1] == 1) {
							b.transform.Rotate (0, 0, 180);
						}

					b.GetComponent<MeshRenderer> ().material.SetColor ("_Color", colors[Random.Range(0, colors.Length)]);
				}
			}
		}
	}

	IEnumerator waitForSeconds () {
		yield return new WaitForSeconds (3);
	}
}
