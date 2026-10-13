using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int totalLapCount;

    private void Awake()
    {
        // 1. Vérifier si une instance existe déjà
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Détruit le doublon
            return;
        }

        // 2. Assigner l'instance et rendre l'objet persistant
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
