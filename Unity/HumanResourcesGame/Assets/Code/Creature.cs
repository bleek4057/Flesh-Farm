using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Creature : MonoBehaviour {

	//resource attrtibutes
	List<GameObject> doodads;
	public Material currentMat;
	private int testCount = 0;

    private GameManager gM;

    //Human stats

    public List<int> resources;//A list of resources attatched to this human, delimitted by |'s 
	private bool inMasher;
    public string resourcesString ="";

    //Human output
    public float cycleLength;//How long it takes between each drop of revenue from this human
    public float lastCycleEnd;
    public int revenuePerCycle;//How much money this human gives per drop of revenue

    public Building currentBuilding;
	public Masher currentMasher;
    public Infuser currentInfuser;

    //Human visuals
    public Renderer humanRenderer;
    
    void Awake()
    {
        gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        resources = new List<int>();

    }
	void Start () {
        lastCycleEnd = Time.time;
		doodads = new List<GameObject>();
	}

	void Update () {
        if(currentBuilding != null){
            CheckForCycleEnd();
        }
	}
    void CheckForCycleEnd()
    {
        if (Time.time >= lastCycleEnd + cycleLength)
        {
            currentBuilding.AddRevenue(revenuePerCycle);
            lastCycleEnd = Time.time;
        }
    }
   
    public void SetBuilding(Building b)
    {
        currentBuilding = b;
        currentMasher = null;
        currentInfuser = null;
    }
	public void SetBuilding(Masher m){
		currentMasher = m;
        currentBuilding = null;
        currentInfuser = null;
	}
    public void SetBuilding(Infuser i)
    {
        currentInfuser = i;
        currentBuilding = null;
        currentMasher = null;
    }
    void OnMouseDown()
    {
        gM.SetSelectedHuman(gameObject);
    }
	//get a resource's info based on an Id
	public void ApplyResource(int resourceId){
        resources.Add(resourceId);
        resourcesString += gM.GetComponent<Constants>().resourceNames[resourceId] + " ";

        //Set the human's material
        humanRenderer.material = gM.GetComponent<Constants>().GetMat(resourceId);
        currentMat = gM.GetComponent<Constants>().GetMat(resourceId);
        
        
        //Add to the human's doodads
        
        //doodads.Add(gM.GetComponent<Constants>().GetDoodad(resourceId));
	}

}
