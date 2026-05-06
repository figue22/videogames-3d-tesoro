using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // SEGURIDAD: Esto hace que el mouse aparezca sí o sí
    void Awake() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void IniciarJuego() {
        // Opción A: Usa el nombre exacto de tu escena principal (ej: "SampleScene")
        // SceneManager.LoadScene("SampleScene");

        // Opción B: Carga la siguiente escena en la lista de Build Profiles
        SceneManager.LoadScene(1); 
    }


    void Start(){
        // Fuerza al sistema a liberar el mouse para que la UI responda bien
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}