using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public Image itemImage; // drag manual dari prefab
    
    public void SetItem(Sprite newSprite)
    {
        itemImage.sprite = newSprite;
    }
}
