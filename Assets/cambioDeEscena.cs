using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cambioDeEscena : MonoBehaviour
{
    public static string nombreNivel;
    public Mario_controlar scriptMario; //Asignado desde GUI el script de gestión de puntaje

    // Start is called before the first frame update
    void Start()
    {
        nombreNivel = SceneManager.GetActiveScene().name; ;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(nombreNivel);

        if (!nombreNivel.Equals(null))
        {
            if (nombreNivel.Equals("PantallaInicio") && (Input.GetKeyDown(KeyCode.UpArrow) | Input.GetButtonDown("BotonA")))
            {
                SceneManager.LoadScene("NivelUno");
            }
            else if ((nombreNivel.Equals("GameOver") | nombreNivel.Equals("Creditos")) && (Input.GetKeyDown(KeyCode.UpArrow) | Input.GetButtonDown("BotonA")))
            {
                SceneManager.LoadScene("PantallaInicio");
            }

            if (!nombreNivel.Equals("PantallaInicio") && !nombreNivel.Equals("GameOver") && !nombreNivel.Equals("Creditos") && scriptMario.puntaje < 0) //Si se queda sin monedas y tocan al personaje, muere.
            {
                SceneManager.LoadScene("GameOver");
            }
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit(); //Cerrar el juego
        }
    }

    public void siguienteNivel()
    {
        switch (nombreNivel){
            case "NivelUno": SceneManager.LoadScene("Creditos"); break;
        }
        
    }
}
