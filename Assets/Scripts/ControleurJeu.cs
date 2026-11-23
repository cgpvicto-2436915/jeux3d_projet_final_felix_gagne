using UnityEngine;
using UnityEngine.InputSystem;

public class ControleurJeu : MonoBehaviour
{
    public static ControleurJeu Instance { get; private set; }


    [field: SerializeField]
    public PlayerInput Controles { get; private set; }

    [field: SerializeField]
    public Joueur Joueur { get; private set; }

    public string CodePorteSortie { get; private set; }

    private void Awake()
    {
        for(int i = 0;i<6; i++)
        {
            CodePorteSortie += Random.Range(0,10).ToString();
        }
        Debug.Log(CodePorteSortie);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
