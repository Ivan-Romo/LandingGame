using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimation : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rb;
    private float timeTutorial = 0f;


    public float smoothSpeed = 0.8f; // Velocidad de suavizado
    public float startPosition = -0.6f; // Posición inicial
    public float endPosition = 0.6f; // Posición final
    private float targetPosition; // Posición objetivo
    private bool movingRight = true; // Controla la dirección del movimiento
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timeTutorial += Time.deltaTime;
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
}
