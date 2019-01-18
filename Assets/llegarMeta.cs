using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class llegarMeta : MonoBehaviour
{
    public AudioClip musicaMetaClip;
    public Camera camaraPrincipal;
    private GameObject limiteMeta; 

    private AudioSource musicaFondo;

    // Start is called before the first frame update
    void Start()
    {
        musicaFondo = camaraPrincipal.GetComponent<AudioSource>();
        limiteMeta = GameObject.Find("goal-roulette super mario bros 3_5");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.gameObject.tag == "Player")
        {
            musicaFondo.clip = musicaMetaClip;
            musicaFondo.Play();
            musicaFondo.loop = false; //Para que no se repita fanfarria.

            limiteMeta.GetComponent<EdgeCollider2D>().enabled = true; //Para evitar que después de ganar vuelva y pierda.
            colision.GetComponent<cambioDeEscena>().Invoke("siguienteNivel", musicaMetaClip.length); //Llamar método que llama a la siguiente escena luego de terminar clip de ganar.
            Destroy(gameObject);
        }
    }
}
