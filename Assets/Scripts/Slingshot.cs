using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {
	public GameObject 		launchPoint;

	void Awake() {
		Transform  launchPointTrans = transform.Find ("LaunchPoint");
		launchPoint = launchPointTrans.gameObject;
		launchPoint.SetActive(false);
	}

	void OnMouseEnter() {
//		print ("Slingshot:OnMouseEnter()");
		launchPoint.SetActive (true);

	}

	void OnMouseExit() {
//		print ("Slingbot:OnMouseExit()");
		launchPoint.SetActive(false);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
