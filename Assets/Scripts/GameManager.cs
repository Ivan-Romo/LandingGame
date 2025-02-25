using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using UnityEngine.UI;

namespace System.Runtime.CompilerServices
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal class IsExternalInit { }
}

[System.Serializable]
public class LevelColors
{
    public LevelColors(string c,bool d,Color back, Color play, Color gnd)
    {
        color = c;
        discovered = d;
        background = back;
        player= play;
        ground = gnd;
    }
    public string color;
    public bool discovered;
    public Color background;
    public Color player;
    public Color ground;
}

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] List<AudioSource> audios = new List<AudioSource>();


    public ParticleSystem explosion;


    [SerializeField] GameObject objective;

    [SerializeField] GameObject player;

    [SerializeField] GameObject bottom;
    [SerializeField] GameObject top;

    [SerializeField] GameObject background;

    [SerializeField] GameObject popup;

    private Vector3 bottomPos;
    private Vector3 topPos;

    private float y = 2.55f;

    private float x = 1.2f;

    private float gravityScale;

    GameObject currentObjective;

    private bool tutorial = true;
    static public bool abortTutorial = false;

    [SerializeField] GameObject coin = null;

    GameObject actualCoin = null;

    private bool createCoin = false;

    private bool coinCorutineStarted = false;

    [SerializeField] Camera cam;
    void Start()
    {
        InfoPlayer.LoadInfo();
        gravityScale = player.gameObject.GetComponent<Rigidbody2D>().gravityScale;

        bottomPos = bottom.transform.position;
        topPos = top.transform.position;
    //    colors.Add(new LevelColors(new Color(182 / 255f, 218 / 255f, 159 / 255f), new Color(242 / 255f, 250 / 255f, 233 / 255f), new Color(231 / 255f, 244 / 255f, 206 / 255f)));
    //    colors.Add(new LevelColors(new Color(182 / 255f, 218 / 255f, 159 / 255f), new Color(242 / 255f, 250 / 255f, 233 / 255f), new Color(231 / 255f, 244 / 255f, 206 / 255f)));
    }

    // Update is called once per frame
    void Update()
    {

        if(actualCoin == null)
        {
            if (createCoin)
            {
                createCoin = false;
                actualCoin = Instantiate(coin);

                float randY = Random.Range(-2f, 2f);
                float randX = Random.Range(-1.1f, 1.1f);
                actualCoin.transform.transform.position = new Vector3(randX, randY, -2);
            }
            else if(!coinCorutineStarted)
            {
                StartCoroutine("coinDelay");
                coinCorutineStarted=true;
            }

        }
        else
        {
            coinCorutineStarted = false;
        }

        if (!abortTutorial)
        {
            player.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        else {
            if (tutorial)
            {
                player.gameObject.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
                tutorial = false;
            }

            if (InfoPlayer.colorDiscovered)
            {
                InfoPlayer.colorDiscovered = false;
                colorPopUp();
            }


            if (player.transform.position.x > (x + 0.3f))
            {
                player.GetComponent<TrailRenderer>().enabled = false;
                player.transform.position = new Vector3((x * -1) - 0.3f, player.transform.position.y, player.transform.position.z);
                StartCoroutine(enableTrail(player));

            }
            else if (player.transform.position.x < (x * -1f - 0.3f))
            {
                player.GetComponent<TrailRenderer>().enabled = false;
                player.transform.position = new Vector3(x + 0.3f, player.transform.position.y, player.transform.position.z);
                StartCoroutine(enableTrail(player));
            }

            if (gravityScale != player.gameObject.GetComponent<Rigidbody2D>().gravityScale)
            {
                if (player.GetComponent<Player>().points % 5 == 0)
                {
                    objective.transform.localScale = new Vector3(objective.transform.localScale.x - objective.transform.localScale.x * 0.05f,
                                                                 objective.transform.localScale.y,
                                                                 -1);

                    //Llenar la lista de posibles colores. ?????? No se q quiere decir este comentario.

                    int index = InfoPlayer.getColor(player.GetComponent<Player>().points);


                    StartCoroutine(ChangeColor(background, InfoPlayer.colors[index].background, 0.3f));
                    //StartCoroutine(ChangeColor(objective, colors[index].player, 0.3f));
                    StartCoroutine(ChangeColor(player, InfoPlayer.colors[index].player, 0.3f));
                    StartCoroutine(ChangeColor(top, InfoPlayer.colors[index].ground, 0.3f));
                    StartCoroutine(ChangeColor(bottom, InfoPlayer.colors[index].ground, 0.3f));
                    popup.GetComponent<Image>().color = InfoPlayer.colors[index].background;
                    objective.GetComponent<SpriteRenderer>().color = InfoPlayer.colors[index].player;
                    //player.GetComponent<SpriteRenderer>().color = colors[index].player;
                    //top.GetComponent<SpriteRenderer>().color = colors[index].ground;
                    //bottom.GetComponent<SpriteRenderer>().color = colors[index].ground;
                    //background.GetComponent<SpriteRenderer>().color = colors[index].background;
                }


                gravityScale = player.gameObject.GetComponent<Rigidbody2D>().gravityScale;

                if (currentObjective != null)
                {
                    audios[((player.GetComponent<Player>().points - 1) % 5)].Play();
                    explosion.transform.position = currentObjective.transform.position;
                    explosion.Play();
                    Destroy(currentObjective);
                }

                currentObjective = Instantiate(objective);
                float xAux = Random.Range(x * -1, x);

                float yAux;
                if (gravityScale > 0)
                {
                    yAux = y * -1;
                    StartCoroutine(MoverGradualmente(top, new Vector3(top.transform.position.x, top.transform.position.y + 0.4f, top.transform.position.z), 10));
                    if (bottom.transform.position != bottomPos)
                        StartCoroutine(MoverGradualmente(bottom, bottomPos, 10));

                    currentObjective.transform.position = new Vector3(xAux, yAux - 0.5f, -1);
                    StartCoroutine(MoverGradualmente(currentObjective, new Vector3(xAux, yAux, -1), 20));

                }
                else
                {
                    yAux = y;

                    StartCoroutine(MoverGradualmente(bottom, new Vector3(bottom.transform.position.x, bottom.transform.position.y - 0.4f, bottom.transform.position.z), 10));
                    if (top.transform.position != topPos)
                        StartCoroutine(MoverGradualmente(top, new Vector3(top.transform.position.x, top.transform.position.y - 0.4f, top.transform.position.z), 10));

                    currentObjective.transform.position = new Vector3(xAux, yAux + 0.5f, -1);
                    StartCoroutine(MoverGradualmente(currentObjective, new Vector3(xAux, yAux, -1), 20));

                }
            }
        }
    }

    IEnumerator MoverGradualmente(GameObject gb, Vector3 end, float velocity)
    {
        // Calcula la nueva posición

        while (Vector3.Distance(gb.transform.position, end) > 0.01f)
        {
            // Interpola suavemente hacia la nueva posición
            gb.transform.position = Vector3.Lerp(
                gb.transform.position,
                end,
                velocity * Time.deltaTime
            );

            yield return null;
        }
        // Asegura que la posición final sea exacta
        gb.transform.position = end;
    }

    IEnumerator ChangeColor(GameObject go, Color colorObjetivo, float duracion)
    {
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();

        Color colorInicial = spriteRenderer.color;
        float tiempoInicio = Time.time;

        while (Time.time - tiempoInicio < duracion)
        {
            float progreso = (Time.time - tiempoInicio) / duracion;
            Color colorInterpolado = Color.Lerp(colorInicial, colorObjetivo, progreso);
            spriteRenderer.color = colorInterpolado;
            yield return null; // Esperar un frame antes de continuar
        }

        // Asegurar que el color final sea exacto
        spriteRenderer.color = colorObjetivo;
    }

    public void colorPopUp()
    {
        if (gravityScale > 0)
        {
            popup.transform.position = new Vector3(2.31f, 2.06f, 0);
            StartCoroutine(MoverGradualmente(popup, new Vector3(0.36f, 2.06f, 0), 10));
            StartCoroutine(waitSecond(2.06f));
        }
        else
        {
            popup.transform.position = new Vector3(2.31f,-2.06f, 0);
            StartCoroutine(MoverGradualmente(popup, new Vector3(0.36f, -2.06f, 0), 10));
            StartCoroutine(waitSecond(-2.06f));
        }
    }

    IEnumerator waitSecond(float y)
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(MoverGradualmente(popup, new Vector3(2.31f, y, 0), 10));
    }

    IEnumerator coinDelay()
    {
        yield return new WaitForSeconds(2f);
        createCoin = true;
    }


    IEnumerator enableTrail(GameObject player)
    {
        yield return new WaitForSeconds(0.3f);
        
        player.GetComponent<TrailRenderer>().enabled = true;
    }

}
