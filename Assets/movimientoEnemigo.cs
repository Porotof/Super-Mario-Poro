using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoEnemigo : MonoBehaviour
{
    public float velocidadMaxima = 1f;
    public float velocidad = 1f;
    public string tipo;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2d.AddForce(Vector2.right * velocidad);
        float limiteVelocidad = Mathf.Clamp(rb2d.velocity.x, -velocidadMaxima, velocidadMaxima);
        rb2d.velocity = new Vector2(limiteVelocidad, rb2d.velocity.y);

        if(rb2d.velocity.x > -0.01f && rb2d.velocity.x < 0.0f)
        {
            velocidad = -velocidad;
            rb2d.velocity = new Vector2(velocidad, rb2d.velocity.y);
        }

        if (velocidad < 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }else if (velocidad > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D colision)
    { 
        if(colision.gameObject.tag == "Player")
        {
            float yOffset = 0.8f; // posiciónYDeEnemigo - PosiciónYDeJugadorSaltandoEnEnemigo = 1.2 aprox.
            if((transform.position.y + yOffset) < colision.transform.position.y && tipo != "Pinchudo")
            {
                colision.SendMessage("saltoSobreEnemigo"); //SendMessenge llama método de objeto
                Destroy(gameObject);
            }
            else
            {
                colision.SendMessage("saltoRetrocesoDanioEnemigo", transform.position.x);
                colision.SendMessage("disminuirPuntaje");
            }

            //Debug.Log("Tocó jugador!");
        }
    }
}
