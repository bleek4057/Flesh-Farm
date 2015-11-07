using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Masher : MonoBehaviour {

	//references to the arms
	public GameObject leftArm;
	public GameObject rightArm;

	//stolen attributes
	public int xPos, yPos;
	public int humanCapacity;
	public int humanCount;
	private List<GameObject> humans;

	void Start () {
		
	}

	void Update () {
		
	}

	//steal Building methods
	void AddHuman(GameObject human)
	{
		if(human != null){
			if(humanCount < humanCapacity){
				humanCount++;
				humans.Add(human);
				MoveHumanToMe(human);
				human.GetComponent<Creature>().SetBuilding(this);
			}
		}
	}
	
	void MoveHumanToMe(GameObject human)
	{
		float maxX = GetComponent<Collider>().bounds.extents.x;
		float maxZ = GetComponent<Collider>().bounds.extents.z;
		
		
		Vector3 newPos = new Vector3(transform.position.x + Random.Range(-1.5f, 1.5f), transform.position.y, transform.position.z + Random.Range(-1.5f, 1.5f));
		human.transform.position = newPos;
	}

	//masher method, requires two people to be present in machine
	void MashHumans(GameObject parent1, GameObject parent2)
	{

	}
}
