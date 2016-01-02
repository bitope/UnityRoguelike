using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityRoguelike;

public class SlotScript : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    public Item Item;

    private Image itemImage;

    void Start()
    {
        //itemImage = transform.GetChild(0).GetComponent<Image>();
        itemImage = transform.FindChild("ItemImage").GetComponent<Image>();
    }

    void Update()
    {
        if (Item != null)
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
}