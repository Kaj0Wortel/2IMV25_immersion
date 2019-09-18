using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexKeyListener : MonoBehaviour {

	private boolean _SpaceDown = false;

	void Update()
	{
		if (Input.GetKeyDown("space")) {
			if (_SpaceDown) return;
			_SpaceDown = true;

			int max = 5; // TODO: get size of object
			Random.Range(0, max);
			// TODO: call texture randomization function.

		} else {
			_SpaceDown = false;
		}
	}


}
