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

    /// <summary>
    /// Le position de l'objet qui fait la tete
    /// </summary>
    [SerializeField]
    private Transform tete;

    private void Start()
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

    }

    private void FixedUpdate()
    {
        Deplacer();
    }

    private void Update()
    {
        Tourner();
    }

    private void OnDestroy()
    {
        if (controles == null || controles.actions == null) { return; }

        InputAction action = controles.actions.FindAction("player/deplacement");
        action.performed -= DebutDeplacement;
        action.canceled -= FinDeplacement;

        InputAction actionRotation = controles.actions.FindAction("player/Regarder");
        actionRotation.performed -= DebutRotation;

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
    }

    /// <summary>
    /// Arrete le deplacement du joueur
    /// </summary>
    /// <param name="contexte">Information du callback de l'action</param>
    private void FinDeplacement(InputAction.CallbackContext contexte)
    {
        deplacement = Vector2.zero;
    }

    /// <summary>
    /// Gère le déplacement du joueur selon l'input du joueur
    /// </summary>
    private void Deplacer()
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
    private void Tourner()
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
