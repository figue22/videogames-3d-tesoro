using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets; // Necesario para acceder al movimiento de Unity
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    private Animator anim;
    private ThirdPersonController moveScript; // Referencia al script de movimiento
    private AudioSource audioJugador; // Componente para reproducir el audio local

    [Header("Configuración de Animación")]
    public float tiempoDeParo = 0.4f; // Tiempo que se queda quieto atacando

    [Header("Componentes de Combate")]
    public Collider colliderEspada; // Box Collider de tu espada

    [Header("Efectos de Sonido (SFX)")]
    public AudioClip sonidoEspadazo; // Arrastra aquí el audio del ataque (.mp3/.wav)

    void Start()
    {
        anim = GetComponent<Animator>();
        moveScript = GetComponent<ThirdPersonController>();

        // Inicializamos u obtenemos el componente de audio en el jugador
        audioJugador = GetComponent<AudioSource>();

        // Aseguramos que la espada inicie APAGADA al empezar el juego
        if (colliderEspada != null)
        {
            colliderEspada.enabled = false;
        }
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Atacar();
        }
    }

    void Atacar()
    {
        if (anim != null)
        {
            anim.SetTrigger("Attack");
            
            // --- FEEDBACK AUDITIVO DEL ATAQUE ---
            if (audioJugador != null && sonidoEspadazo != null)
            {
                // Modificamos el pitch sutilmente para que cada espadazo suene orgánico y único
                audioJugador.pitch = Random.Range(0.9f, 1.1f);
                audioJugador.PlayOneShot(sonidoEspadazo);
            }

            // Detenemos el movimiento y encendemos la física de la espada
            StartCoroutine(DetenerMovimiento());
        }
    }

    IEnumerator DetenerMovimiento()
    {
        if (moveScript != null)
        {
            moveScript.enabled = false; // Frena al jugador
            
            // ENCIENDE LA ESPADA
            if (colliderEspada != null) colliderEspada.enabled = true; 
            
            yield return new WaitForSeconds(tiempoDeParo);
            
            // APAGA LA ESPADA
            if (colliderEspada != null) colliderEspada.enabled = false; 

            moveScript.enabled = true; // Libera al jugador
        }
    }
}