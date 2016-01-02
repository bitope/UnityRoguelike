using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ProximityItemManager : MonoBehaviour
{

    public List<GameObject> proximityList;
    public GameObject proximityItemPrototype;

	// Use this for initialization
	void Start () {
	    proximityList = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void AddItem(GameObject o)
    {
        var x = Instantiate(proximityItemPrototype);
        x.name = "id." + o.GetInstanceID();
        var g = transform.GetChild(0).GetChild(0);
        x.transform.SetParent(g);
    }

    public void RemoveItem(GameObject o)
    {
        var x = GameObject.Find("id." + o.GetInstanceID());
        Destroy(x);
    }
}
