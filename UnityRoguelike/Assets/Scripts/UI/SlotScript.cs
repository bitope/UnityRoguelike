using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityRoguelike;

public class SlotScript : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public Item Item;

    private Image itemImage;

    // Dragging
    private GameObject itemDragged;
    private Vector3 dragStartPos;

    void Start()
    {
        //itemImage = transform.GetChild(0).GetComponent<Image>();
        itemImage = transform.FindChild("ItemImage").GetComponent<Image>();
        Update();
    }

    void Update()
    {
        if (Item != null && !String.IsNullOrEmpty(Item.Icon))
        {
            itemImage.enabled = true;
            itemImage.sprite = Item.ItemIcon;
        }
        else
        {
            itemImage.enabled = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Tooltip: " + name);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Click: " + name);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var targets = "";
        foreach (var o in eventData.hovered)
        {
            targets += ", " + o.name;
        }
        
        Debug.Log("Drag: " + name+": "+targets);
        transform.position = Input.mousePosition;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop: " + name + ": "+ eventData.pointerDrag.name);
        var source = eventData.pointerDrag.GetComponent<SlotScript>();
        var dest = transform.GetComponent<SlotScript>();
        
        var tmp = dest.Item;
        dest.Item = source.Item;
        source.Item = tmp;

        GameManagerScript.stage.Player.Inventory[source.SourceSlot] = source.Item;
        GameManagerScript.stage.Player.Inventory[dest.SourceSlot] = dest.Item;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDrag: " + name);
        transform.SetAsFirstSibling();
        GetComponent<LayoutElement>().ignoreLayout = true;
        itemDragged = gameObject;
        dragStartPos = transform.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag: " + name);
        GetComponent<LayoutElement>().ignoreLayout = false;
        itemDragged = null;
        transform.position = dragStartPos;
    }

    public int SourceSlot { get; set; }
}