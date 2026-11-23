using UnityEngine;

public class Porte : MonoBehaviour
{
    [SerializeField]
    private Animator ouverture;

    public void Ouvrir()
    {
        //Utilisation de l'animation deja fait pour les portes
        // c'est pourquoi j'utilise .Play , comme dans le script de base
        ouverture.Play("Opening");
    }
}
