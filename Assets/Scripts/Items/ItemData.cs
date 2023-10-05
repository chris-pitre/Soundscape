using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemBehavior{
    None,
    SoundEmitting,
    Blinding,
}

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    [Header("Item Details")]
    public string itemName;
    public Sprite icon;
    [TextArea] 
    [SerializeField] private string itemDescription;

    [Header("Item Behavior")]
    [SerializeField] public ItemBehavior behavior;
}
