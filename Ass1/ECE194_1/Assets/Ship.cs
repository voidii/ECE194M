using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    public Rigidbody2D rb;
    public float thrust;
    public float turnThrust;
    public float bulletForce;
    public int health;

    private float thrustInput;
    private float turnInput;
    public int score;
    public int lives;
    public GameObject sound;
    public float time;
    public float tot_time;

    public Text scoreText;
    public Text livesText;
    public Text healthText;

    public GameObject bullet;
    public GameObject gameOverPanel;
    public GameObject PausePanel;

    public Color Wudicolor;
    public Color norColor;

    public GameObject As;

    void Start(){
        score = 0;

        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
        healthText.text = "Health: " + health;
    }

    void Update(){
        
        thrustInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        if(Input.GetButtonDown("Fire1"))
        {
            GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
            newBullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * bulletForce);
            Destroy(newBullet, 4.0f);
        }

        if(Input.GetKeyDown("space"))
        {
            rb.drag = 5;
        }

        if(Input.GetKeyUp("space"))
        {
            rb.drag = 0.1f;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        
        Vector2 newPos = transform.position;

        if(transform.position.y > 39)
        {
            newPos.y = -39;
        }

        if(transform.position.y < -39)
        {
            newPos.y = 39;
        }

        if(transform.position.x > 52)
        {
            newPos.x = -52;
        }

        if(transform.position.x < -52)
        {
            newPos.x = 52;
        }

        transform.position = newPos;

        time += Time.deltaTime;
        tot_time += Time.deltaTime;
        if(time > 5f)
        {
            time = 0;
            Generate();
        }

    }

    void FixedUpdate()
    {
        rb.AddRelativeForce(Vector2.up * thrustInput * thrust);
        rb.AddTorque(-1 * turnInput * turnThrust);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        health = health - Mathf.RoundToInt(col.relativeVelocity.magnitude);
        Debug.Log(health);
        healthText.text = "Health: " + health;
        if(health <= 0)
        {
            Debug.Log("DEAD");
            GameObject s = Instantiate(sound, transform.position, transform.rotation);
            Destroy(s, 2f);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Invoke("Respawn", 3f);
            lives--;
            health = 100;
            livesText.text = "Lives: " + lives;
            healthText.text = "Health: " + health;

            if(lives < 0)
            {
                Debug.Log("Game Over");
                GameOver();
            }
        }
    }

    void ScorePoints(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = "Score: " + score;
    }

    void Respawn()
    {
        Vector2 RebornPos;
        RebornPos.x = 0;
        RebornPos.y = 0;
        transform.position = RebornPos;
        rb.velocity = Vector2.zero;
        
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.enabled = true;
        sr.color = Wudicolor;
        Invoke("Wudi", 3f);
    }

    void Wudi()
    {
        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().color = norColor;
    }

    void GameOver()
    {
        CancelInvoke();
        gameOverPanel.SetActive(true);
    }

    void Pause()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Continue()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Start");
        Time.timeScale = 1;
    }

    void Generate()
    {
        Vector2 randomVector1 = new Vector2(51, Random.Range(-38, 38));
        Vector2 randomVector2 = new Vector2(Random.Range(-51, 51), 38);
        Vector2 randomVector3 = new Vector2(-51, Random.Range(-38, 38));
        Vector2 randomVector4 = new Vector2(Random.Range(-51, 51), -38);
        Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        for(int i = 0; i < tot_time % 30; i++)
        {
            if(score % 2 == 0)
            {
                Instantiate(As, randomVector1, randomRotation);
                Instantiate(As, randomVector2, randomRotation);
            }
            else
            {
                Instantiate(As, randomVector3, randomRotation);
                Instantiate(As, randomVector4, randomRotation);
            }
        }
    }
}
