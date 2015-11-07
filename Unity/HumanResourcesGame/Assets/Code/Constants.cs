using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Constants : MonoBehaviour {
	//needed arrays/lists
	private static List<GameObject[]> doodadsList;
	private static List<Material> texturesList;

    //Building sizes
    public const int BOX_HAB = 0;
    public const int MED_HAB = 1;
    public const int LARGE_HAB = 2;

	//Resources:
	//common
	public const int SAND = 0;
	public const int WOOD = 1;
	public const int GOLD = 2;
	
	//rare
	public const int FRANCIUM = 3;
	
	//exotic
	public const int TEARS = 4;

	//Textures:
	//common
	public Material sandMaterial;
	public Material woodMaterial;
	public Material goldMaterial;

	//rare
	public Material franciumMaterial;

	//exotic
	public Material tearsMaterial;

	//Doodads:
	//common
	public GameObject[] sandDoodads;
	public GameObject[] woodDoodads;
	public GameObject[] goldDoodads;

	//rare
	public GameObject[] franciumDoodads;

	//exotic
	public GameObject[] tearsDoodads;

	void Awake(){
		//populate
		texturesList = new List<Material>{sandMaterial, woodMaterial, goldMaterial, franciumMaterial, tearsMaterial};
		doodadsList = new List<GameObject[]>{sandDoodads, woodDoodads, goldDoodads, franciumDoodads, tearsDoodads};
	}

	void Start () {
		
	}
	
	void Update () {
	
	}

	//takes resource Id number and returns coressponing texture and doodads[]
	public ArrayList GetResourceInfo(int resourceId) {
		ArrayList resourceInfo = new ArrayList ();

		resourceInfo.Add(texturesList [resourceId]);
		resourceInfo.Add(doodadsList [resourceId]);

		return resourceInfo;
	}
}