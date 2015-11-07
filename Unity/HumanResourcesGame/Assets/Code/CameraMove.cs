using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	private bool middleMouseDown;
	public float rotateDamp;
	public float zoomDamp;
	public float fOVDamp;
	public float moveXDamp;
	public float moveZDamp;
	public Camera cam;


	public Quaternion p0LastRot, p1LastRot;
	private Quaternion currentPlayerLastRot;
	public float endTurnRotSpeed;

	private bool rotatingToNewPos;

	void Start () {

    }
	void Update () {
		ZoomInOut();
		RotateAroundY();
		RotateUpdate();
		Move();
	}
	void LookAt(Vector3 target){

	}
	void RotateAroundY(){
		if(RightMouseDown()){
			transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Horizontal") * rotateDamp * Time.deltaTime);
		}
	}
	void AutoRotateAroundY(float speed){
		transform.RotateAround(transform.position, Vector3.up, speed * rotateDamp * Time.deltaTime);
	}
	void MoveTo(Vector3 targetPos){

	}
	void Move(){
		if(MiddleMouseDown()){
			transform.Translate(new Vector3(Input.GetAxis("Mouse X") * moveXDamp * Time.deltaTime * -1,0,0), Space.Self);
			transform.Translate(new Vector3(0,0,Input.GetAxis("Mouse Y") * moveZDamp * Time.deltaTime * -1), Space.Self);
		}
	}
	void ZoomInOut(){
		transform.Translate(transform.forward * (Input.GetAxis("Mouse ScrollWheel") * zoomDamp * Time.deltaTime));
		if(cam.fieldOfView >= 10){
			cam.fieldOfView -= (Input.GetAxis("Mouse ScrollWheel") * fOVDamp);
		}
	}
	bool MiddleMouseDown(){
		return(Input.GetMouseButton(2));
	}
	bool RightMouseDown(){
		return(Input.GetMouseButton(1));
	}
	
	
	private void RotateUpdate(){
		if(rotatingToNewPos){
			if(Mathf.Abs(transform.localEulerAngles.y - currentPlayerLastRot.y) <= 10){
				transform.localRotation = currentPlayerLastRot;
				rotatingToNewPos = false;
			}
			AutoRotateAroundY(endTurnRotSpeed);
		}
	}
}
