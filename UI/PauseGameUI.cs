using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameUI : RyoMonoBehaviour
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _opstionsButton;
    [SerializeField] private Button _mainMenuButton;

    protected override void Awake()
    {
        base.Awake();

        this._mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });

        this._resumeButton.onClick.AddListener(() =>
        {
            KitchenGameManager.Instance.TogglePausegame();
        });
        
        this._opstionsButton.onClick.AddListener(() =>
        {
            OptionsUI.Instance.Show();
        });

    }

    protected override void Start()
    {
        base.Start();

        KitchenGameManager.Instance.OnGamePaused += KitchenGameManager_OnGamePaused;
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;

        this.Hide();

    }

    private void KitchenGameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        this.Hide();
    }

    private void KitchenGameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        this.Show();
    }

    private void Show()
    {
        this.gameObject.SetActive(true);
    }
    
    private void Hide()
    {
        this.gameObject.SetActive(false);
    }

}
