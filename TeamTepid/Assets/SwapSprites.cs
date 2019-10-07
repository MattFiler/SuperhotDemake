using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapSprites : MonoBehaviour
{
    public enum SpriteType {DEFAULT = 0, BOTTLE = 1, KATANA = 2, PISTOL = 3, SHOTGUN = 4, ASSAULT_RIFLE = 5 };
    public SpriteType currentSpriteType = SpriteType.DEFAULT;

    public Sprite[] sprites = new Sprite[6];

   public  void swapSprite(SpriteType spriteType)
   {
        currentSpriteType = spriteType;
        Debug.Log((int)spriteType);
        Debug.Log(sprites[(int)spriteType].name);
        GetComponent<SpriteRenderer>().sprite = sprites[(int)spriteType];
   }
}
