using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI invText;
    [SerializeField] private InventoryComponent inventory;
    void Start()
    {
        invText.text = "Items: " + inventory.GetItem(0).ammo;
    }

    // Update is called once per frame
    void Update()
    {
        invText.text = "Items: " + inventory.GetItem(0).ammo;
    }
}
