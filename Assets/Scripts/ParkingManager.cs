using UnityEngine;
using TMPro;

public class ParkingManager : MonoBehaviour
{
    public static ParkingManager instance;

    public int totalSpots = 0;
    public int occupiedSpots = 0;

    public TextMeshProUGUI parkingText;
    public TextMeshProUGUI timerText;

    private float tiempo = 0f;
    private bool juegoActivo = true;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (juegoActivo)
        {
            tiempo += Time.deltaTime;
            timerText.text = "Tiempo: " + tiempo.ToString("F1") + "s";
        }
    }

    public void RegisterSpot()
    {
        totalSpots++;
        UpdateUI();
    }

    public void ToggleSpot(bool isOccupied)
    {
        if (isOccupied)
            occupiedSpots++;
        else
            occupiedSpots--;

        UpdateUI();

        // 🏁 Verificar si se llenaron todos
        if (occupiedSpots >= totalSpots)
        {
            FinDelJuego();
        }
    }

    void UpdateUI()
    {
        int available = totalSpots - occupiedSpots;
        parkingText.text = "Disponibles: " + available + " / " + totalSpots;
    }

    void FinDelJuego()
    {
        juegoActivo = false;

        timerText.text = "Final: " + tiempo.ToString("F1") + "s\nTodos ocupados 🎉";

        Debug.Log("Juego terminado en: " + tiempo);
    }
}
