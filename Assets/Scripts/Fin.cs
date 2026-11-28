using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fin : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI texteTemps;

    [SerializeField]
    private GameObject uiFin;

    private void Start()
    {
        uiFin.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        ControleurJeu.Instance.Joueur.ModeUi = true;
        uiFin.SetActive(true);

        //source temps : https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/
        float temps = ControleurJeu.Instance.Temps;
        int minutes = Mathf.FloorToInt(temps/60);
        int secondes = Mathf.FloorToInt(temps%60);
        
        texteTemps.text = $"{minutes:00}:{secondes:00}";
    }
    public void Quitter()
    {
        Application.Quit();
    }
    public void Rejouer()
    {
        //source : https://stackoverflow.com/questions/65851443/how-do-i-restart-the-scene-that-im-currently-in-through-script-in-unity-2d-so
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
