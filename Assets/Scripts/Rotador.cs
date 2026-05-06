using UnityEngine;

public class Rotador : MonoBehaviour
{
    public Vector3 velocidadRotacion = new Vector3(0, 100, 0);

    void Update()
    {
        // Rota el objeto sobre su propio eje cada frame
        transform.Rotate(velocidadRotacion * Time.deltaTime);
    }
}