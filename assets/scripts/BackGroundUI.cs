using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundUI : MonoBehaviour {
    public GUISkin mainUI;
    public int numDepth = 0;

    public GameObject activeObjectInterface;
    public string buildingType;

    public static int money;
    public static int currentWorkersAmmount;
    public static int maxWorkersAmmount;
    public static int currentSoldierAmmount;
    public static int maxSoldierAmmount;

    public int intersection = 0;

    void Start () {
       
    }
	
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("exit");
        }
        
       
	}

    private void OnGUI()
    {
     
        GUI.depth = numDepth;
        
        //верхнее меню
        GUI.Box(new Rect(Screen.width - 300, 0, 600, 30), "");
        GUI.Label(new Rect(Screen.width - 280, 3, 200, 30), "Workers: " + currentWorkersAmmount.ToString() + "/" + maxWorkersAmmount.ToString(), GUI.skin.label);
        GUI.Label(new Rect(Screen.width - 180, 3, 200, 30), "Soldiers: " + currentSoldierAmmount.ToString() + "/" + maxSoldierAmmount.ToString(), GUI.skin.label);
        GUI.Label(new Rect(Screen.width - 100, 3, 200, 30), "Money: " + money.ToString(), GUI.skin.label);

        
    }

    public void interfaceDiactivator()
    {
        switch(buildingType)
        {
            case "Главное здание":
                activeObjectInterface.GetComponent<buildingMainBuilding>().visiableMenu = false;
                break;
            case "Ферма":
                activeObjectInterface.GetComponent<Farm>().visiableMenu = false;
                break;
            case "Шахта":
                activeObjectInterface.GetComponent<buildingGoldMine>().visiableMenu = false;
                break;
            case "Рабочий":
                activeObjectInterface.GetComponent<TroopsWorker>().visiableMenu = false;
                break;
        }
    }
}
