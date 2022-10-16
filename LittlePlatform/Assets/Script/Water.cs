using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public Sprite[] WaterAnimation;
    SpriteRenderer spriteRenderer;
    float time = 0;
    int WaterAnimationCounter;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        time += Time.deltaTime;
        if (time > 0.03f)
        {
            spriteRenderer.sprite = WaterAnimation[WaterAnimationCounter++];
            if (WaterAnimation.Length == WaterAnimationCounter)
            {
                WaterAnimationCounter = 0;
            }
            time = 0;
        }
    }
}
