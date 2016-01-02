using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DoorController : MonoBehaviour
{

    private bool isOpen;

    private GameObject openDoor;
    private GameObject closedDoor;

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
    }
}
