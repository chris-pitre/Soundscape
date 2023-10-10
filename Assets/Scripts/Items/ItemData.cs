using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    [Header("Item Details")]
    public string itemName;
    public Sprite icon;
    [TextArea] 
    [SerializeField] private string itemDescription;

    [Header("Prefab")]
    [SerializeField] public GameObject gameObject;
}
