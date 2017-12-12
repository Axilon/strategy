using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TroopsWorker : MonoBehaviour {

    public string text = "Рабочий";
    public bool visiableMenu = false;
    public bool moving = false;

    public GameObject farm;
    public GameObject goldMine;
    public LayerMask mask;
    public float curHealth;
    public int price = 50;
  
    private float moveSpeed = 5f;
    private float startHealth = 70;
    //private int damage = 5;
    private NavMeshAgent nav;
    private BackGroundUI db;
    private GameObject choosedBuilding;
    private bool constructing;
    

    private void OnEnable()
    {
        curHealth = startHealth;
        nav = GetComponent<NavMeshAgent>();
        nav.speed = moveSpeed;
        db = GameObject.FindGameObjectWithTag("MainUI").GetComponent<BackGroundUI>();
        constructing = false;
        choosedBuilding = null;
        BackGroundUI.currentWorkersAmmount++;

    }
    // Use this for initialization
    void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            moving = false;
            constructing = false;
            visiableMenu = false;

        }
        if(visiableMenu  )
        {
            Ray ray;
            RaycastHit hit;

            ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 10000f, mask) && Input.GetMouseButtonDown(0) && choosedBuilding == null && !constructing && moving)
            {
                nav.SetDestination(hit.point);
            }
            else if(Physics.Raycast(ray, out hit, 10000f, mask) && choosedBuilding != null && constructing  )
            {

                choosedBuilding.transform.position = hit.point;
               

                if (Input.GetMouseButtonDown(2) && db.intersection == 0 )
                {
                    moving = false;
                    Vector3 target; 
                    if(choosedBuilding.GetComponent<buildingGoldMine>() != null && choosedBuilding.GetComponent<buildingGoldMine>().ongoldDiggit == true)
                    {
                        target = choosedBuilding.GetComponent<buildingGoldMine>().digitPosition;
                        choosedBuilding.transform.position = target;
                        choosedBuilding.GetComponent<buildingGoldMine>().isBuilted = true;
                    }
                    else if(choosedBuilding.GetComponent<Farm>()!= null)
                    {
                        target = hit.point;
                        choosedBuilding.transform.position = target;
                        choosedBuilding.GetComponent<Farm>().isBuilted = true;
                    }
                    choosedBuilding = null;
                    moving = true;
                    constructing = false;
                    
                }
                
            }

        }
	}
   
    private void OnMouseDown()
    {
        moving = true;
        nav.enabled = true;
        if(db.activeObjectInterface != null)
        {
            db.interfaceDiactivator();
        }
        db.activeObjectInterface = gameObject;
        db.buildingType = text;
        visiableMenu = true;

    }
    public void TakeDamage(float damage)
    {
        curHealth -= damage;
        if (curHealth <= 0)
        {
            Destroy(gameObject);
            BackGroundUI.currentWorkersAmmount--;
        }
    }
    private void OnGUI()
    {
        if (visiableMenu)
        {

            GUI.depth = 1;

            GUI.BeginGroup(new Rect((Screen.width -220) /2, Screen.height - 190, 320, 190));

            GUI.Box(new Rect(0, 0, 320, 190), "");

            if (GUI.Button(new Rect(10, 10, 140, 140), farm.GetComponent<Farm>().text))
            {
                if (BackGroundUI.money >= farm.GetComponent<Farm>().price)
                {
                    
                    constructing = true;
                    choosedBuilding = Instantiate(farm);
                    //choosedBuilding.GetComponent<BoxCollider>().isTrigger = true;
                    BackGroundUI.money -= farm.GetComponent<Farm>().price;
                }
                
            }
            GUI.Box(new Rect(10, 155, 140, 20), farm.GetComponent<Farm>().price.ToString());

            if (GUI.Button(new Rect(160, 10, 140, 140), goldMine.GetComponent<buildingGoldMine>().text))
            {
                if (BackGroundUI.money >= goldMine.GetComponent<buildingGoldMine>().price)
                {
                    
                    constructing = true;
                    choosedBuilding = Instantiate(goldMine);
                    //choosedBuilding.GetComponent<BoxCollider>().isTrigger = true;
                    BackGroundUI.money -= goldMine.GetComponent<buildingGoldMine>().price;
                }
               
            }
            GUI.Box(new Rect(160, 155, 140, 20), goldMine.GetComponent<buildingGoldMine>().price.ToString());

            GUI.EndGroup();

        }
    }
    
}
