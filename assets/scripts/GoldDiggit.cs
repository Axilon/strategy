using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldDiggit : MonoBehaviour {

    public int goldAmmount = 7500;
    
    private GameObject goldMine;
	
	// Update is called once per frame
	void Update () {
		if(goldMine!= null)
        {
            goldMine.GetComponent<buildingGoldMine>().ongoldDiggit = true;
            goldMine.GetComponent<buildingGoldMine>().digitPosition = transform.position;
        }
        
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name=="GoldMine(Clone)")
        {
            goldMine = other.gameObject;
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "GoldMine(Clone)")
        {
            goldMine.GetComponent<buildingGoldMine>().ongoldDiggit = false;
            goldMine = null;
        }
    }
}
