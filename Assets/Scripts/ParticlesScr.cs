using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesScr : MonoBehaviour
{
    [SerializeField] GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.transform.position = player.transform.position;

        Debug.Log(player.GetComponent<Rigidbody2D>().velocity.magnitude);

        if (player.GetComponent<Rigidbody2D>().velocity.magnitude < 0.1f)
        {
            this.GetComponent<ParticleSystem>().Pause();
        }
        else
        {

        }

    }
}
