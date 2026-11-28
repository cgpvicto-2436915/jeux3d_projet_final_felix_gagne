using System;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class Joueur : MonoBehaviour
{
    [SerializeField]
    private float vitesseDeplacement;

    [SerializeField]
    private float vitesseRotation;

    [SerializeField]
    private PlayerInput controles;

    [SerializeField]
    private GameObject cible;

    [SerializeField]
    private GameObject cameraChambre;
    
    [SerializeField]
    private GameObject cameraJoueur;

    /// <summary>
    /// Le deplacement actuel
    /// </summary>
    private Vector2 deplacement;
    private Rigidbody rb;

    /// <summary>
    /// Rotation de la camera/joueur
    /// </summary>
    private Vector2 rotation;


    private float rotationVerticale;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float distanceInteraction;
    [SerializeField]
    private AudioSource audioPas;

    public bool ModeUi { get; set; }
    public bool ModeCamera { get; set; }

    public event Action activerCamera;
    
    [SerializeField]
    public Musique musique;

    /// <summary>
    /// Le position de l'objet qui fait la tete
    /// </summary>
    [SerializeField]
    private Transform tete;

    private EtatJoueur etatPrecedent;
    private EtatJoueur prochainEtat;

    private void Start()
    {
        ModeCamera = false;
        ModeUi = true;
        etatPrecedent = null;
        prochainEtat = new EtatUi();

    }

    public void ActiverCamera()
    {
        activerCamera?.Invoke();
        Debug.Log("Activer camera");
    }
    public void PouvoirBouger()
    {
        //https://gamedevbeginner.com/how-to-lock-hide-the-cursor-in-unity/
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        InputAction action = controles.actions.FindAction("player/deplacement");
        action.performed += DebutDeplacement;
        action.canceled += FinDeplacement;

        InputAction actionRotation = controles.actions.FindAction("player/Regarder");
        actionRotation.performed += DebutRotation;
        cible.SetActive(true);

        InputAction actionInteragir = ControleurJeu.Instance.Controles.actions.FindAction("player/Interagir");
        actionInteragir.performed += Interagir;
    }

    public void Interagir(InputAction.CallbackContext contexte)
    {
        //Code de raycast inspiré des notes de cours
        Vector2 positionSouris = Mouse.current.position.ReadValue();
        Ray rayon = cam.ScreenPointToRay(positionSouris);

        if (Physics.Raycast(rayon, out RaycastHit infoRaycast, Mathf.Infinity))
        {

            Vector3 positionObjet = infoRaycast.collider.transform.position; 
            float distanceObjet = Vector3.Distance(this.gameObject.transform.position, positionObjet);

            if (distanceObjet < distanceInteraction)
            {

                IInteragir script = infoRaycast.collider.gameObject.GetComponent<IInteragir>();

                if(script != null)
                {

                    script.Interagir();
                }
                    
            }

        }
    }

    public void ArreterPouvoirBouger()
    {

        //https://gamedevbeginner.com/how-to-lock-hide-the-cursor-in-unity/
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        deplacement = Vector2.zero;
        if (controles == null || controles.actions == null) { return; }

        InputAction action = controles.actions.FindAction("player/deplacement");
        action.performed -= DebutDeplacement;
        action.canceled -= FinDeplacement;

        InputAction actionRotation = controles.actions.FindAction("player/Regarder");
        actionRotation.performed -= DebutRotation;
        cible.SetActive(false);

        InputAction actionInteragir = ControleurJeu.Instance.Controles.actions.FindAction("player/Interagir");
        actionInteragir.performed -= Interagir;
    }

    public void TransformerEnCamera()
    {
        cameraChambre.SetActive(true);
        cameraJoueur.SetActive(false);


    }
    public void QuitterCamera()
    {
        cameraJoueur.SetActive(true);
        cameraChambre.SetActive(false);

    }

    private void FixedUpdate()
    {
        Deplacer();
        
    }

    private void Update()
    {
        if(etatPrecedent != prochainEtat)
        {
            prochainEtat.OnEnter(this);
        }
        etatPrecedent = prochainEtat;
        prochainEtat = prochainEtat.OnExecuter(this);

        if (etatPrecedent != prochainEtat) 
        {
            etatPrecedent.OnSortir(this);
        }
    }

    private void OnDestroy()
    {
        ArreterPouvoirBouger();

    }
    #region deplacement
    //Inspirer du deplacement de la cible camera dans les notes de cours.

    /// <summary>
    /// Debut du deplacement de joueur
    /// </summary>
    /// <param name="contexte">Information du callback de l'action</param>
    private void DebutDeplacement(InputAction.CallbackContext contexte)
    {
        deplacement = vitesseDeplacement * contexte.ReadValue<Vector2>().normalized;
        audioPas.Play();
    }

    /// <summary>
    /// Arrete le deplacement du joueur
    /// </summary>
    /// <param name="contexte">Information du callback de l'action</param>
    private void FinDeplacement(InputAction.CallbackContext contexte)
    {
        deplacement = Vector2.zero;
        audioPas.Stop();
    }

    /// <summary>
    /// Gère le déplacement du joueur selon l'input du joueur
    /// </summary>
    public void Deplacer()
    {
        if (deplacement.sqrMagnitude > 0.0f)
        {
            Vector3 prochainDeplacement = transform.right * deplacement.x +
            transform.forward * deplacement.y;

            prochainDeplacement = Vector3.Scale(prochainDeplacement, new Vector3(1.0f, 0.0f, 1.0f));
            prochainDeplacement *= Time.fixedDeltaTime;

            rb.MovePosition(rb.position + prochainDeplacement);

        }
    }
    #endregion

    #region rotation
    //Inspirer de ce tutoriel: https://www.youtube.com/watch?v=94MHshe7FAY&t=62s.


    /// <summary>
    /// Debut de la rotation de la camera du joueur.
    /// </summary>
    /// <param name="contexte"></param>
    private void DebutRotation(InputAction.CallbackContext contexte)
    {
        rotation = contexte.ReadValue<Vector2>();
    }

    /// <summary>
    /// Gere la rotation de la camera selon la position de la souris
    /// </summary>
    public void Tourner()
    {
        float rotationHorizon = rotation.x * vitesseRotation * Time.deltaTime;
        float rotationSourisVerticale = rotation.y * vitesseRotation * Time.deltaTime;

        rotationVerticale -= rotationSourisVerticale;
        rotationVerticale = Mathf.Clamp(rotationVerticale, -90f, 90f);

        tete.localRotation = Quaternion.Euler(rotationVerticale, 0f, 0f);

        transform.Rotate(Vector3.up * rotationHorizon);

        rotation = Vector2.zero;
    }

    #endregion

}
