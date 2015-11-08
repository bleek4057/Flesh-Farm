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
    public int humanRespawnTimer;
    public int abductionChamberCount;

    //Resources
	public GameObject resourceInventory;
	public List<string> resourceInvName;
	public List<int> resourceInvCount;
	public List<int> resourceInvId;

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
    public bool tutorialComplete;
    public int currentTipNumber = 0;
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

	public Text[] resourceButtonsText;

    public RectTransform tutorialPanel;
    public Text tutorialText;
    public GameObject tutorialButton;
    public GameObject endTutorialButton;

    public string[] tutorialTips;

    void Awake()
    {
        ground = GameObject.FindGameObjectWithTag("Ground");
        abductionChamber = GameObject.FindGameObjectWithTag("AbductionChamber");
		resourceInvName = new List<string>{"Sand", "Wood", "Gold", "Francium", "Tears"};
		resourceInvCount = new List<int>{0, 0, 0, 0, 0};
		resourceInvId = new List<int>{0, 1, 2, 3, 4};
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
                    tutorialPanel.anchoredPosition3D = new Vector3(0, 100, 0);
                    tutorialPanel.sizeDelta = new Vector2(350, 100);
                    tutorialText.rectTransform.anchoredPosition = new Vector3(0, 3, 0);
                    tutorialText.rectTransform.sizeDelta = new Vector2(320, 70);
                    tutorialText.text = "Welcome. This is your new research facility. You have been contracted to perform experiments and gather information regarding humans. Let's begin by showing you around.";
                break;
            case 1:
                //derp
                    tutorialPanel.anchoredPosition3D = new Vector3(-110, 85, 0);
                    tutorialPanel.sizeDelta = new Vector2(250, 100);
                    tutorialText.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
                    tutorialText.rectTransform.sizeDelta = new Vector2(230, 80);
                    tutorialText.text = "These things here, as you can see, are humans. We will supply you with humans while you are contracted with us.";
                break;
            case 2:
                //Build a hab
                    tutorialPanel.anchoredPosition3D = new Vector3(340, 320, 0);
                    tutorialPanel.sizeDelta = new Vector2(300, 120);
                    tutorialText.rectTransform.anchoredPosition = new Vector3(0, 16, 0);
                    tutorialText.rectTransform.sizeDelta = new Vector2(280, 80);
                    tutorialButton.SetActive(false);
                    tutorialText.text = "You need to hold your humans inside something. We can't let them wander the facility. Click the 'Build' button and build select the 'White Box'.";
                break;
            case 3:
                //pointless congratz :P
                    tutorialText.text = "Good, now click anywhere in the facility to place the habitat.";
                break;
            case 4:
                //place human
                    tutorialText.text = "Doesn't that box look comfy? Try putting one of your humans in it. Left-Click the human to select it, then Right-Click in the box to place it.";
                break;
            case 5:
                //derp
                    tutorialButton.SetActive(true);
                    tutorialText.text = "Nicely done. Now here's what we want you to do. Study that human. How does he react?";
                break;
            case 6:
                //Explain Income
                    tutorialButton.SetActive(false);
                    tutorialText.text = "How you accomplish this is by collecting the 'Research Points' you gain from studying the human. You can collect 'Research Points' by Left-Clicking on the habitat.";

                    if (money >= 1000) NextTutorialTip();
                break;
            case 7:
                //Infuser Tutorial
                    tutorialText.text = "Studying 'Normal' humans can get quite boring. So we've invented a machine to 'Infuse' humans with certain resources. Go buy one now!";
                break;
            case 8:
                //Infuser Tutorial Cont.
                    tutorialText.text = "Look at that beautiful piece of machinery. Now, take another human in the box and place him in the Infuser. Once there, Infuse him with this piece of sand.";
                break;
            case 9:
                //Infuser Tutorial Cont.
                    tutorialText.text = "Look at what you've done! It's magnificent! Quick, do it again to the human in the box, except use this piece of wood! (You may need additional habitats. A white box can only hold 1 human)";
                break;
            case 10:
                //Masher Tutorial
                    tutorialText.text = "Now that you have 2 infused humans. LET'S MASH THEM TOGETHER! For... research of course. Just go buy a Masher... This one's on the house.";
                break;
            case 11:
                //Masher Tutorial Cont.
                    tutorialText.text = "This wonderful piece of machinery takes two infused humans, and mashes them together to make a... 'baby'. Don't worry, our technological advancements will ensure the 'parents' will be safe. Go try it out!";
                break;
            case 12:
                //Masher Tutorial Cont. --- Demo End
                    endTutorialButton.SetActive(true);
                    tutorialText.text = "Congratulations! You've created that wonderful... 'baby'. You've also learned basically all you need to know how to do for this Demo! Enjoy the game!";
                break;
            case 13:
                //Demo End --- GG
                    resourceInvCount = new List<int> { 20, 20, 20, 10, 5 };
                    tutorialComplete = true;
                    this.GetComponent<Constants>().buildingCosts[4] = 2000;
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
        if (!tutorialComplete)
        {
            CheckTutorialConditions();
        }
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

            collectMoneyButton.gameObject.SetActive(true);

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

		//Component[] buttons = GetComponentsInChildren<Button> ();
		int i = 0;
		foreach (Text button in resourceButtonsText) {
			button.text = "" + resourceInvName[i] + " x " + resourceInvCount[i];
			i++;
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
		if (resourceInvCount [id] > 0) {
			stupidNeededInfuser.GetComponent<Infuser> ().Infuse (id);
			resourceInvCount [id]--;

            if (tutorialComplete == false && (currentTipNumber == 8 || currentTipNumber == 9))
            {
                if (currentTipNumber == 8)
                {
                    resourceInvCount[1]++;
                }
                NextTutorialTip();
            }
		}
	}
    void SpawnInitHumans()
    {
        for (int i = 0; i < initHumans; i++ )
        {
            float maxX = abductionChamber.GetComponent<Renderer>().bounds.extents.x;
            float maxZ = abductionChamber.GetComponent<Renderer>().bounds.extents.z;

            //Close to the center of the spawning pen
            Vector3 spawnPos = new Vector3(abductionChamber.transform.position.x + (Random.Range(-maxX + 2, maxX - 2)), 1, abductionChamber.transform.position.z + (Random.Range(-maxZ + 2, maxZ - 2)));
            GameObject human = Instantiate(humanPrefab, spawnPos, Quaternion.identity) as GameObject;
            
            //human.GetComponent<Creature>().ApplyResource(Random.Range(0, 4));

            humans.Add(human);
            abductionChamberCount = 5;
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

        //human timer resolution, after 1 min, add new human to abduction pool, max of 5
        if (abductionChamberCount < 5)
        {
            humanRespawnTimer++;
        }
        else humanRespawnTimer = 0;

        if (humanRespawnTimer >= 3600)
        {
            float maxX = abductionChamber.GetComponent<Renderer>().bounds.extents.x;
            float maxZ = abductionChamber.GetComponent<Renderer>().bounds.extents.z;

            //Close to the center of the spawning pen
            Vector3 spawnPos = new Vector3(abductionChamber.transform.position.x + (Random.Range(-maxX + 2, maxX - 2)), 1, abductionChamber.transform.position.z + (Random.Range(-maxZ + 2, maxZ - 2)));
            GameObject human = Instantiate(humanPrefab, spawnPos, Quaternion.identity) as GameObject;

            humans.Add(human);
            abductionChamberCount++;
            humanRespawnTimer = 0;
        }
	}

    public void PlaceHab(int habType)
    {
        //check if this is fulfilling tutorial req
        if (habType == 0 && !tutorialComplete && currentTipNumber == 2)
        {
            NextTutorialTip();
        }

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

        //check if this is fulfilling tutorial req
        if (!tutorialComplete && ((currentTipNumber == 3 && habType == 0)  || (currentTipNumber == 7 && habType == 3) || (currentTipNumber == 10 && habType == 4)))
        {
            if (currentTipNumber == 7 && habType == 3)
            {
                resourceInvCount[0]++;
            }
            NextTutorialTip();
        }
    }

	public void GainResource(string resourceName, int id, int amount){
		resourceInvName.Add (resourceName);
		resourceInvCount.Add (amount);
		resourceInvId.Add (id);
	}
}
