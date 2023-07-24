using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    [SerializeField]
    private List<TutorialBase> tutorials;
    [SerializeField]
    private string nextScene = "Stage1";

    private TutorialBase currentTutorial = null;
    private int currentIndex = -1;


    void Start()
    {
        SetNextTutorial();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTutorial != null)
        {
            currentTutorial.Execute(this);
        }
    }

    public void SetNextTutorial()
    {
        if (currentTutorial != null)
        {
            currentTutorial.Exit();
        }

        if (currentIndex >= tutorials.Count - 1) 
        {
            CompleteTutorials();
            return;
        }

        currentIndex++;
        currentTutorial = tutorials[currentIndex];

        currentTutorial.Enter();
    }

    public void CompleteTutorials()
    {
        currentTutorial = null;

        if(!nextScene.Equals(""))
            SceneManager.LoadScene(nextScene);
    }
}
