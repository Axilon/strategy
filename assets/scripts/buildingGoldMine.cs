using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buildingGoldMine : MonoBehaviour {

    public bool visiableMenu = false;
    public bool ongoldDiggit;
    public bool isBuilted = false;
    public int numDepth = 1;

    public string text = "Шахта";
    public int price = 350;

    public Vector3 digitPosition;

    private int goldEarning = 25;
    private float goldEarningTime = 5f;
    private float curMiningTime;
    private int currentGoldAmmount;
    private int availableGoldAmmount = 1500;
    private BackGroundUI db;
    


    private void OnEnable()
    {
        curMiningTime = 0;
        db = GameObject.FindGameObjectWithTag("MainUI").GetComponent<BackGroundUI>();
        db.intersection = 0;
        currentGoldAmmount = availableGoldAmmount;
        ongoldDiggit = false;


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
        if (curMiningTime >= goldEarningTime)
        {
            BackGroundUI.money += goldEarning;
            currentGoldAmmount -= goldEarning;
            curMiningTime = 0;
            GoldAmmountScan();
        }
        else
        {
            curMiningTime += Time.deltaTime;
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

            GUI.BeginGroup(new Rect((Screen.width -75) / 2, Screen.height - 190, 150, 190));

            GUI.Box(new Rect(0, 0, 150, 190), "");
            GUI.Label(new Rect(10, 10, 130, 20), "Название: ");
            GUI.Label(new Rect(10, 30, 130, 20),  text);
            GUI.Label(new Rect(10, 50, 130, 20), "Скорость добычи: ");
            GUI.Label(new Rect(10, 70, 130, 20), goldEarning.ToString() + " в 5с." );
            GUI.Label(new Rect(10, 90, 130, 20),  "Доступно золота:");
            GUI.Label(new Rect(10, 110, 130, 20), currentGoldAmmount.ToString() + "/"+ availableGoldAmmount.ToString());





            GUI.EndGroup();

        }
    }


   
    private void GoldAmmountScan()
    {
        if (currentGoldAmmount <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)

    {
        foreach(GameObject diggit in DiggitList.digits)
        {
           if (other.gameObject == diggit)
            {
                ongoldDiggit = true;
                digitPosition = other.gameObject.transform.position;
            }
        }
        



        if (other.tag.Equals("newBuilding"))
        {
            db.intersection++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        foreach (GameObject diggit in DiggitList.digits)
        {
            if (other.gameObject == diggit)
            {
                ongoldDiggit = false;
            }
        }
        
        if (other.tag.Equals("buildings"))
        {
            db.intersection--;
        }
    }
}
