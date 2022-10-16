using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHealth : MonoBehaviour
{
    public Sprite[] HealthAnimation;
    SpriteRenderer spriteRenderer;
    float time = 0;
    int HealthAnimationCounter = 0;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        time += Time.deltaTime;
        if (time>0.1f)
        {
            spriteRenderer.sprite = HealthAnimation[HealthAnimationCounter++];
            if (HealthAnimation.Length == HealthAnimationCounter)
            {
                HealthAnimationCounter = HealthAnimation.Length - 1;
            }
            time = 0;
        }
    }
}
