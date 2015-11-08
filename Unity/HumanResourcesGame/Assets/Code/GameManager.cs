using UnityEngine;
using UnityEngine.UI;
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
    public GameObject infuserPrefab;
    public GameObject masherPrefab;


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

    //Resources

    //Revenue
    public int money;
    public int moneyPerMinute;

    public Building scrolledOverBuilding;

    //Mashing
    public Masher scrolledOverMasher;

    //Infusinig
    public Infuser scrolledOverInfuser;
	private Infuser stupidNeededInfuser;

    //Tutorial
    private int currentTipNumber = 0;
    /*0 - Welcome to your new human factory sir! This is your abduction pen! These are your humans. You're job is to oversee the research on these earthlings for our alien empire!
     1 - Your humans need to live in habitats. Click on the build button to buy a habitat. Then click on the terrain to place it
     2 - Now that you've built your building, click on a human to select it
     3 - Now right click on your building to place your human in it. Your human is now generating income. Hover over the building and click on the button to collect the income
     4 - We need more humans. Build a people-masher and place two humans inside. It will mash them together and make a baby. The humans might survive.. might.
     5 - You're doing great! Here are some resources! You can combine people with resources to give them special properties! */

    //UI Elements
    public Text moneyText;
    public Text selectedHumanText;
    public Text selectedHumanResources;
    public GameObject buildPanel;

    public GameObject scrollOverPanel;
    public GameObject scrollOverText;
    public GameObject collectMoneyButton;
    public GameObject collectMoneyButtonText;
	public GameObject infuserPanel;

    public RectTransform tutorialPanel;
    public Text tutorialText;
    //public Text tutorialButton;

    public string[] tutorialTips;

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
        humanSelected = true;
        selectedHuman = human;
        return true;
    }
    public void NextTutorialTip()
    {
        currentTipNumber++;
    }
    void CheckTutorialConditions()
    {
        switch(currentTipNumber){
            case 0:
                //Welcome, these are humans
                MoveTutorialText(abductionChamber.transform.position);
                tutorialText.text = "Welcome to Earthling Research Base 9 sir!\nHere we conduct tests on humans. A new batch has just arrived in the abduction pen! Build a habiat to move them into so we can start generating research points!";
                break;
            case 1:
                //Build a hab
                    MoveTutorialText(buildPanel.transform.position);
                break;
            case 2:
                //Now place a human in the hab
                if (buildingCount > 0)
                {
                    MoveTutorialText(abductionChamber.transform.position);
                }
                break;
            case 3:
                GameObject building = GameObject.FindGameObjectWithTag("Building");
                MoveTutorialText(building.transform.position);
                break;
            case 4:
                //MoveTutorialText();
                break;
            case 5:
                //MoveTutorialText();
                break;
            default:
                tutorialText.GetComponent<Text>().text = "Shits broke son.";
                break;
        }
    }
    void MoveTutorialText(Vector3 newLoc)
    {
        tutorialPanel.anchoredPosition3D = newLoc;
    }
    void MoveTutorialText(Vector2 newLoc)
    {
        tutorialPanel.position = newLoc;
    }
    void UpdateUIText()
    {
        CheckTutorialConditions();
        moneyText.text = "$ " + money;
        if(humanSelected){
            selectedHumanText.text = selectedHuman.name;
            selectedHumanResources.text = selectedHuman.GetComponent<Creature>().resourcesString;
        }
        else
        {
            selectedHumanResources.text = "";
            selectedHumanText.text = "";
        }
        
        if(scrolledOverBuilding != null){
            scrollOverText.GetComponent<Text>().text = scrolledOverBuilding.gameObject.name;
            collectMoneyButtonText.GetComponent<Text>().text = scrolledOverBuilding.money + "";
        }
        if (scrolledOverMasher != null)
        {
            scrollOverText.GetComponent<Text>().text = "People Masher";

            print(scrolledOverMasher.humanCount);
            if (scrolledOverMasher.humanCount == scrolledOverMasher.humanCapacity)
            {
                collectMoneyButton.gameObject.SetActive(true);
                collectMoneyButtonText.GetComponent<Text>().text = "Mash!";
            }
            else
            {
                collectMoneyButton.gameObject.SetActive(false);
            }
        }
        if (scrolledOverInfuser != null)
        {
			scrollOverText.GetComponent<Text>().text = "Infuser";
			
			//print(scrolledOverMasher.humanCount);
			if (scrolledOverInfuser.humanCount == scrolledOverInfuser.humanCapacity)
			{
				collectMoneyButton.gameObject.SetActive(true);
				collectMoneyButtonText.GetComponent<Text>().text = "Infuse!";
			}
			else
			{
				collectMoneyButton.gameObject.SetActive(false);
			}
        }
    }
    public void CollectFromScrolledOverBuilding()
    {
        if(scrolledOverBuilding != null){
            money += scrolledOverBuilding.CollectMoney();
        }
        if(scrolledOverInfuser != null){
			infuserPanel.SetActive(true);
			infuserPanel.transform.position = Input.mousePosition;
			stupidNeededInfuser = scrolledOverInfuser;
        }
        if(scrolledOverMasher != null){
            scrolledOverMasher.Mash();
        }
    }
	public void InfuserSelection(int id){
		stupidNeededInfuser.GetComponent<Infuser>().Infuse (id);
	}
    void SpawnInitHumans()
    {
        for (int i = 0; i < initHumans; i++ )
        {
            //Close to the center of the spawning pen
            Vector3 spawnPos = new Vector3(abductionChamber.transform.position.x + (Random.Range(-3.5f, 3.5f)), 1, abductionChamber.transform.position.z + (Random.Range(-3.5f, 3.5f)));
            GameObject human = Instantiate(humanPrefab, spawnPos, Quaternion.identity) as GameObject;
            
            //human.GetComponent<Creature>().ApplyResource(Random.Range(0, 4));

            humans.Add(human);
        }
    }
	void Update () {
        //Check to see if we want to deselect the current human by clicking on the terrain

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
        UpdateUIText();
	}

    public void PlaceHab(int habType)
    {
        currentSpawnType = habType;
        placingBuilding = true;
        buildingCount++;
    }
    private void SpawnHab(int habType, Vector3 spawnPos)
    {
        if (gameObject.GetComponent<Constants>().buildingCosts[habType] > money)
        {
            return;
        }
        money -= gameObject.GetComponent<Constants>().buildingCosts[habType];
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
            case Constants.INFUSER:
                habToSpawn = Instantiate(infuserPrefab, spawnPos, Quaternion.identity) as GameObject;
                break;
            case Constants.MASHER:
                habToSpawn = Instantiate(masherPrefab, spawnPos, Quaternion.identity) as GameObject;
                break;
            default:
                print("Spawning hab parameter ate shit.");
                break;
        }
    }
}
