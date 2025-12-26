using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    public GameObject itemSlotPrefab;
    public Transform[] itemSlotParents; //  ARRAY buat 3 lokasi spawn
    public int maxSlots = 3; // max 3 item 

    private void OnEnable()
    {
        RefreshInventory();
    }

    public void RefreshInventory()
{
    //clear semua slot di semua parent
    foreach (Transform parent in itemSlotParents)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }

    int slotCount = 0;

    foreach (ItemData item in InventoryManager.Instance.collectedItems)
    {
        if (item == null) continue;
        if (slotCount >= maxSlots) break;

        //innstantiate prefab biasa 
        GameObject newSlot = Instantiate(itemSlotPrefab);

        //set parent ke container ke-slotCount
        newSlot.transform.SetParent(itemSlotParents[slotCount], false); 

        //reset local position 
        newSlot.transform.localPosition = Vector3.zero; 
        newSlot.transform.localRotation = Quaternion.identity;
        newSlot.transform.localScale = Vector3.one;

        // slot sprite
        newSlot.GetComponent<Image>().sprite = item.itemIcon;

        slotCount++;
    }
}

}
