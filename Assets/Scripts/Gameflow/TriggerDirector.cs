using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerDirector : Step
{
    public PlayableDirector director;
   


    public override void StartGameflow()
    {
        director.Play();
        director.stopped += onPlayableDirectorEnded;

        
       
    }

    public void onPlayableDirectorEnded(PlayableDirector director)
    {
        GameflowManager.Instance.CompletedTutorial();

        Debug.Log("double?");
        BookManager.instance.blocNote.AddSentence("- Papa s'est encore enfermé dans le _bureau_... et il a même refusé de _jouer_ avec Emily. \n" +
           "- Il travaille beaucoup. \n" +
           "- Pourquoi ? \n" +
           "- Pour vous achetez des cadeaux à toi et ta soeur. \n" +
           "- Est-ce que je peux au moins lui apporter un verre d'eau ? \n" +
           "- Oui, je suis sur que ca lui fera plaisir. Un double des clés est caché sous le _pot de fleur_ dans le couloir.");
    }
}
