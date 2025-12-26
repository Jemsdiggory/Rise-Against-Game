using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<ItemData> collectedItems = new List<ItemData>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddItem(ItemData newItem)
    {
        if (newItem == null)
        {
            Debug.LogWarning("Item yang mau ditambahkan ke inventory kosong/null.");
            return;
        }

        collectedItems.Add(newItem);
        Debug.Log($"Item {newItem.itemName} ditambahkan ke inventory.");
    }
}
