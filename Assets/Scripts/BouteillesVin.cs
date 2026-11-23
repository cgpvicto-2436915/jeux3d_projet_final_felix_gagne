using System.Collections.Generic;
using UnityEngine;

public class BouteillesVin : MonoBehaviour
{
    private List<GameObject> bouteilles;
    void Start()
    {
        bouteilles = new List<GameObject>();

        //methode pour chercher toute les enfants provient des notes de cours: https://cours-alexandre-ouellet.github.io/jeux-3d/abc/placer-tour/?h=enfant#masquer-et-afficher-les-zones-de-construction
        foreach (Transform enfant in transform) {
            bouteilles.Add(enfant.gameObject);
            enfant.gameObject.SetActive(false);
        }

        string code = ControleurJeu.Instance.CodePorteSortie;

        //methode de conversion : https://stackoverflow.com/questions/3665757/how-to-convert-char-to-int
        int nombreBouteille = (int)char.GetNumericValue(code[code.Length-1]);



        for (int i = 0; i < nombreBouteille; i++) {
            bouteilles[i].gameObject.SetActive(true);
        }
        
    }

}
