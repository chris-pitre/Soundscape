using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIComponent : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Text GUIText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)){
            GUIText.enabled = !GUIText.enabled;
        }

        GUIText.text = string.Format("Player pos: ({0:F2}, {1:F2}), {2:2F}", player.transform.position.x, player.transform.position.y, player.transform.rotation.eulerAngles.z);
        
    }
}
