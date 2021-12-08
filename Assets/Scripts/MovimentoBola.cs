using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovimentoBola : MonoBehaviour
{
    Rigidbody rb;
    public float velocidade = 10;
    public GameObject objeto;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        #if UNITY_EDITOR
            AplicarTorque
                (new Vector3(
                    Input.GetAxis("Vertical") * velocidade,
                    0,
                    Input.GetAxis("Horizontal") * -velocidade
                    )
                );
        #endif
        #if UNITY_ANDROID
            AplicarTorque
                (new Vector3(
                    Input.acceleration.y * velocidade,
                    0,
                    Input.acceleration.x * -velocidade
                    )
                );
        #endif
    }

    void AplicarTorque(Vector3 direcao) {
        rb.AddTorque(direcao);
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
            Instantiate(objeto, transform);
    }
}
