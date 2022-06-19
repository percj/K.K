using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamemanager : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    [SerializeField] BossController bossController;
    [SerializeField] AIController aIController;
    [SerializeField] TimerController timerController;
    [SerializeField] GameObject ko;
    [SerializeField] GameObject lose;
    [SerializeField] string nextScene;
    public bool isfinis;
    public void finishGame()
    {
        isfinis = true;
            controller.isFinish = true;
            if (controller.GetComponent<healthController>().isDeath)
                lose.SetActive(true);
            else
                ko.SetActive(true);
        if (aIController != null)
            aIController.isFinish = true;
        if (bossController != null)
            bossController.isFinish = true;
        timerController.finish = true;

        if(nextScene!= "")
        StartCoroutine(nextLevel());

    }
    IEnumerator nextLevel()
    {
        yield return new WaitForSeconds(2);
        if(controller.GetComponent<healthController>().isDeath)
        {
            SceneManager.LoadScene("MainMenu");
            yield break;
        }

        SceneManager.LoadScene(nextScene);
    }
}
