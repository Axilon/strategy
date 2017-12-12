using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggitList : MonoBehaviour {

    public static List<GameObject> digits = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        GameObject[] dig = GameObject.FindGameObjectsWithTag("goldDiggits");
        foreach(GameObject diggit in dig)
        {
            digits.Add(diggit);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void removeFromDigitList(GameObject dig)
    {
        digits.Remove(dig);
    }
}
