using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingMainBuilding : MonoBehaviour {

    public bool visiableMenu = false;
    public int numDepth = 1;

    public string text = "Главное здание";
    public int workersToCreate = 1;
    public GameObject worker;

    private int startingGoldAmmount = 1500;
    private string workerText;
    private int workerPrice ;

    private BackGroundUI db;

    // Use this for initialization
    private void Start()
    {
       
        workerPrice = worker.GetComponent<TroopsWorker>().price;
        BackGroundUI.money += startingGoldAmmount;
        BackGroundUI.maxWorkersAmmount += workersToCreate;
        db = GameObject.FindGameObjectWithTag("MainUI").GetComponent<BackGroundUI>();

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
        if (db.activeObjectInterface != null)
        {
            db.interfaceDiactivator();
        }
        db.activeObjectInterface = gameObject;
        db.buildingType = text;
        visiableMenu = true;
    }

    private void OnGUI()
    {
        if (visiableMenu) { 

            GUI.depth = numDepth;

            GUI.BeginGroup(new Rect((Screen.width-150)/2, Screen.height - 190,400,190));

            GUI.Box(new Rect(0, 0, 880, 190), "");
            GUI.Label(new Rect(10,70,150,50), "Название: " + text);

            if(GUI.Button(new Rect(180, 10, 140,140),"Рабочий"))
            {
                if(BackGroundUI.money>= workerPrice && BackGroundUI.currentWorkersAmmount<BackGroundUI.maxWorkersAmmount)
                {
                    Instantiate(worker, transform.position + new Vector3(0.8f,0,-1.5f), transform.rotation);
                    BackGroundUI.money -= workerPrice;
                }
                else
                {
                    return;
                }
            }
            GUI.Box(new Rect(180, 155, 140, 20), workerPrice.ToString());

            GUI.EndGroup();

        }
    }
}
