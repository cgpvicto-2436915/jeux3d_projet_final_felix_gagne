using TMPro;
using UnityEngine;

public class Ordinateur : MonoBehaviour, IInteragir
{
    [SerializeField]
    private string motDePasse;

    [SerializeField]
    private GameObject ui;

    [SerializeField]
    private GameObject erreur;
    [SerializeField]
    private TMP_InputField champMDP;

    /// <summary>
    /// Afficher la fenetre de l'ordinateur
    /// </summary>
    public void Interagir()
    {
        ui.SetActive(true);
        erreur.SetActive(false);
        ControleurJeu.Instance.Joueur.ModeUi = true;
    }

    private void Start()
    {
        ui.SetActive(false);
        
    }

    /// <summary>
    /// Verifier si le mot de passe est correcte.
    /// Si oui, passe en mode camera,
    /// si non, affichage d'une erreur.
    /// </summary>
    public void Connection()
    {
        if (champMDP.text == motDePasse)
        {
            ControleurJeu.Instance.Joueur.ModeCamera = true;
            ui.SetActive(false);
        }
        else
        {
            erreur.SetActive(true);
            champMDP.text = "";
        }
    }

    /// <summary>
    /// Ferme le UI de l'ordinateur
    /// </summary>
    public void Fermer()
    {
        ui.SetActive(false);
        ControleurJeu.Instance.Joueur.ModeUi = false;
    }
}
