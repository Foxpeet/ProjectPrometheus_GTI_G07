using UnityEngine;

public class SphereController : MonoBehaviour
{
    public Camera mainCamera;
    public float speed = 155f; 
    public LayerMask wallLayer;
    public Transform Inicio;
    public Transform finLab;

    private Vector3 destino;
    private Vector3 posicionInicial;
    private float stopDistance = 0.05f; // Distancia a la que la esfera se para antes de llegar a la pared
    private bool estaEnMovimiento = false;
    private bool estaAlFinal = false;

    MinijuegoLaberinto minijuegoLaberinto;

    private void Start()

    {   //Guardamos la posicion inicial
        mainCamera = Camera.main;
        posicionInicial = transform.position;
        destino = transform.position;
        minijuegoLaberinto = transform.root.gameObject.GetComponent<MinijuegoLaberinto>();
    }

    private void Update()
    {   
           //Cuando se haca click con el raton
        if (Input.GetMouseButton(0))
        {
            //Se crea un rayo desde la cámara al clic del raton
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //Guardammos la posicion del clic en la pantalla, ajustamos la altura para que coincida con la de la esfera y calcula la dirección y la distancia a ese punto.
                Vector3 targetPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                Vector3 direction = (targetPosition - transform.position).normalized;
                float distance = Vector3.Distance(targetPosition, transform.position);

                
                // Si choca ajustamos la distancia para que no atraviese la pared
                if (Physics.SphereCast(transform.position, 0.01f, direction, out RaycastHit hitInfo, distance, wallLayer))
                {   
                    
                    distance = hitInfo.distance - stopDistance;
                }

                destino = transform.position + direction * distance;
            }

            // Movemos la esfera a la posicion
            transform.position = Vector3.MoveTowards(transform.position, destino, speed * Time.deltaTime);
        }

        // Si se suelta el click del raton, comprobamos si la esfera a llegado al final del laberinto
        if (Input.GetMouseButtonUp(0) && !estaAlFinal)
        {
            // Si no ha llegado, vuelve a la posicion inicial 
            transform.position = posicionInicial;
            estaEnMovimiento = false;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            estaEnMovimiento = true;
        }
    }
    // Si la esfera llega al final del laberinto, cambiamos a true, que esta al final
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == finLab)
        {
            estaAlFinal = true;
            minijuegoLaberinto.EjecutarVictoria();
        }
    }
}
