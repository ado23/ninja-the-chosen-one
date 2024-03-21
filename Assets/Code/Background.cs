using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	public Transform Background1;
	public Transform Background2;

	public bool Wichone = true;

	public Transform cam;

	private float currentHeight = 3;

	void Update()
	{
		if (currentHeight < cam.position.x + 6) {
		
			if (Wichone)
				Background1.localPosition = new Vector3 (8, Background1.localPosition.x + 6, -10);
			else
				Background2.localPosition = new Vector3 (8, Background2.localPosition.x + 6, -10);

			currentHeight += 3;
			Wichone = !Wichone;

		}

		if (currentHeight < cam.position.x + 6) {

			if (currentHeight < cam.position.x) {

				if (Wichone)
					Background2.localPosition = new Vector3 (8, Background2.localPosition.x - 6, -10);
				else
					Background1.localPosition = new Vector3 (8, Background1.localPosition.x - 6, -10);

				currentHeight -= 3;
				Wichone = !Wichone;

			}
		}
			
	}

}
