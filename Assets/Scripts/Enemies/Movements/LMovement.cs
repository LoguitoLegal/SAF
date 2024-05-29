using UnityEngine;

public class LMovement : MonoBehaviour
{
    private Vector3 startLocation;
    private Renderer render;

    [SerializeField] private float curveDistance;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    private bool isTurning = false;
    private Quaternion targetRotation;

    void Start()
    {
        render = GetComponent<Renderer>();
        startLocation = transform.position;
        targetRotation = Quaternion.Euler(0f, 90f, 0f);
    }

    void Update()
    {
        if (GetComponent<Renderer>().isVisible)
        {

            if (transform.position.x >= 1.278f || transform.position.x <= -1.278f)
            {
                Destroy(gameObject);
            }
            // Movimento linear inicial
            if (!isTurning)
            {
                transform.position += transform.forward * speed * Time.deltaTime;

                // Verifica se chegou ao ponto de curva
                if (transform.position.z <= startLocation.z - curveDistance)
                {
                    isTurning = true;
                }
            }
            else
            {
                // Interpola��o suave da rota��o
                float t = Mathf.Clamp01(rotationSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, t);

                // Movimento na dire��o da rota��o
                Vector3 moveDirection = transform.forward;
                transform.position += moveDirection * speed * Time.deltaTime;
            }
        }
    }
}
