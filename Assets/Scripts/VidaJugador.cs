using UnityEngine;
using UnityEngine.UI;

public class VidaJugador : MonoBehaviour {
    public float vidaMaxima = 100f;
    private float vidaActual;

    [Header("Interfaz")]
    public Image imagenBarraVida;

    [Header("Sonidos de Daño")]
    private AudioSource audioSource;       // El componente del jugador
    public AudioClip sonidoQueja;          // El archivo de audio del gemido/queja

    void Start() {
        vidaActual = vidaMaxima;
        
        // Obtenemos el componente AudioSource del propio personaje
        audioSource = GetComponent<AudioSource>();
        
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
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
        
        ActualizarInterfaz();

        // --- NUEVA LÓGICA DE AUDIO ---
        if (audioSource != null && sonidoQueja != null) {
            // Variamos ligeramente el pitch para que no suene robótico si le pegan seguido
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            
            // Usamos PlayOneShot para que el sonido de dolor no interrumpa abruptamente otros sonidos del personaje
            audioSource.PlayOneShot(sonidoQueja);
        }

        if (vidaActual <= 0) {
            Debug.Log("Jhonatanga ha sido derrotado");
        }
    }

    void ActualizarInterfaz() {
        if (imagenBarraVida != null) {
            imagenBarraVida.fillAmount = vidaActual / vidaMaxima;
        }
    }
}