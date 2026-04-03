using UnityEngine;

public class ParkingSpot : MonoBehaviour
{
    private Renderer rend;
    public Transform snapPoint;
    public bool ocupado = false;

    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        SetColor();

        ParkingManager.instance.RegisterSpot();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.IsChildOf(transform))
                    {
                        ocupado = !ocupado;
                        ParkingManager.instance.ToggleSpot(ocupado);
                        SetColor();
                    }
                }
            }
        }
    }

    void SetColor()
    {
        rend.material.color = ocupado ? Color.red : Color.green;
    }

    public void Ocupar(GameObject car)
    {
        if (ocupado) return;

        ocupado = true;

        car.transform.position = snapPoint.position;
        car.transform.rotation = transform.rotation;

        car.GetComponent<CarController>().enabled = false;

        ParkingManager.instance.ToggleSpot(true);

        CarManager.instance.CarPlaced();
    }



}
