using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // 1. Importante añadir esta librería

public class ControladorPausa : MonoBehaviour
{
    public GameObject objetoMenuPausa;
    private bool estaPausado = false;

    void Update()
    {
        // 2. Nueva forma de detectar la tecla Escape
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (estaPausado)
                Reanudar();
            else
                Pausar();
        }
    }

    public void Pausar()
    {
        estaPausado = true;
        objetoMenuPausa.SetActive(true);
        Time.timeScale = 0f; 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Reanudar()
    {
        estaPausado = false;
        objetoMenuPausa.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void IrAlMenuPrincipal()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("menu"); 
    }
}