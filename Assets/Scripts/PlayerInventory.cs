using UnityEngine;
using TMPro; // Asegúrate de tener instalado TextMeshPro

public class PlayerInventory : MonoBehaviour
{
    public int keysCount = 0;
    public AudioSource pickupSound; // Sonido para llaves y moneda
    public TextMeshProUGUI keysText; // Tu texto con la fuente de DaFont

    void Start()
    {
        ActualizarUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Lógica para las Llaves
        if (other.CompareTag("Key"))
        {
            keysCount++;
            ActualizarUI();
            if (pickupSound != null) pickupSound.Play();
            Destroy(other.gameObject);
        }
        // Lógica para la Moneda Pirata
        else if (other.CompareTag("Coin"))
        {
            Debug.Log("¡Moneda Pirata obtenida!");
            if (pickupSound != null) pickupSound.Play();
            
            // Mensaje especial de victoria
            if (keysText != null) keysText.text = "¡TESORO OBTENIDO!";
            
            Destroy(other.gameObject);
        }
    }

    public void ActualizarUI()
    {
        if (keysText != null)
        {
            keysText.text = "Llaves: " + keysCount + " / 3";
        }
    }
}