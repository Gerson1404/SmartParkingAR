using UnityEngine;

public class CarController : MonoBehaviour
{
    private Camera cam;
    private bool isDragging = false;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Ray ray = cam.ScreenPointToRay(touch.position);
            RaycastHit hit;

            // TOCAR
            if (touch.phase == TouchPhase.Began)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == transform)
                    {
                        isDragging = true;
                    }
                }
            }

            // MOVER
            if (touch.phase == TouchPhase.Moved && isDragging)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                }
            }

            // SOLTAR
            if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;

                if (Physics.Raycast(ray, out hit))
                {
                    ParkingSpot spot = hit.transform.GetComponentInParent<ParkingSpot>();

                    if (spot != null && !spot.ocupado)
                    {
                        spot.Ocupar(gameObject);
                    }
                }
            }
        }
    }
}
