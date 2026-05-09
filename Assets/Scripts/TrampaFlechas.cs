using UnityEngine;
using System.Collections;

public class TrampaFlechas : MonoBehaviour {
    public GameObject prefabFlecha;
    public float fuerzaDisparo = 20f;
    public float intervalo = 0.5f;

    public void IniciarTrampa() {
        StartCoroutine(DispararRafaga());
    }

    IEnumerator DispararRafaga() {
        for (int i = 0; i < 5; i++) { // Dispara 5 flechas
            GameObject flecha = Instantiate(prefabFlecha, transform.position, transform.rotation);
            Rigidbody rb = flecha.GetComponent<Rigidbody>();
            rb.linearVelocity = transform.forward * fuerzaDisparo;
            
            Destroy(flecha, 3f); // Limpieza de memoria
            yield return new WaitForSeconds(intervalo);
        }
    }
}