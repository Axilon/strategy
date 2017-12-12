using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Farm : MonoBehaviour {

    public bool visiableMenu = false;
    public bool isBuilted = false;
    public int numDepth = 1;

    public string text = "Ферма";
    public int price = 250;
    public GameObject warrior;
    public GameObject bowMan;


    private int workersAmmount = 1;
    private int troopsAmmount = 3;
    
    private Vector3 troopsSpawnPosition;
    private BackGroundUI db;
    private void OnEnable()
    {
        troopsSpawnPosition = transform.position;
        troopsSpawnPosition.z += 1.5f;
        BackGroundUI.maxWorkersAmmount += workersAmmount;
        BackGroundUI.maxSoldierAmmount += troopsAmmount;
        db = GameObject.FindGameObjectWithTag("MainUI").GetComponent<BackGroundUI>();
        db.intersection = 0;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {

            visiableMenu = false;

        }
       
    }
    private void OnMouseDown()
    {
        if (isBuilted)
        {
            if (db.activeObjectInterface != null)
            {
                db.interfaceDiactivator();
            }

            db.activeObjectInterface = gameObject;
            db.buildingType = text;
            visiableMenu = true;
        }
    }

    private void OnGUI()
    {
        if (visiableMenu && isBuilted)
        {

            GUI.depth = numDepth;

            GUI.BeginGroup(new Rect((Screen.width -320) / 2, Screen.height - 190, 570, 190));

            GUI.Box(new Rect(0, 0, 880, 190), "");
            GUI.Label(new Rect(10, 70, 150, 50), "Название: " + text);

            if (GUI.Button(new Rect(180, 10, 140, 140), warrior.GetComponent<troopsWarrior>().text))
            {
                if (BackGroundUI.money >= warrior.GetComponent<troopsWarrior>().price && BackGroundUI.currentSoldierAmmount < BackGroundUI.maxSoldierAmmount)
                {
                    Instantiate(warrior, transform.position + new Vector3(0.8f, 0, -1.5f), transform.rotation);
                    BackGroundUI.money -= warrior.GetComponent<troopsWarrior>().price;
                }
                else
                {
                    return;
                }
            }
            GUI.Box(new Rect(180, 155, 140, 20), warrior.GetComponent<troopsWarrior>().price.ToString());

            if (GUI.Button(new Rect(330, 10, 140, 140), bowMan.GetComponent<troopsBowMan>().text))
            {
                if (BackGroundUI.money >= bowMan.GetComponent<troopsBowMan>().price && BackGroundUI.currentSoldierAmmount < BackGroundUI.maxSoldierAmmount)
                {
                    Instantiate(bowMan, transform.position + new Vector3(0.8f, 0, -1.5f), transform.rotation);
                    BackGroundUI.money -= bowMan.GetComponent<troopsBowMan>().price;
                }
                else
                {
                    return;
                }
            }
            GUI.Box(new Rect(330, 155, 140, 20), bowMan.GetComponent<troopsBowMan>().price.ToString());


            GUI.EndGroup();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
         if (other.tag.Equals("buildings"))
        {
            db.intersection++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
         if (other.tag.Equals("buildings"))
        {
            db.intersection--;
        }
    }
}
