using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Para poder usar tipo de dato Text con el Canvas

public class Mario_controlar : MonoBehaviour
{
    public Camera camaraPrincipal;
    public int puntaje;
    public Text textoPuntaje;
    public float maxVel = 10f;
    public float velocidad = 25f;
    public bool tocandoSuelo;
    public float fuerzaSalto = 6.5f;
    public Vector2 rangoHorizontal = Vector2.zero; //Límites horizontales por los que Mario puede transitar
    public Vector2 rangoVertical = Vector2.zero; //Límites verticales por los que Mario puede transitar

    public AudioClip saltarClip;
    public AudioClip morirClip;
    public AudioClip recibirDanioClip;
    public AudioClip eliminarEnemigoClip;
    public AudioClip recogerMonedaClip;
    private AudioClip musicaFondo;

    private Rigidbody2D rb2d;
    private Animator animacion;
    private SpriteRenderer sprite;
    private bool saltar;
    private bool movimiento = true;
    private Transform laTransformacion;
    private AudioSource reproductorSonidosFX;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animacion = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        laTransformacion = GetComponent<Transform>();
        reproductorSonidosFX = GetComponent<AudioSource>();
        musicaFondo = camaraPrincipal.GetComponent<AudioSource>().clip;
        puntaje = 0;
    }

    // Update is called once per frame
    void Update()
    {
        animacion.SetFloat("velocidad", Mathf.Abs(rb2d.velocity.x));
        animacion.SetBool("tocando_suelo", tocandoSuelo);

        if ((Input.GetKeyDown(KeyCode.UpArrow) | Input.GetButtonDown("BotonA")) && tocandoSuelo)
        { //Saltar con análogo hacia arriba: Input.GetAxis("Vertical") > 0.002f
            saltar = true;
            reproducirFX(saltarClip);
        }
    }

    void FixedUpdate()
    {
        Vector3 velocidadFixeada = rb2d.velocity;
        velocidadFixeada.x *= 0.75f;

        if (tocandoSuelo)
        {
            rb2d.velocity = velocidadFixeada;
        }

        float horizontal = Input.GetAxis("Horizontal");
        if (!movimiento) horizontal = 0; //Para cancelar movimiento horizontal cuando enemigo lastima a personaje

        rb2d.AddForce(Vector2.right * velocidad * horizontal);

        float limiteVelocidad = Mathf.Clamp(rb2d.velocity.x, -maxVel, maxVel);

        if (Input.GetButton("BotonX") | Input.GetKey(KeyCode.Space)) //Aumentar velocidad mientras se presiona botón de correr
        {
            limiteVelocidad = Mathf.Clamp(rb2d.velocity.x, -maxVel*2.3f, maxVel*2.3f);
        }
        
        rb2d.velocity = new Vector2(limiteVelocidad, rb2d.velocity.y);

        /* Otra forma de hacer lo mismo de arriba
        if(rb2d.velocity.x > maxVel)
        {
            rb2d.velocity = new Vector2(maxVel, rb2d.velocity.y)
        }

        if(rb2d.velocity.x < -maxVel)
        {
            rb2d.velocity = new Vector2(-maxVel, rb2d.velocity.y)
        } 
        */

        if (horizontal > 0.1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (horizontal < -0.1f) //Invertir diección hacia la que mira el sprite cuando se camina hacia la izquierda
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (saltar)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            saltar = false;
        }

        //Debug.Log(rb2d.velocity.x);
    }

    void LateUpdate() //Se ejecuta después de Start, Update y FixedUpdate
    {
        laTransformacion.position = new Vector3(
            Mathf.Clamp(transform.position.x, rangoHorizontal.x, rangoHorizontal.y),
            Mathf.Clamp(transform.position.y, rangoVertical.x, rangoVertical.y),
            transform.position.z
        );
    }

    void OnBecameInvisible()
    {
        transform.position = new Vector3(-7.952f, -4.00f, -0.5f);
        disminuirPuntaje();
        disminuirPuntaje();

        if(camaraPrincipal != null) //Para que el IDE no llore :p
        {
            camaraPrincipal.GetComponent<AudioSource>().clip = morirClip;
            camaraPrincipal.GetComponent<AudioSource>().Play();
            Invoke("restaurarMusicaFondo", morirClip.length); //Llamar método que reproduce música de fondo original luego de terminar clip de muerte.
        }
        
    }

    public void saltoSobreEnemigo()
    {
        saltar = true;
        reproducirFX(eliminarEnemigoClip);
    }

    public void saltoRetrocesoDanioEnemigo(float posicionEnemigoX)
    {
        saltar = true;

        float lado = Mathf.Sign(posicionEnemigoX - transform.position.x); //Obtiene signo del resultado de la resta - Si enemigo se encuentra más a la izquierda será negativo
        rb2d.AddForce(Vector2.left * lado * fuerzaSalto, ForceMode2D.Impulse);

        movimiento = false;
        Invoke("habilitarMovimiento", 0.7f);

        Color color = new Color(255 / 255f, 150 / 255f, 135 / 255f, 255 / 255f); //Rojo, Verde, Azul, Alfa - Valores entre cero y uno.
        sprite.color = color;
        reproducirFX(recibirDanioClip);
    }

    void habilitarMovimiento()
    {
        movimiento = true;
        sprite.color = Color.white;
    }

    void aumentarPuntaje()
    {
        puntaje++;
        textoPuntaje.text = puntaje.ToString();
        reproducirFX(recogerMonedaClip);
        //Debug.Log(puntaje);
    }

    void disminuirPuntaje()
    {
        puntaje--;

        if(puntaje >= 0) {
            textoPuntaje.text = puntaje.ToString();
        }
        
    }

    void reproducirFX(AudioClip sonido)
    {
        reproductorSonidosFX.clip = sonido;
        reproductorSonidosFX.Play();
    }

    void restaurarMusicaFondo()
    {
        camaraPrincipal.GetComponent<AudioSource>().clip = musicaFondo;
        camaraPrincipal.GetComponent<AudioSource>().Play();
    }
}
