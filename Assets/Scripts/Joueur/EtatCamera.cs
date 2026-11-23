using UnityEngine;

public class EtatCamera : EtatJoueur
{
    /// <summary>
    /// active la camera
    /// </summary>
    /// <param name="joueur">Joueur qui doit etre modifier par l'etat</param>
    public override void OnEnter(Joueur joueur)
    {
        joueur.ModeUi = false;
        joueur.ActiverCamera();
        
    }

    /// <summary>
    /// Change d'etat si le joueur n'est plus en mode camera
    /// </summary>
    /// <param name="joueur">Joueur qui doit etre modifier par l'etat</param>
    /// <returns>L'etat pour la prochaine frame</returns>
    public override EtatJoueur OnExecuter(Joueur joueur)
    {
        if (!joueur.ModeCamera)
        {
            return new EtatDeplacement();
        }
        return this;
    }
}
