using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour
{
    private List<GameObject> Slots;
 
    public bool isVisible;
    public GameObject slotprototype ;

    private Canvas canvas;

	// Use this for initialization
	void Start ()
	{
        Slots = new List<GameObject>();

	    var i = 0;
	    for (int y = 4; y >=0 ; y--)
	    {
	        for (int x = 0; x < 10; x++)
	        {
	            var slot = Instantiate(slotprototype) as GameObject;
                slot.transform.SetParent(transform,false);
	            //slot.transform.parent = transform;
	            slot.transform.localPosition = new Vector3(x*55 - 250, y*55 - 120, 0);
	            slot.name = "Slot_" + i;
	            i++;
                Slots.Add(slot);
	        }
	    }
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
}