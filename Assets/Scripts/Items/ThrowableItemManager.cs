using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SoundComponent))]
public class ThrowableItemManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SoundComponent sound;

    public Vector2 start;
    public Vector2 target;

    private float t;

    private void Start(){
        start = transform.position;
    }
    private void Update(){
        transform.position = Vector2.Lerp(start, target, t);
        t += 0.5f * Time.deltaTime;
    }
}
