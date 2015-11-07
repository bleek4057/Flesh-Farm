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

    //Human Spawning
    public int initHumans;
    public GameObject humanPrefab;
    private GameObject abductionChamber;

    //Humans
    private List<GameObject> humans;
    public GameObject selectedHuman;
    private bool humanSelected;

    void Awake()
    {
        ground = GameObject.FindGameObjectWithTag("Ground");
        abductionChamber = GameObject.FindGameObjectWithTag("AbductionChamber");
    }
	void Start () {
        humans = new List<GameObject>();
        SpawnInitHumans();
	}
    public bool SetSelectedHuman(GameObject human)
    {
        //returns bool based on whether the human was successfully selected
        if(human == null || human == selectedHuman){
            humanSelected = false;
            selectedHuman = null;
            return false;
        }  
        selectedHuman = human;
        return true;
    }
    void SpawnInitHumans()
    {
        for (int i = 0; i < initHumans; i++ )
        {
            //Close to the center of the spawning pen
            Vector3 spawnPos = new Vector3(abductionChamber.transform.position.x + (Random.Range(-1.5f, 1.5f)), 1, abductionChamber.transform.position.z + (Random.Range(-1.5f, 1.5f)));
            humans.Add(Instantiate(humanPrefab, spawnPos, Quaternion.identity) as GameObject);
        }
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
                        clickLoc.y = 0;

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
