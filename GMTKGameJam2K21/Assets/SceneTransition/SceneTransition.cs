using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    #region Singleton pattern

    public static SceneTransition Instance { get; private set; }

    private void Awake()
    {
        sceneTransitionAnimator.gameObject.SetActive(true);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    [SerializeField] internal Animator sceneTransitionAnimator = null;
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += closeTransition;
        SceneManager.sceneLoaded += DestroyInTitle;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= closeTransition;
        SceneManager.sceneLoaded -= DestroyInTitle;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLevel();
        }
    }

    private void DestroyInTitle(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(gameObject);
            Destroy(ProjectilesManager.Instance.gameObject);

            var menuCanvas = GameObject.Find("MenuCanvas");
            if(menuCanvas != null)
             Destroy(menuCanvas);
        }
    }
    public void ResetLevel()
    {
        ProjectilesManager.Instance.CloseTextBox();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartTransition(int sceneNumber)
    {
        sceneTransitionAnimator.gameObject.SetActive(true);
        sceneTransitionAnimator.Play("Expand");
        StartCoroutine(EndTransition(sceneNumber));
    }

    IEnumerator EndTransition(int sceneNumber)
    {
        ProjectilesManager.Instance.CloseTextBox();
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene(sceneNumber);
    }

    public void closeTransition(Scene scene, LoadSceneMode loadSceneMode)
    {
        sceneTransitionAnimator.Play("Close");
    }
}