using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    static UIManager _instance;

    [Header("Button")]
    [SerializeField] Button GameManagerBtn;
    [SerializeField] Button[] PlayersButtons;
    [SerializeField] Button PlayerX;
    [SerializeField] Button PlayerO;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI GameManagerMSGs;
    [SerializeField] TextMeshProUGUI WinnerPlayer;
    [SerializeField] TextMeshProUGUI[] PlayersTexts;
    [SerializeField] TextMeshProUGUI Score_PlayerX;
    [SerializeField] TextMeshProUGUI Score_PlayerO;

    [Header("Panel")]
    [SerializeField] GameObject WinnerPanel;
    [SerializeField] GameObject DrawPanel;
    [SerializeField] GameObject ESCPanel;

    //Properties
    public static UIManager UIManagerInstance => _instance;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
        else
            Destroy(gameObject);

        GameManagerBtn.interactable = false;
    }
    public void SetInitialPlayerChoice(string choice)
    {
        GameManager.Instance.SetPlayerChoice(choice);
        SetGameManagerMSG(GameManager.Instance.GetPlayerChoice() + "turn");
        UnLockGrid();
    }
    public void SetGameManagerMSG(string message)
    {
        GameManagerMSGs.text = message;
    }
    public void SetGameManagerBtnInteractable(bool _isInteractable)
    {
        GameManagerBtn.interactable = _isInteractable;
    }
    public void LockGrid()
    {
        for (int i = 0; i < PlayersButtons.Length; i++)
        {
            PlayersButtons[i].interactable = false;
        }
    }
    public void UnLockGrid()
    {
        for (int i = 0; i < PlayersButtons.Length; i++)
        {
            PlayersButtons[i].interactable = true;
        }
    }
    public void BlankTexts()
    {
        for (int i = 0; i < PlayersTexts.Length; i++)
        {
            PlayersTexts[i].text = "";
        }
    }
    public TextMeshProUGUI[] GetPlayerTexts()
    {
        return PlayersTexts;
    }
    public void UpdateScore()
    {
        Score_PlayerX.text = GameManager.ScorePlayerX.ToString();
        Score_PlayerO.text = GameManager.ScorePlayerO.ToString();
    }
    public void ShowWinnerPanel(string _playerChoice, bool _isActive)
    {
        WinnerPlayer.text = _playerChoice;
        WinnerPanel.SetActive(_isActive);
    }
    public void ShowDrawPanel(bool _isActive)
    {
        DrawPanel.SetActive(_isActive);
    }
    public void ShowEscPanel(bool _isActive)
    {
        ESCPanel.SetActive(_isActive);
    }
    public void SetPlayersChoiceInteractable(bool _isActive)
    {
        PlayerX.gameObject.SetActive(_isActive);
        PlayerO.gameObject.SetActive(_isActive);
    }
}
