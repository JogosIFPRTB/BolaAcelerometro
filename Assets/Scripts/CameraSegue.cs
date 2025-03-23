using UnityEngine;

public class CameraSegue : MonoBehaviour
{
    [Header("Alvo a seguir")]
    public Transform target;

    [Header("Configuração de offset e suavização")]
    public Vector3 offset = new Vector3(0, 5, -10);
    public float smoothSpeed = 0.125f;

    [Header("Rotação opcional")]
    public bool followRotation = false;

    void LateUpdate()
    {
        if (target == null) return;

        // Calcula a posição desejada
        Vector3 desiredPosition = target.position + offset;

        // Interpola suavemente a posição
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Aplica a posição suavizada
        transform.position = smoothedPosition;

        // Opcional: rotaciona a câmera para olhar o personagem
        if (followRotation)
        {
            transform.LookAt(target);
        }
    }
}
