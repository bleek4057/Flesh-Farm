using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Constants : MonoBehaviour {
	//needed arrays/lists
	private static GameObject[] doodads;

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
    public Material[] materials;
    /*Common
     0 - Sand
     * 1 - Wood
     * 2 - Gold
     * 
     Rare
     3 - Francium
     * 
     Exotic
     4 - Tears
     */

	//Doodads:
	//common
	public GameObject sandDoodad;
	public GameObject woodDoodad;
	public GameObject goldDoodad;

	//rare
	public GameObject franciumDoodad;

	//exotic
	public GameObject tearsDoodad;

    //Resource revenue rates
    public int[] resourceRevenueRates;//How much of a bonus each material gets to research points per cycle

	void Awake(){
		//populate
		doodads = new GameObject[]{sandDoodad, woodDoodad, goldDoodad, franciumDoodad, tearsDoodad};
	}

	void Start () {
		
	}
	
	void Update () {
	
	}
    public Material GetMat(int resourceId)
    {
        return materials[resourceId];
    }
    public GameObject GetDoodad(int resourceId)
    {
        return doodads[resourceId];
    }
	//takes resource Id number and returns coressponing texture and doodads[]
	/*public ArrayList GetResourceInfo(int resourceId) {
		ArrayList resourceInfo = new ArrayList ();

		resourceInfo.Add(texturesList [resourceId]);
		resourceInfo.Add(doodadsList [resourceId]);

		return resourceInfo;
	}*/
}