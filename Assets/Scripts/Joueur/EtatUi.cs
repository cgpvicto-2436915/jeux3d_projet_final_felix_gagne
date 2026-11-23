using UnityEngine;

public class EtatUi : EtatJoueur
{
    /// <summary>
    /// Change d'etat si le joueur n'est plus en mode ui
    /// </summary>
    /// <param name="joueur">Joueur qui doit etre modifier par l'etat</param>
    /// <returns>L'etat pour la prochaine frame</returns>
    public override EtatJoueur OnExecuter(Joueur joueur)
    {
        if (!joueur.ModeUi)
        {
            return new EtatDeplacement();

        }else if (joueur.ModeCamera)
        {
            return new EtatCamera();
        }


        return this;
    }
}
