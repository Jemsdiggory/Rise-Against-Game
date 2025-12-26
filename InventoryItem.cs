using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public Sprite itemSprite;
    public string itemName;  // nama item

    // pas item diambil
    public void AddItem(Sprite sprite, string name)
    {
        itemSprite = sprite;  // save sprite item
        itemName = name;      // save nama item
    }
}
