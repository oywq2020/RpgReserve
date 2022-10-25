using UnityEngine;
using System.Collections;

public class Crosshairs : MonoBehaviour {

	public LayerMask targetMask;
	public SpriteRenderer dot;
	public Color dotHighlightColour;
	Color originalDotColour;

	void Start() {
		Cursor.visible = false;
		originalDotColour = dot.color;
	}
	
	void FixedUpdate () {
		transform.Rotate (Vector3.forward * -40 * Time.deltaTime);
	}

	public void DetectTargets(Ray ray) {
		if (Physics.Raycast (ray, 100)) {
			dot.color = dotHighlightColour;
		} else {
			dot.color = originalDotColour;
		}
	}
}
