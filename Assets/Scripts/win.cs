using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cargar escenas

public class win : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Verifica si la colisión fue con el jugador
        if (collision.gameObject.CompareTag("main_character"))
        {
            // Carga la escena actual para reiniciar el juego
            Application.Quit();
        }
    }
}
