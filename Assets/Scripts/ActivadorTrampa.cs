using UnityEngine;

public class ActivadorTrampa : MonoBehaviour
{
    // Cambia 'GestorTrampas' por el nombre EXACTO de tu script de disparo
    public TrampaFlechas scriptTrampa; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (scriptTrampa != null)
            {
                scriptTrampa.IniciarTrampa(); // Asegúrate de que el método se llame igual
            }
            
            // Opcional: Desactivar el trigger para que no se repita infinitamente
            // gameObject.SetActive(false); 
        }
    }
}