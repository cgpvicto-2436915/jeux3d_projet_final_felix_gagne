using System.Collections;
using TMPro;
using UnityEngine;

public class ClavierCode : MonoBehaviour
{
    private string codeSaisi;

    private string bonCode;
    private bool verrouiller;

    [SerializeField]
    private GameObject ui;

    [SerializeField]
    private TextMeshProUGUI texteCode;

    private void Start()
    {
        bonCode = ControleurJeu.Instance.CodePorteSortie;
        codeSaisi = "";
        AfficherCode();
        verrouiller = false;
    }

    public void AfficherCode()
    {
        if (verrouiller) { return; }
        //Source : https://discussions.unity.com/t/changing-color-of-textmeshpro-in-code/804006/2
        texteCode.color = Color.white;

        string codeAfficher = "";
 
        for (int i = 0; i < bonCode.Length; i++) {
            if(codeSaisi.Length > i)
            {
                codeAfficher += codeSaisi[i] + " ";
            }
            else
            {
                codeAfficher+= "_ ";
            }
            
        }
        texteCode.SetText(codeAfficher);
    }

    public void AjouterChiffre(string chiffre)
    {
        if (verrouiller) { return; }

        if (codeSaisi.Length <bonCode.Length)
        {
            codeSaisi += chiffre;
        }
        
        AfficherCode();
    }
    public void Valider()
    {
        verrouiller=true;
        if(codeSaisi == bonCode)
        { 
            
            StartCoroutine(SuccesCoroutine());
        }
        else
        {
            StartCoroutine(ErreurCoroutine());
        }
    }

    private IEnumerator ErreurCoroutine()
    {
        texteCode.color = Color.red;
        for (int i = 0;i < 3; i++)
        {
            codeSaisi = "";
            texteCode.SetText("ERREUR!");
            yield return new WaitForSeconds(0.75f);
            texteCode.SetText("");
            yield return new WaitForSeconds(0.75f);
        }
        verrouiller = false;
        AfficherCode();
    }

    private IEnumerator SuccesCoroutine()
    {
        texteCode.color = Color.green;
        yield return new WaitForSeconds(1f);
        ui.gameObject.SetActive(false);
    }

    public void fermer()
    {
        codeSaisi = "";
        ui.gameObject.SetActive(false);
    }

    public void Supprimer()
    {
        if (verrouiller) { return; }
        codeSaisi = "";
        AfficherCode();
    }
}
