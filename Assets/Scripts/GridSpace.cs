using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridSpace : MonoBehaviour
{
    [SerializeField] Button GridSpaceButton;
    [SerializeField] TextMeshProUGUI GridSpaceButtonText;
    
    [HideInInspector] public string InitialPlayerChoice;

    public void SetGridSpace()
    {
        GridSpaceButtonText.text = GameManager.Instance.GetPlayerChoice();
        GameManager.Instance.PlayerClick(GridSpaceButtonText.text);
        GridSpaceButton.interactable = false;
        GameManager.Instance.EndPlayerTurn();
    }
}
