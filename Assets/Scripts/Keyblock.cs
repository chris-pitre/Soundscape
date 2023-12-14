using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyblock : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource audioSource;
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other);
        switch (other.tag){
            case "Player":
                if(other.gameObject.GetComponent<PlayerManager>().GetKeys() >= 3){
                    audioSource.Play();
                    StartCoroutine(WaitForSound());
                }
                break;
        }
    }

    private IEnumerator WaitForSound(){
        while(audioSource.isPlaying){
            yield return null;
        }
        Destroy(this.gameObject);
    }

}
