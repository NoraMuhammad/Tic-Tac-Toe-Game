using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    const string Str_AnotherTurn = "Another Turn ?";
    const string Str_ChooseYourSide = "Choose your side\n'X' or 'O'";
    const string Str_Draw = "DRAW !";
    public const string Str_PlayerX = "X";
    public const string Str_PlayerO = "O";

    string _playerChoice;
    static GameManager _instance;

    int _moveCount = 0;
    public static int ScorePlayerX = 0;
    public static int ScorePlayerO = 0;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip PlayerX_Audio;
    [SerializeField] AudioClip PlayerO_Audio;
    [SerializeField] AudioClip Win_Audio;
    [SerializeField] AudioClip Draw_Audio;

    //Properties
    public static GameManager Instance => _instance;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        UIManager.UIManagerInstance.SetGameManagerMSG(Str_ChooseYourSide);
        UIManager.UIManagerInstance.LockGrid();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.UIManagerInstance.ShowEscPanel(true);
        }
    }

    #region Player Options
    public string GetPlayerChoice()
    {
        return _playerChoice;
    }
    public void SetPlayerChoice(string CurrPlayerChoice)
    {
        _playerChoice = CurrPlayerChoice;
    }
    public void EndPlayerTurn()
    {
        _moveCount++;

        TMPro.TextMeshProUGUI[] PlayersTexts = UIManager.UIManagerInstance.GetPlayerTexts();
        if ((PlayersTexts[0].text == _playerChoice && PlayersTexts[1].text == _playerChoice && PlayersTexts[2].text == _playerChoice)
            || (PlayersTexts[0].text == _playerChoice && PlayersTexts[3].text == _playerChoice && PlayersTexts[6].text == _playerChoice)
            || (PlayersTexts[0].text == _playerChoice && PlayersTexts[4].text == _playerChoice && PlayersTexts[8].text == _playerChoice)
            || (PlayersTexts[1].text == _playerChoice && PlayersTexts[4].text == _playerChoice && PlayersTexts[7].text == _playerChoice)
            || (PlayersTexts[2].text == _playerChoice && PlayersTexts[5].text == _playerChoice && PlayersTexts[8].text == _playerChoice)
            || (PlayersTexts[2].text == _playerChoice && PlayersTexts[4].text == _playerChoice && PlayersTexts[6].text == _playerChoice)
            || (PlayersTexts[3].text == _playerChoice && PlayersTexts[4].text == _playerChoice && PlayersTexts[5].text == _playerChoice)
            || (PlayersTexts[6].text == _playerChoice && PlayersTexts[7].text == _playerChoice && PlayersTexts[8].text == _playerChoice))
        {
            GameIsOver("");
            if(_playerChoice == Str_PlayerX)
            {
                ScorePlayerX++;
            }
            else if (_playerChoice == Str_PlayerO)
            {
                ScorePlayerO++;
            }
            UIManager.UIManagerInstance.UpdateScore();
        }
        else if (_moveCount == 9) //draw
        {
            GameIsOver(Str_Draw);
        }
        else
        {
            SwapPlayer();
            UIManager.UIManagerInstance.SetGameManagerMSG(GetPlayerChoice() + "turn");
        }
    }
    void SwapPlayer()
    {
        _playerChoice = (_playerChoice == Str_PlayerX) ? Str_PlayerO : Str_PlayerX;
    }
    public void PlayerClick(string playerChoice)
    {
        if (playerChoice == Str_PlayerX)
        {
            PlayAudio(PlayerX_Audio);
        }
        else if (playerChoice == Str_PlayerO)
        {
            PlayAudio(PlayerO_Audio);
        }
    }
    #endregion

    #region Game Options
    void GameIsOver(string status)
    {
        _moveCount = 0;

        UIManager.UIManagerInstance.LockGrid();
        UIManager.UIManagerInstance.SetGameManagerMSG(Str_AnotherTurn);
        UIManager.UIManagerInstance.SetGameManagerBtnInteractable(true);
        UIManager.UIManagerInstance.SetPlayersChoiceInteractable(false);

        if(string.Compare(status, Str_Draw) == 0)
        {
            UIManager.UIManagerInstance.ShowDrawPanel(true);
            PlayAudio(Draw_Audio);
        }
        else
        {
            UIManager.UIManagerInstance.ShowWinnerPanel(_playerChoice, true);
            PlayAudio(Win_Audio);
        }
    }
    public void StartAnotherTurn()
    {
        _moveCount = 0;

        UIManager.UIManagerInstance.ShowDrawPanel(false);
        UIManager.UIManagerInstance.ShowWinnerPanel(_playerChoice, false);
        UIManager.UIManagerInstance.BlankTexts();
        UIManager.UIManagerInstance.LockGrid();
        UIManager.UIManagerInstance.SetGameManagerBtnInteractable(false);
        UIManager.UIManagerInstance.SetGameManagerMSG(Str_ChooseYourSide);
        UIManager.UIManagerInstance.SetPlayersChoiceInteractable(true);
    }
    public void StartNewGame()
    {
        StartAnotherTurn();
        ScorePlayerX = 0;
        ScorePlayerO = 0;

        UIManager.UIManagerInstance.UpdateScore();
        UIManager.UIManagerInstance.ShowEscPanel(false);
    }
    public void QuitTheGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    void PlayAudio(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
    #endregion
}