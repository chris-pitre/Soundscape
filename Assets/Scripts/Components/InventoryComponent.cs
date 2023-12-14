using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField] private List<ItemInstance> items = new();

    public void AddItem(ItemInstance item){
        items.Add(item);
    }

    public void RemoveItem(ItemInstance item){
        items.Remove(item);
    }

    public ItemInstance GetItem(int index){
        return items[index];
    }

    
}
