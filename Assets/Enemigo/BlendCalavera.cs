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
    // Start is called before the first frame update
    void Start()
    {   
        this.puntoOrigen=this.transform.position;
        Agente=this.GetComponent<NavMeshAgent> ();  
        this.GetComponent<Animator>().SetFloat("Acciones",0.01f);
        this.sangre=1.0f;
        bandera=true;
        this.GetComponent<Animator>().ResetTrigger("muerte");
    }

    // Update is called once per frame
   void Update()
{
    UnityEngine.AI.NavMeshHit hit;
    blocked = NavMesh.Raycast(this.transform.position, Jugador.position, out hit, UnityEngine.AI.NavMesh.AllAreas);
    
    // CORRECCIÓN: Primero evaluamos la distancia del Raycast
    // Si no está bloqueado Y además estás en su rango de persecución
    if (!blocked && hit.distance > 4f && hit.distance < 10f)
    {
        this.GetComponent<Animator>().SetFloat("Acciones", 0.4f);
        this.Agente.SetDestination(Jugador.position);
    }
    // Si estás muy cerca, ataca
    else if (!blocked && hit.distance <= 4f)
    {
        this.GetComponent<Animator>().SetTrigger("atacar");
        this.Agente.SetDestination(this.transform.position);
    }
    // Si está bloqueado (pared) O te alejaste a más de 10 metros, regresa a casa
    else 
    {
        float dist = Vector3.Distance(transform.position, this.puntoOrigen);
        if (dist < 2.0f) // Llegó al punto de origen
        {
            this.Agente.SetDestination(this.transform.position);
            this.GetComponent<Animator>().SetFloat("Acciones", 0.01f); // IDLE
        }
        else // Regresando al punto de origen
        {
            this.Agente.SetDestination(this.puntoOrigen);
            this.GetComponent<Animator>().SetFloat("Acciones", 0.2f); // CAMINAR
        }
    }

    if (!bandera)
        this.Agente.SetDestination(this.transform.position);
}
    public void setGolpe(int animacion)//recibir el golpe
    {
     if(animacion==1)
     {
      this.GetComponent<Animator>().SetTrigger("golpe");
      if(this.sangre>0f)
      this.sangre=this.sangre-0.25f;
      
      if(this.sangre<=0f)
      {
      print("sangre:  "+this.sangre);
       if(bandera)
       {
        this.GetComponent<Animator>().SetInteger("final",1);
        this.GetComponent<Animator>().SetTrigger("muerte");
        bandera=false;
       } 
      // this.GetComponent<BlendCalavera>().enabled=!this.GetComponent<BlendCalavera>().enabled;
      Destroy(this.gameObject,8f);
      
      }
     }
     if(animacion==2)
     {
         
       this.GetComponent<Animator>().ResetTrigger("golpe");
     }

    }
}
