using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    //Buildings iformation
    private List<GameObject> buildings;
    private int buildingCount;

    //Prefabs
    public GameObject boxHab;
    public GameObject mediumHab;
    public GameObject largeHab;


    //Placing buildings
    public float gridSquareSize;
    private Vector3 placementPos;
    private GameObject habToSpawn;//The building currently being placed
    private bool placingBuilding;
    private int currentSpawnType;

    //Environment
    private GameObject ground;

    void Awake()
    {
        ground = GameObject.FindGameObjectWithTag("Ground");
    }
	void Start () {
	
	}
	
	void Update () {
        //If we have selected a building type to place.....
	    if(placingBuilding){
            //..and if we click....
            if(Input.GetMouseButtonDown(0)){
                RaycastHit hit;
                //..find the position of the mouse click
                if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000)){
                    if(hit.collider.gameObject.tag == "Ground"){
                        Vector3 clickLoc = hit.point;

                        //round out clickLoc values
                        clickLoc.x = Mathf.Round(clickLoc.x);
                        clickLoc.z = Mathf.Round(clickLoc.z);

                        SpawnHab(currentSpawnType, clickLoc);
                        placingBuilding = false;
                    }
                }
            }
        }
	}

    public void PlaceHab(int habType)
    {
        currentSpawnType = habType;
        placingBuilding = true;
    }
    private void SpawnHab(int habType, Vector3 spawnPos)
    {
        switch (habType)
        {
            case Constants.BOX_HAB:
                habToSpawn = Instantiate(boxHab, spawnPos, Quaternion.identity) as GameObject;
                break;
            case Constants.MED_HAB:
                habToSpawn = Instantiate(mediumHab, spawnPos, Quaternion.identity) as GameObject;
                break;
            case Constants.LARGE_HAB:
                habToSpawn = Instantiate(largeHab, spawnPos, Quaternion.identity) as GameObject;
                break;
            default:
                print("Spawning hab parameter ate shit.");
                break;
        }
    }
}
