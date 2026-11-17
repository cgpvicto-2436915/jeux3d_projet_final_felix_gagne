using TMPro;

using UnityEngine;
using UnityEngine.InputSystem;

public class Tiroir : MonoBehaviour
{
    [SerializeField]
    private Transform joueur;

    [SerializeField]
    private Animator animateur;

    [SerializeField]
    private TextMeshProUGUI AffichageCode;

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
        InputAction actionInteragir = controles.actions.FindAction("player/Interagir");
        actionInteragir.performed += OuvrirFermerTiroir;

        ouvert = false;

        string code = ControleurJeu.Instance.CodePorteSortie;
        AffichageCode.SetText(code[0] + " " +code[1] + " _ _ _ _");
        
    }

    private void OnDestroy()
    {
        if(controles == null) { return; }
        InputAction actionInteragir = controles.actions.FindAction("player/Interagir");
        actionInteragir.performed -= OuvrirFermerTiroir;
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

    private void OuvrirFermerTiroir(InputAction.CallbackContext contexte)
    {

        //Code de raycast provient des notes de cours

        Vector2 positionSouris = Mouse.current.position.ReadValue();
        Ray rayon = cam.ScreenPointToRay(positionSouris);

        if (Physics.Raycast(rayon, out RaycastHit infoRaycast, Mathf.Infinity))
        {

            if (infoRaycast.collider.gameObject == this.gameObject)
            {
                float distanceJoueur = Vector3.Distance(this.gameObject.transform.position,joueur.position);
                if (distanceJoueur < distanceOuverture)
                {

                    ouvert = !ouvert;
                    animateur.SetBool("Ouvert", ouvert);
                }

            }
        }


    }
}
