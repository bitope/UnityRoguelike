using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class ProximityItemManager : MonoBehaviour
{
    private List<GameObject> pointerList;
    public GameObject proximityItemPrototype;

	// Use this for initialization
	void Start () {
        pointerList = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        
        //TODO: FIXME. Detta är en en quickfix för att få listen till höger att tömmas även i Build.
        var g = transform.GetChild(0).GetChild(0);
	    var allChildren = g.GetComponentsInChildren<Transform>();
	    foreach (var child in allChildren)
	    {
            if (child.name!="Grid")
	            Destroy(child.gameObject);
	    }

        foreach (var o in pointerList)
        {
             AddItem(o);
        }
	}

    public void UpdatePointerList(List<GameObject> p)
    {
        //foreach (var o in pointerList)
        //{
        //    if (!p.Contains(o))
        //        RemoveItem(o);
        //}

        //foreach (var o in p)
        //{
        //    var x = GameObject.Find("id." + o.GetInstanceID());
        //    if (x == null)
        //        AddItem(o);
        //}

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
        
        Debug.Log("Destroying: ("+o.GetInstanceID()+") "+x);
        Destroy(x);

    }
}
