using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Tiroir : MonoBehaviour
{
    [SerializeField]
    private Transform joueur;

    [SerializeField]
    private Animator animateur;

    private bool ouvert;

    [SerializeField]
    private PlayerInput controles;

    private void Start()
    {
        InputAction actionInteragir = controles.actions.FindAction("player/Interagir");
        actionInteragir.performed += OuvrirFermerTiroir;
        ouvert = false;
    }

    private void OnDestroy()
    {
        if(controles == null) { return; }
        InputAction actionInteragir = controles.actions.FindAction("player/Interagir");
        actionInteragir.performed += OuvrirFermerTiroir;
    }

    private void OuvrirFermerTiroir(InputAction.CallbackContext contexte)
    {
        ouvert = !ouvert;
        animateur.SetBool("Ouvert", ouvert);
    }
}
