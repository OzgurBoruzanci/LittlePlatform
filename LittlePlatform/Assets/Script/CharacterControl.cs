using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterControl : MonoBehaviour
{
    public Sprite[] WaitingAnimation;
    public Sprite[] JumpAnimation;
    public Sprite[] WalkingAnimation;
    public Text HealthText;
    public Text GameOverText;
    public Image BlackBackground;
    int Health = 100;

    int WaitingAnimationCounter = 0;
    //int JumpAnimationCounter = 0;
    int WalkingAnimationCounter = 0;

    SpriteRenderer spriteRenderer;
    Rigidbody2D physics;

    Vector3 vec;
    Vector3 CameraLastPosition;
    Vector3 CameraFirstPosition;

    bool JumpOnce = true;

    float horizontal = 0;
    float WaitingAnimationTime = 0;
    float WalkingAnimationTime = 0;
    float BlackBackgroundCounter = 0;
    float GameOverCounter = 0;
    float MainMenuTime = 0;

    GameObject Camera;

    void Start()
    {
        PlayerPrefs.GetInt("Record", int.Parse(SceneManager.GetActiveScene().name));
        spriteRenderer = GetComponent<SpriteRenderer>();
        physics = GetComponent<Rigidbody2D>();
        Camera = GameObject.FindGameObjectWithTag("MainCamera");

        CameraFirstPosition = Camera.transform.position - transform.position;
        HealthText.text = "Health : " + Health;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (JumpOnce)
            {
                physics.AddForce(new Vector2(0, 500));
                JumpOnce = false;
            }

        }
    }

    void FixedUpdate()
    {
        CharacterMovement();
        Animation();
        if (Health<=0)
        {
            Time.timeScale = 0.4f;
            HealthText.enabled = false;
            BlackBackgroundCounter += 0.03f;
            GameOverCounter += 0.01f;
            BlackBackground.color = new Color(0, 0, 0, BlackBackgroundCounter);
            GameOverText.text = "GAME OVER";
            GameOverText.color = new Color(GameOverCounter, GameOverCounter, GameOverCounter);
            MainMenuTime += Time.deltaTime;
            if (MainMenuTime>1.3f)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    private void LateUpdate()
    {
        CameraControl();
    }

    void CharacterMovement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vec = new Vector3(horizontal * 10, physics.velocity.y, 0);
        physics.velocity = vec;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        JumpOnce = true;
    }

    void Animation()
    {
        if (JumpOnce)
        {
            if (horizontal == 0)
            {
                WaitingAnimationTime += Time.deltaTime;
                if (WaitingAnimationTime > 0.1f)
                {
                    spriteRenderer.sprite = WaitingAnimation[WaitingAnimationCounter++];
                    if (WaitingAnimationCounter == WaitingAnimation.Length)
                    {
                        WaitingAnimationCounter = 0;
                    }
                    WaitingAnimationTime = 0;

                }

            }
            else if (horizontal > 0)
            {
                WalkingAnimationTime += Time.deltaTime;
                if (WalkingAnimationTime > 0.01f)
                {
                    walkingAnimation();
                }
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontal < 0)
            {
                WalkingAnimationTime += Time.deltaTime;
                if (WalkingAnimationTime > 0.01f)
                {
                    walkingAnimation();
                }
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            if (physics.velocity.y>0)
            {
                spriteRenderer.sprite = JumpAnimation[0];
            }
            else
            {
                spriteRenderer.sprite = JumpAnimation[1];
            }
            if (horizontal>0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontal<0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
       

    }
    void walkingAnimation()
    {
        spriteRenderer.sprite = WalkingAnimation[WalkingAnimationCounter++];
        if (WalkingAnimationCounter == WalkingAnimation.Length)
        {
            WalkingAnimationCounter = 0;
        }
        WalkingAnimationTime = 0;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag=="Bullet")
        {
            Health--;
            HealthText.text = "Health : " + Health;
        }
        if (col.gameObject.tag == "Saw")
        {
            Health-=20;
            HealthText.text = "Health : " + Health;
        }
        if (col.gameObject.tag == "Mace")
        {
            Health -= 10;
            HealthText.text = "Health : " + Health;
        }
    }

    void CameraControl()
    {
        CameraLastPosition = CameraFirstPosition + transform.position;
        Camera.transform.position = Vector3.Lerp(Camera.transform.position, CameraLastPosition, 0.1f);
    }
}
