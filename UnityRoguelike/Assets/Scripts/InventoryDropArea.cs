using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class InventoryDropArea : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("Drop: " + name + ": " + eventData.pointerDrag.name);
        var source = eventData.pointerDrag.GetComponent<SlotScript>();

        if (!IsValidDrop(source.Item, name))
        {
            // Cant drop cursed items.
            Debug.Log("Not valid drop: Source");
            return;
        }

        var item = source.Item;
        source.Item = null;
        GameManagerScript.stage.Player.SetInventory(source.SourceSlot, source.Item);

        if (item!=null)
            GameManagerScript.ItemDroppedBy(item,GameManagerScript.stage.Player);
    }

    private bool IsValidDrop(UnityRoguelike.Item item, string name)
    {
        if (item == null)
            return true;

        return !item.isCursed();
    }
}
