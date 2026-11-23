using UnityEngine;

public class IconeInteragir : MonoBehaviour
{
    [SerializeField]
    private GameObject interagir;
    [SerializeField]
    private float distance = 2.5f;

    private Transform joueur;

    private void Start()
    {
        joueur = ControleurJeu.Instance.Joueur.transform;
    }

    private void Update()
    {
        float distanceJoueur = Vector3.Distance(this.gameObject.transform.position, joueur.position);
        if (distanceJoueur < distance)
        {
            interagir.SetActive(true);
        }
        else
        {
            interagir.SetActive(false);
        }

    }
}
