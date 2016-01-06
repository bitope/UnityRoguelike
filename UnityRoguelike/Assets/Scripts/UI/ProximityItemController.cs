using Dungeon;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProximityItemController : MonoBehaviour
{
    public Text textbox;
    private Image image;

	// Use this for initialization
	void Start () {
	    textbox = transform.FindChild("foo").GetComponent<Text>();
        image = transform.FindChild("Image").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void SetInfo(string t, string icon)
    {
        textbox = transform.FindChild("foo").GetComponent<Text>();
        image = transform.FindChild("Image").GetComponent<Image>();

        var tt = textbox.GetComponent<Text>();
        tt.text = t;

        image.sprite = SpriteResourceManager.Get(icon);
    }
}
