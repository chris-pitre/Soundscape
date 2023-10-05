using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For use as a game object
public class Item : MonoBehaviour
{
    [Header("Item Info")]
    public ItemData itemData;
    private SoundComponent sound;
    public void Use(){
        switch(itemData.behavior){
            case ItemBehavior.SoundEmitting:
                sound = gameObject.AddComponent<SoundComponent>();
                sound.MakeSoundImpulse(2f, 2f);
                break;
        }
    }
}
