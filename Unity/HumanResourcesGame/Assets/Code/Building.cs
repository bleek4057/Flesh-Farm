using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building : MonoBehaviour {

    //Util
    private GameManager gM;
    private bool scrolledOver;

    //Building stats
    public int xPos, yPos;
    public int humanCapacity;
    public int comfortLevel;

    public int humanCount;
    public int money;

    //Humans
    private List<GameObject> humans;

	void Start () {
        gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        humans = new List<GameObject>();
	}
	
	void Update () {
	
	}
    public int CollectMoney()
    {
        int moneyToCollect = money;
        money = 0;
        return moneyToCollect;
    }
    void OnMouseOver()
    {
        gM.scrolledOverBuilding = this;

        if(Input.GetButtonDown("Fire2")){
            print("Clicked on building: " + gameObject.name);
            AddHuman(gM.selectedHuman);
            gM.SetSelectedHuman(null);
        }
        if(!scrolledOver){
            gM.scrollOverPanel.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }
    void OnMouseExit()
    {
        scrolledOver = false;
        gM.scrolledOverBuilding = null;
        gM.scrollOverPanel.transform.position = new Vector2(3000, 3000);
    }
    public void AddRevenue(int revenue)
    {
        money += revenue;
    }
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
        float maxX = GetComponent<Collider>().bounds.extents.x;
        float maxZ = GetComponent<Collider>().bounds.extents.z;
        Vector3 newPos;
        RaycastHit hit;
        //..find the position of the mouse click
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000))
        {
            newPos = hit.point;
            //Vector3 newPos = new Vector3(transform.position.x + Random.Range(-maxX, maxX), transform.position.y, transform.position.z + Random.Range(-maxZ, maxZ));
            human.transform.position = newPos;
        }       

        if (gM.tutorialComplete == false && gM.currentTipNumber == 4)
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
