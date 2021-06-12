using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UISystem : MonoBehaviour
{
    public GameObject BackgroundWhenMenuIsOpen;

    public UIScreen StartingScreen;
    public UIScreen[] screens = new UIScreen[0];
    public UnityEngine.UI.Button[] screenButtons = new UnityEngine.UI.Button[0];

    private bool areButtonsInteractable; //Used instead of checking everytime.
    private UIScreen currentScreen;
    private UIScreen previousScreen;

    //MenuWhilePlaying Variables
    public GameObject Menu;
    public UnityEvent OnMenuActive;
    public UIScreen MenuUIScreen;

    private void Start()
    {
        screens = gameObject.GetComponentsInChildren<UIScreen>(true);
        screenButtons = gameObject.GetComponentsInChildren<UnityEngine.UI.Button>(true);

        foreach (var screen in screens)
        {
            var screenCanvas = screen.GetComponent<CanvasGroup>();
            if (screenCanvas != null)
                screenCanvas.alpha = 0;
        }

        if (StartingScreen != null)
            SwitchScreen(StartingScreen);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Menu != null && MenuUIScreen != null)
            MenuWhilePLaying(Menu, MenuUIScreen);

        foreach (var screen in screens) //Disable any screen other than the current one if it's alpha is 0(which what all previous screens should be),
            //you wait for the alpha to be zero so that fade out ends before disabling the previous screen.
            if (screen != currentScreen)
                if (screen.canvasGroup != null)
                    if (screen.canvasGroup.alpha == 0)
                        screen.gameObject.SetActive(false);

        if (currentScreen != null) //Is there any screen open?
        {
            Utility.LocalTimeScale = 0; //For pausing the game
            if (currentScreen.canvasGroup.alpha < 0.98f) //So that you can't press buttons while transitioning from one screen to the other
            {
                if (screenButtons != null && areButtonsInteractable)
                    foreach (var screenButton in screenButtons)
                    {
                        screenButton.interactable = false;
                        areButtonsInteractable = false;
                    }
            }
            else
            {
                if (screenButtons != null && !areButtonsInteractable)
                    foreach (var screenButton in screenButtons)
                    {
                        screenButton.interactable = true;
                        areButtonsInteractable = true;
                    }
            }
        }
        else
        {
            Utility.LocalTimeScale = 1;
        }

        if (currentScreen != null)
            if (Math.Abs(currentScreen.canvasGroup.alpha - 1) < 0.04f) //So that you can't go back while transitioning from one screen to the other
                if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Escape))
                    SwitchToPreviousScreen();
    }

    public void SwitchScreen(UIScreen screen)
    {
        if (currentScreen != screen) //No need to fuck-up the screens order if you somehow didn't switch to a different screen.
            if (screen != null)
            {
                if (currentScreen != null)
                {
                    currentScreen.CloseScreen();
                    previousScreen = currentScreen;
                }

                currentScreen = screen;
                currentScreen.gameObject.SetActive(true);

                currentScreen.StartScreen();
            }
    }

    public void SwitchToPreviousScreen()
    {
        if (currentScreen.CloseInsteadOfPrevious)
        {
            if (BackgroundWhenMenuIsOpen != null) BackgroundWhenMenuIsOpen.SetActive(false);
            currentScreen.CloseScreen();
            currentScreen = null;
        } //Is there a SpecificPreviousScreen put? switch to it, otherwise just switch to the previous screen you were in.
        else if (currentScreen.canReturnToPreviousScreen)
        {
            if (previousScreen != null)
                SwitchScreen(previousScreen);
        }
    }

    public void MenuWhilePLaying(GameObject menu, UIScreen menuScreen)
    {
        //If there is any screen that is currently active, close it.
        foreach (var screen in menu.GetComponentsInChildren<UIScreen>(true))
            if (screen.gameObject.activeSelf)
                screen.CloseScreen();

        //If there isn't any screen that's active, switch to MenuScreen.
        if (!menuScreen.gameObject.activeSelf && currentScreen == null)
        {
            SwitchScreen(menuScreen);
            if (BackgroundWhenMenuIsOpen != null)
                BackgroundWhenMenuIsOpen.SetActive(true);
        }
        else
        {
            menuScreen.CloseScreen();
            currentScreen = null;
            if (BackgroundWhenMenuIsOpen != null)
                BackgroundWhenMenuIsOpen.SetActive(false);
        }

        OnMenuActive?.Invoke();
    }

    public void CloseCurrentScreen()
    {
        if (BackgroundWhenMenuIsOpen != null) BackgroundWhenMenuIsOpen.SetActive(false);
        currentScreen.CloseScreen();
        currentScreen = null;
    }

    public void ReturnToTitleScreen()
    {
        LoadScene(0);
        Destroy(gameObject);
    }

    public void LoadScene(string SceneName) //Just used for the "Play" button.
    {
        SceneManager.LoadScene(SceneName);
    }

    public void LoadScene(int SceneNumber) //Just used for the "Play" button.
    {
        SceneManager.LoadScene(SceneNumber);
    }

    public void ExitGame() //Just used for the "Exit" button.
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}