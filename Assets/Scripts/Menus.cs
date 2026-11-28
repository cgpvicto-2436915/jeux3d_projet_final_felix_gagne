using UnityEngine;

public class Menus : MonoBehaviour
{
    [SerializeField]
    private GameObject accueil;
    [SerializeField]
    private GameObject instruction;

    private void Start()
    {
        Menu();
    }
    public void Instruction()
    {
        accueil.SetActive(false);
        instruction.SetActive(true);
    }
    public void Menu()
    {
        accueil.SetActive(true);
        instruction.SetActive(false);
    }

    public void Quitter()
    {
        Application.Quit();
    }
    public void Jouer()
    {
        ControleurJeu.Instance.Joueur.ModeUi = false;
        ControleurJeu.Instance.EnJeu = true;
        this.gameObject.SetActive(false);
    }
}
