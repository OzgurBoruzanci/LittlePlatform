using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public Sprite[] GoldhAnimation;
    SpriteRenderer spriteRenderer;
    float time = 0;
    int GoldAnimationCounter = 0;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        time += Time.deltaTime;
        if (time > 0.03f)
        {
            spriteRenderer.sprite = GoldhAnimation[GoldAnimationCounter++];
            if (GoldhAnimation.Length == GoldAnimationCounter)
            {
                GoldAnimationCounter = 0;
            }
            time = 0;
        }
    }
}
