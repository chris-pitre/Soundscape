using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScriptManager : MonoBehaviour
{
    [Header("References")]
    public Animator transition;
    private static SceneScriptManager _instance;
    public static SceneScriptManager Instance{ get { return _instance;}}
    private void Awake(){
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
    }

    public void ChangeScene(int sceneNum)
    {
        StartCoroutine(ChangeSceneCoroutine(sceneNum));
    }

    private IEnumerator ChangeSceneCoroutine(int sceneNum){
        transition.SetTrigger("StartFade");
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(sceneNum);
    }
}
