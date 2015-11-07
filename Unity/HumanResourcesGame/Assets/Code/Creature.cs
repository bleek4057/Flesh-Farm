using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Creature : MonoBehaviour {

	//resource attrtibutes
	List<GameObject> resourceDoodads;
	public Material currentMat;
	private int testCount = 0;

	void Start () {
		resourceDoodads = new List<GameObject>();
		ApplyResource (0);
	}

	void Update () {

	}

	//get a resource's info based on an Id
	void ApplyResource(int resourceId){
		/*ArrayList info = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Constants>().GetResourceInfo (resourceId);

		currentMat = info [0] as Material;
		//print (currentMat.name);

		GameObject[] parse = info [1] as GameObject[];

		foreach (GameObject piece in parse) {
			resourceDoodads.Add (piece);
		}

		//Apply texture 
		Renderer render = GetComponent<Renderer> ();
		render.material = currentMat;*/
	}

}
