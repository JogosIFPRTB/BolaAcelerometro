using UnityEngine;

/// <summary>
/// Controla o movimento de uma bola usando torque (rotação) com suporte a múltiplas plataformas.
/// No Unity Editor, usa as teclas de input configuradas (Horizontal e Vertical).
/// No Android, usa o acelerômetro para controle.
/// Também instancia e destrói objetos ao pressionar um botão de ação.
/// </summary>
[RequireComponent(typeof(Rigidbody))] // Garante que o componente Rigidbody esteja presente
public class MovimentoBola : MonoBehaviour
{
    /// <summary> Referência ao Rigidbody anexado ao GameObject. </summary>
    private Rigidbody rb;

    /// <summary> Velocidade de rotação aplicada à bola. </summary>
    [Tooltip("Define a velocidade da rotação aplicada à bola.")]
    [SerializeField] private float velocidade = 10f;

    /// <summary> Prefab ou objeto a ser instanciado ao pressionar o botão de ação. </summary>
    [Tooltip("Objeto que será instanciado ao pressionar o botão de ação.")]
    [SerializeField] private GameObject objetoPrefab;

    /// <summary> Inicializa componentes necessários ao iniciar a cena. </summary>
    private void Awake()
    {
        // Captura o Rigidbody anexado ao GameObject
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// FixedUpdate é chamado em intervalos fixos de tempo, ideal para física.
    /// Aplica torque à bola com base no tipo de controle (Editor ou Android).
    /// </summary>
    private void FixedUpdate()
    {
        AplicarTorque(CapturarEntrada());
    }

    /// <summary>
    /// Captura a entrada do usuário para movimentação, considerando diferentes plataformas.
    /// </summary>
    /// <returns>Vetor de torque a ser aplicado.</returns>
    private Vector3 CapturarEntrada()
    {
        Vector3 direcaoTorque = Vector3.zero;

#if UNITY_EDITOR || UNITY_STANDALONE
        // Captura input das teclas configuradas (WASD ou setas) no Editor e Standalone
        direcaoTorque = new Vector3(
            Input.GetAxis("Vertical") * velocidade, // Controle no eixo Z
            0,
            Input.GetAxis("Horizontal") * -velocidade // Controle no eixo X
        );
#elif UNITY_ANDROID
        // Captura input do acelerômetro no Android (melhorado para considerar a orientação do dispositivo)
        direcaoTorque = new Vector3(
            Mathf.Clamp(Input.acceleration.y * velocidade, -1, 1), // Limita a entrada para evitar valores extremos
            0,
            Mathf.Clamp(Input.acceleration.x * -velocidade, -1, 1)
        );
#endif

        return direcaoTorque;
    }

    /// <summary>
    /// Aplica torque ao Rigidbody com base no vetor de direção fornecido.
    /// </summary>
    /// <param name="direcao">Vetor que define a direção e intensidade do torque.</param>
    private void AplicarTorque(Vector3 direcao)
    {
        if (direcao != Vector3.zero)
        {
            rb.AddTorque(direcao, ForceMode.Force);
        }
    }

    /// <summary>
    /// Update é chamado a cada frame.
    /// Instancia um objeto ao pressionar o botão de ação e destrói o clone após 1 segundo.
    /// </summary>
    private void Update()
    {
        if (Input.GetButton("Fire1")) // Garante que a ação ocorre apenas uma vez por clique/toque
        {
            CriarEProgramarDestruicaoObjeto();
        }
    }

    /// <summary>
    /// Instancia um objeto na posição da bola e o destrói após 1 segundo.
    /// </summary>
    private void CriarEProgramarDestruicaoObjeto()
    {
        if (objetoPrefab == null)
        {
            Debug.LogWarning("Nenhum objeto foi atribuído ao script!", this);
            return;
        }

        // Instancia o objeto como filho da bola
        GameObject clone = Instantiate(objetoPrefab, transform.position, Quaternion.identity);
        clone.transform.SetParent(transform, true);

        // Destroi o clone após 1 segundo
        Destroy(clone, 1f);
    }
}
