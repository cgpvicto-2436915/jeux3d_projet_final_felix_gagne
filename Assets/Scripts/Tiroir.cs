using TMPro;

using UnityEngine;
using UnityEngine.InputSystem;

public class Tiroir : MonoBehaviour, IInteragir
{
    [SerializeField]
    private Transform joueur;

    [SerializeField]
    private Animator animateur;

    [SerializeField]
    private TextMeshProUGUI AffichageCode;

    [SerializeField]
    private float distanceOuverture = 2.5f;

    [SerializeField]
    private GameObject interagir;

    [SerializeField]
    private Camera cam;

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

    private void Update()
    {
        float distanceJoueur = Vector3.Distance(this.gameObject.transform.position, joueur.position);
        if (distanceJoueur < distanceOuverture) {
            interagir.SetActive(true);
        }
        else
        {
            interagir.SetActive(false);
        }

    }

    public void Interagir()
    {
        ouvert = !ouvert;
        animateur.SetBool("Ouvert", ouvert);
    }

}
