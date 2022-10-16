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
    public Text GoldCounterText;
    public Text GameOverText;
    //public Text CompletedText;
    public Image BlackBackground;
    //public Image GreenBackground;
    int Health = 100;
    int nextScene;
    int WaitingAnimationCounter = 0;
    int goldCounter = 0;
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
    //float GreenBackgroundCounter = 0;
    float GameOverCounter = 0;
    float MainMenuTime = 0;
    //float NextLevelTime = 0;
    //float CompletedCounter = 0;

    GameObject Camera;
    
    void Start()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Record", int.Parse(SceneManager.GetActiveScene().name));
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
            GoldCounterText.text= "GOLD : " + goldCounter;
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
        if (col.gameObject.tag == "Water")
        {
            Health -= 50;
            HealthText.text = "Health : " + Health;
        }
        if (col.gameObject.tag == "Finished")
        {
            PlayerPrefs.SetInt("RecordHealth", Health);
            Health = PlayerPrefs.GetInt("RecordHealth");
            HealthText.text = "Health : " + Health;
            nextScene = int.Parse(SceneManager.GetActiveScene().name) + 1;
            SceneManager.LoadScene($"{nextScene}");
        }
        if (col.gameObject.tag == "10Health")
        {
            if (Health >= 90)
            {
                Health = 100;
                HealthText.text = "Health : " + Health;
            }
            else
            {
                Health += 10;
                HealthText.text = "Health : " + Health;
            }
            col.GetComponent<BoxCollider2D>().enabled = false;
            col.GetComponent<GetHealth>().enabled = true;
            Destroy(col.gameObject, 1.5f);

        }
        if (col.gameObject.tag == "Border")
        {
            Health = 0;
            HealthText.text = "Health : " + Health;
            
        }
        
        if (col.gameObject.tag == "Gold")
        {
            goldCounter++;
            GoldCounterText.text = "GOLD : " + goldCounter;
            Destroy(col.gameObject);
        }

        //if (col.gameObject.tag == "Finished")
        //{
        //    CompletedText.text = "COMPLETED";
        //    NextLevelTime += Time.deltaTime;
        //    Debug.Log(Time.deltaTime);
        //    Debug.Log(NextLevelTime);
        //    if (NextLevelTime > 0.02f)
        //    {
        //        nextScene = int.Parse(SceneManager.GetActiveScene().name) + 1;
        //        SceneManager.LoadScene($"{nextScene}");
        //    }
        //}
    }


    //void NextLevelAnim()
    //{
    //    Time.timeScale = 0.4f;
    //    HealthText.enabled = false;
    //    CompletedCounter += 0.01f;
    //    CompletedText.text = "COMPLETED";
    //    CompletedText.color = new Color(CompletedCounter, CompletedCounter, CompletedCounter);
    //    GreenBackgroundCounter += 0.03f;
    //    GreenBackground.color = new Color(45, 190, 40, GreenBackgroundCounter);
    //}

    void CameraControl()
    {
        CameraLastPosition = CameraFirstPosition + transform.position;
        Camera.transform.position = Vector3.Lerp(Camera.transform.position, CameraLastPosition, 0.1f);
    }
}
