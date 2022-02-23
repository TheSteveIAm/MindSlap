using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FENCER : MonoBehaviour {
	public float swordHeight = 0f;
	public float maxDistance = 5f;
	private Rigidbody body;

	public HUMAN player;

	[Range (0.05f, 0.25f)]
	public float lerpPercent = 0.15f;

	public float smoothTime = 0.3f;

	private Vector3 velocity;

	[Range (0.1f, 2f)]
	public float throwPower = 1f;

	// Start is called before the first frame update
	private void Awake () {
		body = GetComponent<Rigidbody> ();
		UpdatePositionAndRotation ();
	}

	private void FixedUpdate () {
		UpdatePositionAndRotation ();
	}

	private Vector3 debugPos;

	private void UpdatePositionAndRotation () {
		var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit)) {
			var newPos = Vector3.zero;
			var lerpDistance = lerpPercent;
			var distance = Vector3.Distance (player.transform.position, body.position);

			var playerToMouseRay = new Ray (player.transform.position, (hit.point - player.transform.position).normalized);
			RaycastHit hit2;

			if (Physics.Raycast (playerToMouseRay, out hit2, maxDistance)) {
				newPos = hit2.point;
			} else {
				newPos = hit.point;
			}
			newPos.y = swordHeight;
			debugPos = newPos;

			if (distance > maxDistance) {
				newPos = newPos - (newPos / maxDistance);
			}

			//smoothTime = Mathf.Clamp (1 - (1 / distance), 0.2f, 0.3f);
			//Vector3.SmoothDamp
			//var lerpedPos = Vector3.Lerp (body.position, newPos, lerpDistance);
			var smoothPos = Vector3.SmoothDamp (body.position, newPos, ref velocity, smoothTime);

			body.MovePosition (smoothPos);
		}
	}

	private void OnDrawGizmos () {
		Gizmos.DrawWireSphere (player.transform.position, maxDistance);

		Gizmos.color = Color.red;
		Gizmos.DrawSphere (debugPos, 0.25f);
	}
}

//[CustomEditor (typeof (FENCER))]
//public class FencerEditor : Editor {
//	public void OnSceneGUI () {
//		var t = target as FENCER;
//		var tr = t.transform;
//		var pos = tr.position;
//		// display an orange disc where the object is
//		var color = new Color (1, 0.8f, 0.4f, 1);
//		Handles.color = color;
//		Handles.DrawWireDisc (t.player.transform.position, tr.up, t.maxDistance, 5f);
//	}
//}