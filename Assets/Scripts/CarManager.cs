using UnityEngine;

public class CarManager : MonoBehaviour
{
    public static CarManager instance;

    public GameObject carPrefab;
    public Transform spawnPoint;

    private GameObject currentCar;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SpawnCar();
    }

    public void SpawnCar()
    {
        if (currentCar != null) return;

        currentCar = Instantiate(carPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public void CarPlaced()
    {
        currentCar = null;

        Invoke("SpawnCar", 1.5f); // pequeño delay
    }
}
