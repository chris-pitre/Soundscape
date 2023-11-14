using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class PostProccessingComponent : MonoBehaviour
{
    [SerializeField] private Volume volume;
    public Vignette vignette;
    private FilmGrain filmGrain;

    // Start is called before the first frame update
    void Start()
    {
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out filmGrain);
        //vignette.color.value = Color.red;
        
    }

    // Update is called once per frame
    void Update()
    {
        //vignette.color.value = Color.Lerp(Color.red, Color.black, Time.time);
        
    }

    public IEnumerator Hurt()
    {
        for (float time = 0; time <= 1.05f; time += 0.05f)
        {
            vignette.color.value = Color.Lerp(Color.red, Color.black, time);
            yield return null;
        }
        StopAllCoroutines();
    }
}
