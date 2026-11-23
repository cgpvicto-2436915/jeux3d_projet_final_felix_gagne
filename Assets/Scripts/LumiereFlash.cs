using Mono.Cecil.Cil;
using System.Collections;
using System.Text.Encodings.Web;
using UnityEngine;

public class LumiereFlash : MonoBehaviour
{
    [SerializeField]
    private GameObject lumiere;
    private int nombreFlash;
    void Start()
    { 
        string code = ControleurJeu.Instance.CodePorteSortie;

        //methode de conversion : https://stackoverflow.com/questions/3665757/how-to-convert-char-to-int
        nombreFlash = (int)char.GetNumericValue(code[code.Length - 2]);
        StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        while (true) {

            StartCoroutine(SequenceCoroutine());
            yield return new WaitForSeconds(nombreFlash+2);

        }
    }

    private IEnumerator SequenceCoroutine()
    {


        for (int i = 0; i < nombreFlash; i++)
        {
            lumiere.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            lumiere.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

}
