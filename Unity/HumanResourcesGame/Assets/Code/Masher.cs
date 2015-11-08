﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Masher : MonoBehaviour {

	//references to the arms
	public GameObject leftArm;
	public GameObject rightArm;
	public GameObject childPrefab;

	//stolen attributes
	public int xPos, yPos;
	public int humanCapacity;
	public int humanCount;
	private List<GameObject> humans;

    private bool scrolledOver;
    public bool playingAnimation;

    //Util
    private GameManager gM;

    public ParticleSystem blood;
    float startMashTime;
    public float mashLength;
    private bool mashing;

	void Start () {
        //GetComponent<Animator>().Stop();
        SetMashAnim(false);
        gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        humans = new List<GameObject>();
	}

	void Update () {
		/*if (humanCount == humanCapacity) {
			MashHumans(humans[0], humans[1]);
		}*/
        if(mashing){
            if(Time.time >= startMashTime + mashLength){
                //Its time to stop mashing
                mashing = false;
                SetMashAnim(false);
                MashHumans(humans[0], humans[1]);
            }
        }
	}
    public void SetMashAnim(bool b)
    {
        GetComponent<Animator>().enabled = b;
        if(b){
            startMashTime = Time.time;
            blood.enableEmission = b;
            mashing = true;
            blood.Play();
        }
        else
        {
            GetComponent<Animator>().Rebind();
        }
    }

    void OnMouseOver()
    {
        gM.scrolledOverMasher = this;

        if (Input.GetButtonDown("Fire2"))
        {
            print("Clicked on building: " + gameObject.name);
            AddHuman(gM.selectedHuman);
            gM.SetSelectedHuman(null);
        }
        if (!scrolledOver)
        {
            gM.scrollOverPanel.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }
    void OnMouseExit()
    {
        scrolledOver = false;
        gM.scrolledOverMasher = null;
        gM.scrollOverPanel.transform.position = new Vector2(3000, 3000);
    }
	//steal Building methods
	void AddHuman(GameObject human)
	{
		if(human != null){
			if(humanCount < humanCapacity){
				human.GetComponent<Creature>().Leave();
				humanCount++;
				humans.Add(human);
				MoveHumanToMe(human);
				human.GetComponent<Creature>().SetBuilding(this);
			}
		}
	}
	
	void MoveHumanToMe(GameObject human)
	{
		/*float maxX = GetComponent<Collider>().bounds.extents.x;
		float maxZ = GetComponent<Collider>().bounds.extents.z;
		
		Vector3 newPos = new Vector3(transform.position.x + Random.Range(-1.5f, 1.5f), transform.position.y, transform.position.z + Random.Range(-1.5f, 1.5f));
		human.transform.position = newPos;*/
        Vector3 newPos;
        RaycastHit hit;
        //..find the position of the mouse click
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000))
        {
            newPos = hit.point;
            //Vector3 newPos = new Vector3(transform.position.x + Random.Range(-maxX, maxX), transform.position.y, transform.position.z + Random.Range(-maxZ, maxZ));
            human.transform.position = newPos;
        }
	}
    public void Mash()
    {
        SetMashAnim(true);
    }
	//masher method, requires two people to be present in machine
	void MashHumans(GameObject parent1, GameObject parent2)
	{
        GameObject child = GameObject.Instantiate(childPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z - 3), Quaternion.identity) as GameObject;
		Creature childScript = child.GetComponent<Creature> ();

		//set random attributes
		int randomMat = Random.Range (0, 1);
		if (randomMat == 0) { //parent 1
			childScript.ApplyResource(parent1.GetComponent<Creature>().resources[0]);
		} else { //parent 2
			childScript.ApplyResource(parent2.GetComponent<Creature>().resources[0]);

		}

		/*give them random doodads
		List<GameObject> randomDoodads = new List<GameObject> ();
		randomDoodads.AddRange (parent1.GetComponent<Creature>().doodads);
		randomDoodads.AddRange (parent2.GetComponent<Creature>().doodads);

		//rng for number of doodads
		int rngDoodads = Random.Range (0, randomDoodads.Count);

		//take random elements from list
		int cap = rngDoodads.Count;

		for (int i = 0; i < rngDoodads; i++) {
			child.doodads.Add(rngDoodads[Random.Range(0, rngDoodads.Count - 1)]);
		}*/

        if (gM.tutorialComplete == false && gM.currentTipNumber == 11)
        {
            gM.NextTutorialTip();
        }
	}

	public void RemoveHuman(GameObject human){
		if(human != null){
			humanCount--;
			humans.Remove(human);
		}
	}
}
