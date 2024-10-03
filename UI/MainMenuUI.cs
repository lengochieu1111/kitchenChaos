using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : RyoMonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;

    protected override void Awake()
    {
        base.Awake();

        this._playButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.GameScene);
        });
        
        this._quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        Time.timeScale = 1.0f;

    }

    

}
