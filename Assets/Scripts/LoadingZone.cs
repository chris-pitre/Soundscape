using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingZone : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator anim;
    [SerializeField] private TextMeshProUGUI winText;
    private bool win = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            anim.SetTrigger("StartFade");
            winText.gameObject.SetActive(true);
            win = true;
        }
    }
    private void Update() {
        if(win){
            if(Input.anyKeyDown){
                SceneScriptManager.Instance.ChangeScene(0);
            }
        }
    }
}
