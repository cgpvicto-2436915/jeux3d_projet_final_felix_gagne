using UnityEngine;
using UnityEngine.InputSystem;

public class ControleurJeu : MonoBehaviour
{
    public static ControleurJeu Instance { get; private set; }


    [field: SerializeField]
    public PlayerInput Controles { get; private set; }

    private void Awake()
    {
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
