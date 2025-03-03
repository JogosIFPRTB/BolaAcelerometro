using UnityEngine;

/// <summary>
/// Controla o movimento de uma bola usando torque (rota��o) com suporte a m�ltiplas plataformas.
/// No Unity Editor, usa as teclas de input configuradas (Horizontal e Vertical).
/// No Android, usa o aceler�metro para controle.
/// Tamb�m instancia e destr�i objetos ao pressionar um bot�o de a��o.
/// </summary>
[RequireComponent(typeof(Rigidbody))] // Garante que o componente Rigidbody esteja presente
public class MovimentoBola : MonoBehaviour
{
    /// <summary> Refer�ncia ao Rigidbody anexado ao GameObject. </summary>
    private Rigidbody rb;

    /// <summary> Velocidade de rota��o aplicada � bola. </summary>
    [Tooltip("Define a velocidade da rota��o aplicada � bola.")]
    [SerializeField] private float velocidade = 10f;

    /// <summary> Prefab ou objeto a ser instanciado ao pressionar o bot�o de a��o. </summary>
    [Tooltip("Objeto que ser� instanciado ao pressionar o bot�o de a��o.")]
    [SerializeField] private GameObject objetoPrefab;

    /// <summary> Inicializa componentes necess�rios ao iniciar a cena. </summary>
    private void Awake()
    {
        // Captura o Rigidbody anexado ao GameObject
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// FixedUpdate � chamado em intervalos fixos de tempo, ideal para f�sica.
    /// Aplica torque � bola com base no tipo de controle (Editor ou Android).
    /// </summary>
    private void FixedUpdate()
    {
        AplicarTorque(CapturarEntrada());
    }

    /// <summary>
    /// Captura a entrada do usu�rio para movimenta��o, considerando diferentes plataformas.
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
        // Captura input do aceler�metro no Android (melhorado para considerar a orienta��o do dispositivo)
        direcaoTorque = new Vector3(
            Mathf.Clamp(Input.acceleration.y * velocidade, -1, 1), // Limita a entrada para evitar valores extremos
            0,
            Mathf.Clamp(Input.acceleration.x * -velocidade, -1, 1)
        );
#endif

        return direcaoTorque;
    }

    /// <summary>
    /// Aplica torque ao Rigidbody com base no vetor de dire��o fornecido.
    /// </summary>
    /// <param name="direcao">Vetor que define a dire��o e intensidade do torque.</param>
    private void AplicarTorque(Vector3 direcao)
    {
        if (direcao != Vector3.zero)
        {
            rb.AddTorque(direcao, ForceMode.Force);
        }
    }

    /// <summary>
    /// Update � chamado a cada frame.
    /// Instancia um objeto ao pressionar o bot�o de a��o e destr�i o clone ap�s 1 segundo.
    /// </summary>
    private void Update()
    {
        if (Input.GetButton("Fire1")) // Garante que a a��o ocorre apenas uma vez por clique/toque
        {
            CriarEProgramarDestruicaoObjeto();
        }
    }

    /// <summary>
    /// Instancia um objeto na posi��o da bola e o destr�i ap�s 1 segundo.
    /// </summary>
    private void CriarEProgramarDestruicaoObjeto()
    {
        if (objetoPrefab == null)
        {
            Debug.LogWarning("Nenhum objeto foi atribu�do ao script!", this);
            return;
        }

        // Instancia o objeto como filho da bola
        GameObject clone = Instantiate(objetoPrefab, transform.position, Quaternion.identity);
        clone.transform.SetParent(transform, true);

        // Destroi o clone ap�s 1 segundo
        Destroy(clone, 1f);
    }
}
