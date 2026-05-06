using UnityEngine;
using UnityEngine.AI;

public class NPC_Patrol : MonoBehaviour
{
    public float radioDeCaminata = 8f; 
    public float tiempoDeEspera = 4f; 
    
    private NavMeshAgent agente;
    private Animator anim; // Añadimos referencia al Animator
    private float cronometro;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>(); // Obtenemos el componente
        cronometro = tiempoDeEspera;
    }

  void Update()
    {
        cronometro += Time.deltaTime;

        if (cronometro >= tiempoDeEspera)
        {
            Vector3 destinoNuevo = PuntoAleatorio(transform.position, radioDeCaminata);
            agente.SetDestination(destinoNuevo);
            cronometro = 0;
        }

        if (anim != null)
        {
            float velocidad = agente.velocity.magnitude;
            
            // 1. Decimos la velocidad (Caminar/Correr)
            anim.SetFloat("Speed", velocidad);
            
            // 2. IMPORTANTE: MotionSpeed debe ser 1 para que la animación se mueva
            anim.SetFloat("MotionSpeed", 1f);
            
            // 3. IMPORTANTE: Grounded debe ser true o se quedará en pose de caída
            anim.SetBool("Grounded", true);
        }
    }

    public Vector3 PuntoAleatorio(Vector3 centro, float distancia)
    {
        Vector3 dirAleatoria = Random.insideUnitSphere * distancia;
        dirAleatoria += centro;
        NavMeshHit hit;
        NavMesh.SamplePosition(dirAleatoria, out hit, distancia, 1);
        return hit.position;
    }
}