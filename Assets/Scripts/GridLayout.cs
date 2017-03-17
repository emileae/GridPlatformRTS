using UnityEngine;
using System.Collections;

public class GridLayout : MonoBehaviour {

	public int gridLength = 40;

	public int yPos = 1;

	// Foundation prefabs
	public GameObject blankFoundation;
	public GameObject shoreFoundation;
	public GameObject wellFoundation;
	public GameObject gardenFoundation;
	public GameObject houseFoundation;
	public GameObject lighthouseFoundation;

	// Resource prefabs
	public GameObject tree;
	public GameObject rock;
	public GameObject grass;

	// Base / Ground prefab
	public GameObject baseObject;


	// Use this for initialization
	void Start () {
		LayoutGrid ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void LayoutGrid ()
	{
		// TODO: update this to use Perlin noise or something other than pure randomness to make resource distribution more natural looking

		int i = 0;
		while (i < gridLength) {
			Vector3 position = new Vector3 (i, yPos, 0);
			int gridNum = Random.Range (0, 8);

			int foundationWidth = 0;

			// TODO: detect if its the last unit and add shore to end... not always added if units extend beyond the gridLength

			// offset foundation base by 0.5 * its width, to align properly
			if (i == 0) {
				Debug.Log ("Shoreline @ index 0");
				foundationWidth = 1;
				Instantiate (shoreFoundation, position + new Vector3(0.5f * foundationWidth, 0, 0), Quaternion.identity);
//				GameObject go = Instantiate (shoreFoundation, position, Quaternion.identity) as GameObject;
//				Foundation foundationScript = go.GetComponent<Foundation>();
//				i += 1;
			} else {
				if (gridNum >= 4) {
					foundationWidth = 1;
					Vector3 foundationPos = position + new Vector3(0.5f * foundationWidth, 0, 0);
					Instantiate (blankFoundation, foundationPos, Quaternion.identity);
					// only on blank units can there be resources
					int resourceProbability = Random.Range (0, 9);
					if (resourceProbability < 6) {
						if (resourceProbability < 2) {
							Instantiate (tree, foundationPos, Quaternion.identity);
						}else if(resourceProbability > 2 && resourceProbability < 4){
							Instantiate (rock, foundationPos, Quaternion.identity);
						}else if(resourceProbability > 4 && resourceProbability < 6){
							Instantiate (grass, foundationPos, Quaternion.identity);
						}	
					}
//					i += 1;
				} else if (gridNum == 0) {
					foundationWidth = 3;
					Instantiate (wellFoundation, position + new Vector3(0.5f * foundationWidth, 0, 0), Quaternion.identity);
//					i += 3;
				} else if (gridNum == 1) {
					foundationWidth = 4;
					Instantiate (gardenFoundation, position + new Vector3(0.5f * foundationWidth, 0, 0), Quaternion.identity);
//					i += 4;
				} else if (gridNum == 2) {
					foundationWidth = 3;
					Instantiate (houseFoundation, position + new Vector3(0.5f * foundationWidth, 0, 0), Quaternion.identity);
//					i += 3;
				} else if (gridNum == 3) {
					foundationWidth = 3;
					// add 1 units to the x position to center the triple unit
					Instantiate (lighthouseFoundation, position + new Vector3(0.5f * foundationWidth, 0, 0), Quaternion.identity);
//					i += 3;// triple unit so it occupies 3 grid spaces
				}
			}

			i += foundationWidth;

		}

		// add final shore after all foundations are added
		Vector3 endShorePos = new Vector3 (i, yPos, 0);
		Debug.Log("new grid length: " + i);
		Instantiate (shoreFoundation, endShorePos + new Vector3(0.5f * 1, 0, 0), Quaternion.identity);
		i += 1;// add the extra shore unit and use i to scale the base

		// update base ground to fit the new grid length
		baseObject.transform.localScale += new Vector3 (i, 0, 0);
		baseObject.transform.Translate (new Vector3 (0.5f * i, 0.5f * yPos, 0));

	}
}
