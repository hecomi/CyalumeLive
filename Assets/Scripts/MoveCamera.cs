using UnityEngine;
using System.Collections;
 
public class MoveCamera : MonoBehaviour 
{
	public float speed = 10.0f;
	public float rotateSpeed = 10.0f;

	void Update () 
	{
		if (Input.GetKey(KeyCode.W)) {
			transform.position += speed * transform.forward * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.S)) {
			transform.position -= speed * transform.forward * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.A)) {
			transform.position -= speed * transform.right * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.D)) {
			transform.position += speed * transform.right * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.Q)) {
			transform.Rotate(- rotateSpeed * Vector3.up * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.E)) {
			transform.Rotate(rotateSpeed * Vector3.up * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.Z)) {
			transform.position -= speed * Vector3.up * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.X)) {
			transform.position += speed * Vector3.up * Time.deltaTime;
		}
	}
}