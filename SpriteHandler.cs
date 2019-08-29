using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteHandler : MonoBehaviour
{
    
    public SpriteRenderer spr_render;

    public Sprite SmallLeft;

    public Sprite SmallRight;

    public Sprite NormalLeft;

    public Sprite NormalRight;


    public void ChangeSpriteTo(Sprite spritename)
    {
        spr_render.GetComponent<SpriteRenderer>();
        spr_render.sprite = spritename;  
    }

    
}
