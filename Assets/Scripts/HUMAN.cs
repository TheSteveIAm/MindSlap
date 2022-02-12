using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HUMAN : MonoBehaviour {
	private CharacterController control;

	public float speed = 10f;

	private void Awake () {
		control = GetComponent<CharacterController> ();
	}

	private void Update () {
		//SCREW IT - FOR NOW

		var movement = Vector3.zero;
		if (Input.GetKey (KeyCode.W)) {
			movement += Vector3.forward;
		}
		if (Input.GetKey (KeyCode.S)) {
			movement += Vector3.back;
		}
		if (Input.GetKey (KeyCode.A)) {
			movement += Vector3.left;
		}
		if (Input.GetKey (KeyCode.D)) {
			movement += Vector3.right;
		}

		control.Move (movement * speed * Time.deltaTime);
	}

	//public void Move (InputAction.CallbackContext context) {
	//	//context.ReadValue ();
	//}
}