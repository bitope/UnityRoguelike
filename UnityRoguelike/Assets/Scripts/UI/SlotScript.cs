using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityRoguelike;

public class SlotScript : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public Item Item;
    public int SourceSlot;

    private Image itemImage;

    // Dragging
    private Transform startParent;
    private Vector3 dragStartPos;

    void Start()
    {
        var child = transform.FindChild("ItemImage");
        if (child!=null)
            itemImage = child.GetComponent<Image>();

        if (itemImage == null)
            itemImage = transform.GetComponent<Image>();

        Update();
    }

    void Update()
    {
        if (Item != null && !String.IsNullOrEmpty(Item.Icon))
        {
            //itemImage.enabled = true;
            itemImage.color = new Color(1f, 1f, 1f, 1f);
            itemImage.sprite = Item.ItemIcon;
        }
        else
        {
            itemImage.color = new Color(1f,1f,1f,0f);
            //itemImage.enabled = false;
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

        if (!IsValidDrop(source.Item, name))
        {
            Debug.Log("Not valid drop: ");
            return;
        }

        var tmp = dest.Item;
        dest.Item = source.Item;
        source.Item = tmp;

        GameManagerScript.stage.Player.Inventory[source.SourceSlot] = source.Item;
        GameManagerScript.stage.Player.Inventory[dest.SourceSlot] = dest.Item;
    }

    private bool IsValidDrop(UnityRoguelike.Item item, string name)
    {
        if (name.StartsWith("Slot"))
            return true;

        if (name==item.Equipmentslot.ToString())
            return true;

        return false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDrag: " + name);
        startParent = transform.parent;
        transform.SetParent(transform.parent.parent);
        //transform.SetAsFirstSibling();
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        //GetComponent<LayoutElement>().ignoreLayout = true;
        dragStartPos = transform.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag: " + name);
        transform.SetParent(startParent);
        //GetComponent<LayoutElement>().ignoreLayout = false;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.position = dragStartPos;
    }

}