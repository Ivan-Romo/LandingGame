using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    private float gravityScale = 0.1f;

    private float horizontalForce = 5.5f;

    private float verticalForce = 7f;

    private Rigidbody2D rb;

    private float time = 0f;

    private bool gameStarted = false;

    public int points;
    private bool touchLeft = false;
    private bool touchRight = true;
    private float timeWithoutTouching  = 0f;
    private float timeTutorial = 0f;


    public float smoothSpeed = 0.8f; // Velocidad de suavizado
    public float startPosition = -0.6f; // Posición inicial
    public float endPosition = 0.6f; // Posición final
    private float targetPosition; // Posición objetivo
    private bool movingRight = true; // Controla la dirección del movimiento

    void Start()
    {
        InfoPlayer.LoadInfo();
        rb = GetComponent<Rigidbody2D>();
        if(InfoPlayer.sr != null)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = InfoPlayer.sr;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeTutorial += Time.deltaTime;
        if (!GameManager.abortTutorial)
        {
            if (timeTutorial > 0.1)
            {
                rb.transform.position = Vector3.Lerp(rb.transform.position, new Vector3(targetPosition, rb.transform.position.y, rb.transform.position.z), smoothSpeed * Time.deltaTime);

                // Si estamos cerca del objetivo, cambiamos la dirección
                if (Mathf.Abs(rb.transform.position.x - targetPosition) < 0.1f)
                {
                    if (movingRight)
                    {
                        targetPosition = startPosition;
                    }
                    else
                    {
                        targetPosition = endPosition;
                    }
                    movingRight = !movingRight;
                }
            }


        }


        this.transform.rotation = new Quaternion(0,0,0,0);
        rb.angularVelocity = 0;

        if (Settings.oneHandMode)
        {
            if (Input.touchCount > 0)
            {
                touchLeft = true;
                touchRight = false;
            }
            else
            {
                timeWithoutTouching += Time.deltaTime;
                if (timeWithoutTouching > 0.1f)
                {
                    timeWithoutTouching = 0;
                    touchLeft = false;
                    touchRight = true;
                }
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                GameManager.abortTutorial = true;
                touchLeft = false;
                touchRight = false;

                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.GetTouch(i);

                    float touchX = touch.position.x / Screen.width;

                    if (touchX < 0.5f)
                    {

                        touchLeft = true;
                    }
                    else
                    {
                        touchRight = true;
                    }
                }
                
            }
            else
            {
                timeWithoutTouching += Time.deltaTime;
                if (timeWithoutTouching > 0.1f)
                {
                    timeWithoutTouching = 0;
                    touchLeft = false;
                    touchRight = false;
                }
            }

        }

    }

    void FixedUpdate()
    {

        time = 0;

        float upForce;

        if (gravityScale > 0)
            upForce = 1;
        else
            upForce = -1f;

        if (Input.GetKey(KeyCode.LeftArrow) || touchLeft)
        {
            Vector3 movimiento = new Vector3(-1, 0, 0);
            rb.AddForce(movimiento * horizontalForce);

        }

        if (Input.GetKey(KeyCode.RightArrow) ||touchRight)
        {
            Vector3 movimiento = new Vector3(1, 0, 0);
            rb.AddForce(movimiento * horizontalForce);

            }
        Vector2 velocidadObjetivo = new Vector3(0, rb.velocity.y);

        rb.velocity = Vector2.Lerp(rb.velocity, velocidadObjetivo, 0.1f);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ChangeGravity")
        {
            points++;

            if(points % 5 == 0)
            {
                horizontalForce += 0.5f;

            }

            if(points % 20 == 0)
            {
                horizontalForce += 0.5f;
            }
            gravityScale *= -1;

            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = gravityScale;

            if (gravityScale > 0)
            {
                gravityScale += 0.01f;
            }
            else
            {
                gravityScale -= 0.01f;

                //StartCoroutine(MoverGradualmente(top, new Vector3(top.transform.position.x, top.transform.position.y + 0.4f, top.transform.position.z), 10));


                //bottom.transform.position = new Vector3(bottom.transform.position.x, bottom.transform.position.y - 0.4f, bottom.transform.position.z);

            }
        }
        else if(collision.gameObject.tag == "Stop")
        {
            if (gameStarted)
            {
                SceneManager.LoadScene("Over");

            }
            else
            {
                gravityScale *= -1;

                this.gameObject.GetComponent<Rigidbody2D>().gravityScale = gravityScale;

                if (gravityScale > 0)
                    gravityScale += 0.02f;
                else
                    gravityScale -= 0.02f;
                gameStarted = true;
            }

            
        }
    }

}
