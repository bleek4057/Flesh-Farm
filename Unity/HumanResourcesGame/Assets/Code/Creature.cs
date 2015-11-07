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
    private List<int> resources;//A list of resources attatched to this human, delimitted by |'s 
	private bool inMasher;

    //Human output
    public float cycleLength;//How long it takes between each drop of revenue from this human
    public float lastCycleEnd;
    public int revenuePerCycle;//How much money this human gives per drop of revenue
    public Building currentBuilding;
	public Masher currentMasher;

    //Human visuals
    public Renderer humanRenderer;

	void Start () {
        lastCycleEnd = Time.time;
        gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		doodads = new List<GameObject>();
        resources = new List<int>();
		ApplyResource (Random.Range(0, 4));
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
    }
	public void SetBuilding(Masher m){
		currentMasher = m;
	}
    void OnMouseDown()
    {
        gM.SetSelectedHuman(gameObject);
    }
	//get a resource's info based on an Id
	void ApplyResource(int resourceId){
        resources.Add(resourceId);

        //Set the human's material
        humanRenderer.material = gM.GetComponent<Constants>().GetMat(resourceId);

        //Add to the human's doodads
        doodads.Add(gM.GetComponent<Constants>().GetDoodad(resourceId));
        

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
