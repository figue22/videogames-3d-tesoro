using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets; // Necesario para acceder al movimiento de Unity

public class PlayerCombat : MonoBehaviour
{
    private Animator anim;
    private ThirdPersonController moveScript; // Referencia al script de movimiento
    
    // Tiempo que el personaje se quedará quieto (ajusta según tu animación)
    public float tiempoDeParo = 0.8f; 

    void Start()
    {
        anim = GetComponent<Animator>();
        moveScript = GetComponent<ThirdPersonController>();
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
            
            // Detenemos el movimiento
            StartCoroutine(DetenerMovimiento());
        }
    }

    // Corrutina para pausar y reanudar el movimiento
    System.Collections.IEnumerator DetenerMovimiento()
    {
        if (moveScript != null)
        {
            // Bloqueamos el movimiento (esta variable existe en Starter Assets)
            moveScript.enabled = false; 
            
            yield return new WaitForSeconds(tiempoDeParo);
            
            // Reanudamos el movimiento
            moveScript.enabled = true;
        }
    }
}