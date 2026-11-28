using TMPro;

using UnityEngine;
using UnityEngine.InputSystem;

public class Tiroir : MonoBehaviour, IInteragir
{
    [SerializeField]
    private Animator animateur;

    [SerializeField]
    private TextMeshProUGUI AffichageCode;

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private AudioSource audioTirroir;

    private bool ouvert;

    [SerializeField]
    private PlayerInput controles;

    private void Start()
    {

        ouvert = false;

        string code = ControleurJeu.Instance.CodePorteSortie;
        AffichageCode.SetText(code[0] + " " +code[1] + " _ _ _ _");
        
    }

    private void OnDestroy()
    {
        if(controles == null) { return; }
    }

    public void Interagir()
    {
        ouvert = !ouvert;
        audioTirroir.Play();
        animateur.SetBool("Ouvert", ouvert);
    }

}
