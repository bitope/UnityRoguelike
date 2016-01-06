using System;
using Dungeon;
using UnityEngine;
using System.Collections;
using UnityRoguelike;

public class ItemController : MonoBehaviour
{
    public Item Item;

    private GameObject _visual;

    // Use this for initialization
	void Start () {
        //Item.SetItemIcon();
	    _visual = transform.GetChild(0).gameObject;
        UpdateGraphics();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateGraphics()
    {
        if (_visual == null)
        {
            Debug.LogError("Trying to set Graphics before visual exists.");
            return;
        }

        var mr = _visual.GetComponent<MeshRenderer>();
        if (mr != null)
        {
            Material material = Instantiate(mr.material);
            material.mainTexture = SpriteResourceManager.Get(Item.Icon).texture;
            mr.material = material;
        }
    }
}
