using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building : MonoBehaviour {

    //Util
    private GameManager gM;

    //Building stats
    public int xPos, yPos;
    public int humanCapacity;
    public int comfortLevel;

    public int humanCount;

    //Humans
    private List<GameObject> humans;

	void Start () {
        gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        humans = new List<GameObject>();
	}
	
	void Update () {
	
	}
    void OnMouseOver()
    {
        if(Input.GetButtonDown("Fire2")){
            print("Clicked on building: " + gameObject.name);
            AddHuman(gM.selectedHuman);
        }else{
            
        }
    }
    void AddHuman(GameObject human)
    {
        if(human != null){
            if(humanCount < humanCapacity){
                humanCount++;
                humans.Add(human);
                MoveHumanToMe(human);
            }
        }
    }
    void MoveHumanToMe(GameObject human)
    {
        float maxX = GetComponent<MeshRenderer>().bounds.extents.x;
        float maxZ = GetComponent<MeshRenderer>().bounds.extents.z;

        Vector3 newPos = new Vector3(transform.position.x + Random.Range(-1.5f, 1.5f), 1, transform.position.z + Random.Range(-1.5f, 1.5f));
        human.transform.position = newPos;
    }
}
