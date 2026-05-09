using UnityEngine;
using UnityEngine.UI; // Necesario para controlar la imagen

public class VidaJugador : MonoBehaviour {
    public float vidaMaxima = 100f;
    private float vidaActual;

    [Header("Interfaz")]
    public Image imagenBarraVida; // Arrastra aquí la 'BarraRoja'

    void Start() {
        vidaActual = vidaMaxima;
        ActualizarInterfaz();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Trampa")) {
            RecibirDaño(15f);
            Destroy(other.gameObject);
        }
    }

    public void RecibirDaño(float cantidad) {
        vidaActual -= cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima); // Evita que baje de 0
        
        ActualizarInterfaz();

        if (vidaActual <= 0) {
            Debug.Log("El jugador ha sido derrotado");
            // Aquí puedes llamar a tu menú de Game Over
        }
    }

    void ActualizarInterfaz() {
        if (imagenBarraVida != null) {
            // El Fill Amount va de 0 a 1, por eso dividimos
            imagenBarraVida.fillAmount = vidaActual / vidaMaxima;
        }
    }
}