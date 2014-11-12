using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {
	public GameObject		prefabProjectile;
	public float			velocityMult = 4f;
	private GameObject 		launchPoint;
	private Vector3			launchPos;
	private GameObject		projectile;
	private bool			aimingMode;


	void Awake() {
		Transform  launchPointTrans = transform.Find ("LaunchPoint");
		launchPoint = launchPointTrans.gameObject;
		launchPoint.SetActive(false);
		launchPos = launchPointTrans.position;
	}

	void OnMouseEnter() {
//		print ("Slingshot:OnMouseEnter()");
		launchPoint.SetActive (true);

	}

	void OnMouseExit() {
//		print ("Slingbot:OnMouseExit()");
		launchPoint.SetActive(false);
	}

	void OnMouseDown() {
		//The player has pressed the mouse button while over slingshot
		aimingMode = true;
		//Instantiate a Projectile
		projectile = Instantiate(prefabProjectile) as GameObject;
		//Start it at the launchPoint
		projectile.transform.position = launchPos;
		//Set it to isKinematic for now
		projectile.rigidbody.isKinematic = true;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//If Slingshot is not in aimingMode, don't run this code
		if (!aimingMode) return;
		var mouseDelta = dragSlingshot();
		mouseUp(mouseDelta);
	}

	Vector3 dragSlingshot() {
		//Get the current mouse position in 2D screen coordinates
		Vector3 mousePos2D = Input.mousePosition;

		//Convert the mouse position to 3D world coordinates
		mousePos2D.z = -Camera.main.transform.position.z;
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

		//Find the Delta from the launchPos to the mousePos3D
		Vector3 mouseDelta = mousePos3D - launchPos;

		//Limit mouseDelta to the radius of the slingshot SphereCollider
		float maxMagnitude = this.GetComponent<SphereCollider>().radius;

		if(mouseDelta.magnitude > maxMagnitude){
			mouseDelta.Normalize();
			mouseDelta *= maxMagnitude;
		}

		//Move the projectile to this new position
		Vector3 projPos = launchPos + mouseDelta;
		projectile.transform.position = projPos;
		return mouseDelta;


	}

	void mouseUp(Vector3 mouseDelta) {
		if(Input.GetMouseButtonUp(0)){
			//The mouse has been released
			aimingMode = false;
			projectile.rigidbody.isKinematic = false;
			projectile.rigidbody.velocity = -mouseDelta * velocityMult;
			projectile = null;
		}
	}
}
