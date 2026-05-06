using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    [Header("Configuración Visual")]
    public GameObject cofreCerrado;
    public GameObject grupoCofreAbierto;

    [Header("Recompensa")]
    public GameObject monedaPrefab; // Tu prefab de Pirate Coin
    public Transform puntoSalidaMoneda; // Objeto vacío (Empty) arriba del cofre

    [Header("Lógica")]
    public int llavesNecesarias = 3;
    private bool yaSeAbrio = false;

    private void OnTriggerEnter(Collider other)
    {
        // Si ya está abierto, no hacemos nada
        if (yaSeAbrio) return;

        // Buscamos el inventario en el objeto que entró al trigger
        PlayerInventory inventario = other.GetComponent<PlayerInventory>();

        if (inventario != null)
        {
            if (inventario.keysCount >= llavesNecesarias)
            {
                AbrirCofre();
            }
            else
            {
                Debug.Log("Te faltan llaves. Tienes: " + inventario.keysCount);
                // Opcional: Podrías poner un mensaje en pantalla aquí también
            }
        }
    }

    void AbrirCofre()
    {
        yaSeAbrio = true;
        cofreCerrado.SetActive(false);
        grupoCofreAbierto.SetActive(true);

        // Crear la moneda
        if (monedaPrefab != null && puntoSalidaMoneda != null)
        {
            GameObject nuevaMoneda = Instantiate(monedaPrefab, puntoSalidaMoneda.position, puntoSalidaMoneda.rotation);
            
            // Si la moneda tiene Rigidbody, le damos un pequeño impulso hacia arriba
            Rigidbody rb = nuevaMoneda.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
            }
        }

        Debug.Log("¡Cofre abierto!");
    }
}