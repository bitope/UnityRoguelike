using UnityEngine;
using System.Collections;
using UnityRoguelike;

public class ItemController : MonoBehaviour
{
    public Item Item;

    // Use this for initialization
	void Start () {
        Item.SetItemIcon();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
