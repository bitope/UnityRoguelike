using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ProximityItemManager : MonoBehaviour
{

    private List<GameObject> proximityList;
    private List<GameObject> pointerList;
    public GameObject proximityItemPrototype;

	// Use this for initialization
	void Start () {
	    proximityList = new List<GameObject>();
        pointerList = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void UpdatePointerList(List<GameObject> p)
    {
        foreach (var o in pointerList)
        {
            if (!p.Contains(o))
                RemoveItem(o);
        }

        foreach (var o in p)
        {
            var x = GameObject.Find("id." + o.GetInstanceID());
            if (x == null)
                AddItem(o);
        }

        pointerList = p;
    }

    public void AddItem(GameObject o)
    {
        var x = Instantiate(proximityItemPrototype);
        x.name = "id." + o.GetInstanceID();
        var g = transform.GetChild(0).GetChild(0);
        x.transform.SetParent(g);

        var controller = x.GetComponent<ProximityItemController>();
        
        controller.SetInfo(o.name);
    }

    public void RemoveItem(GameObject o)
    {
        var x = GameObject.Find("id." + o.GetInstanceID());
        Destroy(x);
    }
}
