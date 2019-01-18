using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contarMoneda : MonoBehaviour
{
    //Uso este script porque si hacía el contéo en el personaje, cuando colisionaba el collider del personaje con el de la moneda y el piso a la vez, sumaba 2 puntos.
    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.gameObject.tag == "Player")
        {
            colision.SendMessage("aumentarPuntaje");
            Destroy(gameObject);
        }
    }
}
