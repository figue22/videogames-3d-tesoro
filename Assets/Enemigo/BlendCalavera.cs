using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlendCalavera : MonoBehaviour
{
    float sangre;
    NavMeshAgent Agente;
    public Transform Jugador;
    Vector3 puntoOrigen;
    bool blocked;
    bool bandera;

    [Header("Configuración de Velocidades")]
    public float velocidadPersecucion = 4.5f;
    public float velocidadRetorno = 2.0f;

    [Header("Sonidos de Impacto")]
    public AudioClip sonidoDolorEnemigo; 
    private AudioSource audioEnemigo;

    // Start is called before the first frame update
    void Start()
    {   
        this.puntoOrigen = this.transform.position;
        Agente = this.GetComponent<NavMeshAgent>();  
        this.GetComponent<Animator>().SetFloat("Acciones", 0.01f);
        this.sangre = 1.0f;
        bandera = true;
        this.GetComponent<Animator>().ResetTrigger("muerte");

        // Inicializamos el componente de audio local
        audioEnemigo = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // COMPUERTA LÓGICA: Si el enemigo ya murió o el agente se desactivó, 
        // abortamos la ejecución de este frame inmediatamente para evitar errores en la consola.
        if (!bandera || Agente == null || !Agente.enabled) 
        {
            return; 
        }

        UnityEngine.AI.NavMeshHit hit;
        blocked = NavMesh.Raycast(this.transform.position, Jugador.position, out hit, UnityEngine.AI.NavMesh.AllAreas);
        
        // 1. RANGO DE PERSECUCIÓN
        if (!blocked && hit.distance > 4f && hit.distance < 10f)
        {
            this.GetComponent<Animator>().SetFloat("Acciones", 0.4f);
            this.Agente.speed = velocidadPersecucion; 
            this.Agente.SetDestination(Jugador.position);
        }
        // 2. RANGO DE ATAQUE
        else if (!blocked && hit.distance <= 4f)
        {
            this.GetComponent<Animator>().SetTrigger("atacar");
            this.Agente.speed = 0f; 
            this.Agente.SetDestination(this.transform.position);
        }
        // 3. RETORNO O PÉRDIDA DE VISTA
        else 
        {
            float dist = Vector3.Distance(transform.position, this.puntoOrigen);
            
            if (dist < 2.0f) // Llegó a su casa -> IDLE
            {
                this.Agente.SetDestination(this.transform.position);
                this.GetComponent<Animator>().SetFloat("Acciones", 0.01f); 
                this.Agente.speed = 0f;
            }
            else // Regresando a su casa -> Caminar lento coordinado
            {
                this.Agente.SetDestination(this.puntoOrigen);
                this.GetComponent<Animator>().SetFloat("Acciones", 0.2f); 
                this.Agente.speed = velocidadRetorno; 
            }
        }
    }

    //  DETECCIÓN POR NOMBRE DEL GAMEOBJECT ---
   // --- DETECCIÓN POR NOMBRE DEL GAMEOBJECT ---
    // --- DETECCIÓN POR NOMBRE DEL GAMEOBJECT ---
   // --- DETECCIÓN POR NOMBRE DEL GAMEOBJECT ---
 // --- DETECCIÓN POR NOMBRE DEL GAMEOBJECT ---
    private void OnTriggerEnter(Collider other)
    {
        // Verificar el nombre del gameObject para determinar si es la espada del jugador
        if (other.gameObject.name.ToLower().Contains("espada") || other.gameObject.name.ToLower().Contains("sword"))
        {
            this.setGolpe(1);
        }
    }

    public void setGolpe(int animacion) 
    {
        if (animacion == 1)
        {
            this.GetComponent<Animator>().SetTrigger("golpe");

            // KNOCKBACK (EMPUJE POR IMPACTO)
            if (Agente != null && Agente.enabled && Jugador != null)
            {
                Vector3 direccionEmpuje = (transform.position - Jugador.position).normalized;
                direccionEmpuje.y = 0; 

                float distanciaRetroceso = 1.5f;
                Vector3 posicionDestino = transform.position + (direccionEmpuje * distanciaRetroceso);

                this.Agente.speed = 8.0f; 
                this.Agente.SetDestination(posicionDestino);
            }

            // FEEDBACK AUDITIVO
            if (audioEnemigo != null && sonidoDolorEnemigo != null)
            {
                audioEnemigo.pitch = Random.Range(0.85f, 1.15f);
                audioEnemigo.PlayOneShot(sonidoDolorEnemigo);
            }

            if (this.sangre > 0f)
            {
                this.sangre = this.sangre - 0.25f;
            }
          
            if (this.sangre <= 0f)
            {
                print("sangre:  " + this.sangre);
                if (bandera)
                {
                    this.GetComponent<Animator>().SetInteger("final", 1);
                    this.GetComponent<Animator>().SetTrigger("muerte");
                    bandera = false;
                } 
                
                if (Agente != null) 
                {
                    Agente.enabled = false;
                }
                
                Destroy(this.gameObject, 8f);
            }
        }
        
        if (animacion == 2)
        {
            this.GetComponent<Animator>().ResetTrigger("golpe");
        }
    }
}