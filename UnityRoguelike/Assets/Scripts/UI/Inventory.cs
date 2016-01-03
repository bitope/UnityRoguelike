using System.Collections.Generic;
using System.ComponentModel;
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
        //for (int y = 0; y < 5 ; y++)
	    {
	        for (int x = 0; x < 10; x++)
	        {
	            var slot = Instantiate(slotprototype) as GameObject;
                slot.transform.SetParent(transform,false);
	            slot.transform.localPosition = new Vector3(x*50 -4.5f*50, y*50 -12 -4.5f*50 , 0);
	            slot.name = "Slot_" + i;
	            i++;
                Slots.Add(slot);
	        }
	    }
	}

    void RegisterWithStage()
    {
        Debug.Log(name + " has registered.");
        GameManagerScript.stage.Player.PropertyChanged+=Player_PropertyChanged;
        UpdateInventory();
    }

    private void Player_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "Inventory")
        {
            UpdateInventory();
        }
    }

    private void UpdateInventory()
    {
        for (int slot = 0; slot < GameManagerScript.stage.Player.Inventory.Length; slot++)
        {
            var script = Slots[slot].GetComponent<SlotScript>();
            script.Item = GameManagerScript.stage.Player.Inventory[slot];
            script.SourceSlot = slot;
        }
    }

    // Update is called once per frame
	void Update ()
	{
	}
}