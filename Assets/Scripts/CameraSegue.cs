using UnityEngine;

public class CameraSegue : MonoBehaviour
{
    [Header("Alvo a seguir")]
    public Transform target;

    [Header("Configura��o de offset e suaviza��o")]
    public Vector3 offset = new Vector3(0, 5, -10);
    public float smoothSpeed = 0.125f;

    [Header("Rota��o opcional")]
    public bool followRotation = false;

    void LateUpdate()
    {
        if (target == null) return;

        // Calcula a posi��o desejada
        Vector3 desiredPosition = target.position + offset;

        // Interpola suavemente a posi��o
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Aplica a posi��o suavizada
        transform.position = smoothedPosition;

        // Opcional: rotaciona a c�mera para olhar o personagem
        if (followRotation)
        {
            transform.LookAt(target);
        }
    }
}
