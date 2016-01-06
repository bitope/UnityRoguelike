using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityRoguelike;

public class DoorController : MonoBehaviour
{
    private bool isOpen;

    private GameObject openDoor;
    private GameObject closedDoor;
    public Tiles Tile;
    public Vec Position;

	// Use this for initialization
	void Start ()
	{
	    openDoor = transform.FindChild("DoorOpen").gameObject;
	    closedDoor = transform.FindChild("DoorClosed").gameObject;
	}
	
	// Update is called once per frame
	void Update () 
    {
        openDoor.SetActive(isOpen);
        closedDoor.SetActive(!isOpen);
	}

    public void SetOpen(bool open)
    {
        isOpen = open;
    }

    public void Activate()
    {
        isOpen = !isOpen;

        switch (Tile)
        {
            case Tiles.OpenDoor_NS:
                Tile = Tiles.ClosedDoor_NS;
                break;
            case Tiles.OpenDoor_EW:
                Tile = Tiles.ClosedDoor_EW;
                break;
            case Tiles.ClosedDoor_NS:
                Tile = Tiles.OpenDoor_NS;
                break;
            case Tiles.ClosedDoor_EW:
                Tile = Tiles.OpenDoor_EW;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        GameManagerScript.stage[Position.x, Position.y] = Tile;
    }
}
