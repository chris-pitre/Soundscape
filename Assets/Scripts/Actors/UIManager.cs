using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI invText;
    [SerializeField] private InventoryComponent inventory;
    [SerializeField] private Image healthBar;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private TextMeshProUGUI gameoverText;
    private bool dead = false;
    void Start()
    {
        invText.text = "Rocks: " + inventory.GetItem(0).ammo;
    }

    // Update is called once per frame
    void Update()
    {
        invText.text = "Rocks: " + inventory.GetItem(0).ammo;
        if(dead){
            if(Input.anyKeyDown){
                SceneScriptManager.Instance.ChangeScene(0);
            }
        }
    }

    public void UpdateHealth(float playerHP)
    {
        healthBar.rectTransform.localScale = new Vector3(playerHP, 1f, 1f);
    }

    public void ShowGameOverScreen()
    {
        gameoverText.gameObject.SetActive(true);
        invText.gameObject.SetActive(false);
        dead = true;
    }
}
