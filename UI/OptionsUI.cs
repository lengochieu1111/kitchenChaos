using Pattern.Singleton;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : Singleton<OptionsUI>
{
    [SerializeField] private Button _sounfEffectsButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _closeButton;

    [SerializeField] private TextMeshProUGUI _sounfEffectsText;
    [SerializeField] private TextMeshProUGUI _musicText;

    [SerializeField] private Button _moveUpButton;
    [SerializeField] private Button _moveDownButton;
    [SerializeField] private Button _moveRightButton;
    [SerializeField] private Button _moveLeftButton;
    [SerializeField] private Button _interactButton;
    [SerializeField] private Button _interacAltButton;
    [SerializeField] private Button _pauseButton;
    
    [SerializeField] private TextMeshProUGUI _moveUpText;
    [SerializeField] private TextMeshProUGUI _moveDownText;
    [SerializeField] private TextMeshProUGUI _moveRightText;
    [SerializeField] private TextMeshProUGUI _moveLeftText;
    [SerializeField] private TextMeshProUGUI _interactText;
    [SerializeField] private TextMeshProUGUI _interacAltText;
    [SerializeField] private TextMeshProUGUI _pauseText;

    protected override void Awake()
    {
        base.Awake();

        this._sounfEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            this.UpdateVisual();
        });

        this._musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            this.UpdateVisual();
        });
        
        this._closeButton.onClick.AddListener(() =>
        {
            this.Hide();
        });

    }

    protected override void Start()
    {
        base.Start();

        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;

        this.UpdateVisual();

        this.Hide();
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        this.Hide();
    }

    private void UpdateVisual()
    {
        this._sounfEffectsText.text = "Sound Effects : " + Mathf.Round(SoundManager.Instance.GetVolume() * 10);
        this._musicText.text = "Music : " + Mathf.Round(MusicManager.Instance.GetVolume() * 10);

        this._moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        this._moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        this._moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        this._moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        this._interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        this._interacAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact_Alternate);
        this._pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

}
