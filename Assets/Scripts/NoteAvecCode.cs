using TMPro;
using UnityEngine;

public class NoteAvecCode : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI texteNote;
    private void Start()
    {
        string code = ControleurJeu.Instance.CodePorteSortie;
        texteNote.SetText("_ _ " + code[2] +" " + code[3]);
    }
}
