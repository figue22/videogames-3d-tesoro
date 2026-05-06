using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System.Collections;

public class NPC_Interaction : MonoBehaviour
{
    [Header("Configuración de Texto")]
    public string mensajeCompleto;
    public TextMeshProUGUI keysText;
    public float velocidadEscritura = 0.05f;

    [Header("Configuración de Sonido")]
    private AudioSource audioSource;

    private NavMeshAgent agente;
    private Transform jugador;
    private bool estaCerca = false;
    private Coroutine corrutinaEscritura;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            estaCerca = true;
            jugador = other.transform;
            if (agente != null) agente.isStopped = true;

            if (keysText != null)
            {
                if (corrutinaEscritura != null) StopCoroutine(corrutinaEscritura);
                corrutinaEscritura = StartCoroutine(EscribirMensaje());
            }
        }
    }

        IEnumerator EscribirMensaje()
    {
        keysText.text = ""; 
        
        foreach (char letra in mensajeCompleto.ToCharArray())
        {
            keysText.text += letra;

            if (audioSource != null && letra != ' ')
            {
                // 1. Variedad de tono para que suene natural
                audioSource.pitch = Random.Range(0.85f, 1.15f); 
                
                // 2. Usamos PlayOneShot para asegurar que se escuche la muestra completa
                // pero le pasamos el clip del propio componente
                audioSource.PlayOneShot(audioSource.clip);
            }

            yield return new WaitForSeconds(velocidadEscritura);
        }

        // 3. Al terminar el bucle, esperamos un breve instante y limpiamos el buffer
        yield return new WaitForSeconds(0.1f);
        if (audioSource != null) audioSource.Stop();
        audioSource.pitch = 1f;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            estaCerca = false;
            if (agente != null) agente.isStopped = false;
            
            // --- SOLUCIÓN: Detener todo si el jugador se va ---
            if (corrutinaEscritura != null) StopCoroutine(corrutinaEscritura);
            if (audioSource != null) audioSource.Stop(); 
            
            if (keysText != null) keysText.text = ""; 
        }
    }

    void Update()
{
    // Si el jugador está cerca, giramos el NPC para que lo mire
    if (estaCerca && jugador != null)
    {
        // Calculamos la dirección pero ignoramos la altura (Y) para que el NPC no se incline
        Vector3 direccionLook = new Vector3(jugador.position.x, transform.position.y, jugador.position.z);
        
        // Usamos una rotación suave (Lerp) para que no sea un giro brusco
        Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionLook - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * 5f);
    }
}
}