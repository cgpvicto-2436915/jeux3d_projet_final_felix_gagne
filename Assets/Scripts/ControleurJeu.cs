using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControleurJeu : MonoBehaviour
{
    public static ControleurJeu Instance { get; private set; }


    [field: SerializeField]
    public PlayerInput Controles { get; private set; }

    [field: SerializeField]
    public Joueur Joueur { get; private set; }

    public string CodePorteSortie { get; private set; }

    public float Temps { get; private set;}

    public bool EnJeu;
    [SerializeField]
    private TextMeshPro texteTemps;


    private void Awake()
    {
        EnJeu = false;
        for(int i = 0;i<6; i++)
        {
            CodePorteSortie += Random.Range(1,10).ToString();
        }
        Debug.Log(CodePorteSortie);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (EnJeu)
        {
            Temps += Time.deltaTime;
            
            //source temps : https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/
            int minutes = Mathf.FloorToInt(Temps / 60);
            int secondes = Mathf.FloorToInt(Temps % 60);

            texteTemps.text = $"{minutes:00}:{secondes:00}";
        }
    }
}
