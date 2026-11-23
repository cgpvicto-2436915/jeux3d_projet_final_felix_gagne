using UnityEngine;
using UnityEngine.InputSystem;

public class CameraChambre : MonoBehaviour
{
    private Vector2 rotation;

    private float rotationHorizontale = 70f;
    private float rotationVerticale = 45f;

    [SerializeField]
    private float vitesseRotation;
    [SerializeField]
    private float rotationMax;

    [SerializeField]
    private float rotationMin;

    [SerializeField]
    private GameObject tete;

    [SerializeField]
    private GameObject ui;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float vitesseZoom;
    [SerializeField]
    private float zoomMin;
    [SerializeField]
    private float zoomMax;
    [SerializeField]
    private Joueur joueur;

    private float directionZoom;

    private void Start()
    {
        joueur.activerCamera += Commencer;
        tete.SetActive(false);
        ui.SetActive(false);


    }

    /// <summary>
    /// Affiche le UI de camera et permet de pouvoir la deplacer.
    /// </summary>
    private void Commencer()
    {
        InputAction actionRotation = ControleurJeu.Instance.Controles.actions.FindAction("CameraSurveillance/Deplacement");
        actionRotation.performed += DebutRotation;
        actionRotation.canceled += FinRotation;

        InputAction actionZoom = ControleurJeu.Instance.Controles.actions.FindAction("CameraSurveillance/Zoom");
        actionZoom.performed += DebutZoom;
        actionZoom.canceled += FinZoom;

        tete.SetActive(true);
        ui.SetActive(true);



    }

    /// <summary>
    /// Arrete le deplacement de la camera et ferme le UI.
    /// </summary>
    public void Arreter()
    {
        if (ControleurJeu.Instance.Controles == null) { return; }
        InputAction actionRotation = ControleurJeu.Instance.Controles.actions.FindAction("CameraSurveillance/Deplacement");
        actionRotation.performed -= DebutRotation;
        actionRotation.canceled -= FinRotation;

        InputAction actionZoom = ControleurJeu.Instance.Controles.actions.FindAction("CameraSurveillance/Zoom");
        actionZoom.performed -= DebutZoom;
        actionZoom.canceled -= FinZoom;

        tete.SetActive(false);
        ui.SetActive(false);
        joueur.ModeCamera = false;
        
    }

    private void Update()
    {
        Tourner();
        Zoomer();
    }

    private void OnDestroy()
    {
        Arreter();
    }

    #region Rotation

    /// <summary>
    /// Arrete la rotation.
    /// </summary>
    /// <param name="contexte"></param>
    private void FinRotation(InputAction.CallbackContext contexte)
    {
        rotation = Vector2.zero;
    }

    /// <summary>
    /// Debut de la rotation de la camera.
    /// </summary>
    /// <param name="contexte"></param>
    private void DebutRotation(InputAction.CallbackContext contexte)
    {
        rotation = contexte.ReadValue<Vector2>();
    }

    /// <summary>
    /// Gere la rotation de la camera
    /// inspirer des notes de cours et du script de Joueur.
    /// </summary>
    public void Tourner()
    {
        float rotationHorizon = rotation.x * vitesseRotation * Time.deltaTime;
        float rotationSourisVerticale = rotation.y * vitesseRotation * Time.deltaTime;

        rotationVerticale -= rotationSourisVerticale;
        rotationVerticale = Mathf.Clamp(rotationVerticale, 0f, 90f);

        tete.transform.localRotation = Quaternion.Euler(rotationVerticale, 0f, 0f);

        rotationHorizontale += rotationHorizon;
        rotationHorizontale = Mathf.Clamp(rotationHorizontale, rotationMin, rotationMax);

        transform.rotation = Quaternion.Euler(0f,rotationHorizontale,0f);
    }
    #endregion

    #region Zoom 

    /// <summary>
    /// Commence le zoom
    /// </summary>
    /// <param name="contexte"></param>
    private void DebutZoom(InputAction.CallbackContext contexte)
    {
        directionZoom = contexte.ReadValue<float>();
    }

    /// <summary>
    /// arrete le zoom
    /// </summary>
    /// <param name="contexte"></param>
    private void FinZoom(InputAction.CallbackContext contexte)
    {
        directionZoom = 0f;
    }
    
    /// <summary>
    /// Zoom selon les touches appuye
    /// </summary>
    private void Zoomer()
    {
        float zoom = cam.fieldOfView;
        zoom -= directionZoom * vitesseZoom * Time.deltaTime;

        cam.fieldOfView = Mathf.Clamp(zoom, zoomMin, zoomMax);
    }
    #endregion
}
