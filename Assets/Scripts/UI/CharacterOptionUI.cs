using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterOptionUI : MonoBehaviour
{
    public PlayerData.PlayerModel modelType;

    [SerializeField] GameObject[] models;
    TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        Unselect();
    }

    public void Select()
    {
        for (int i = 0; i < models.Length; i++)
        {
            models[i].SetActive(true);
        }
        text.fontStyle = FontStyles.Bold | FontStyles.Underline;
    }

    public void Unselect()
    {
        for (int i = 0; i < models.Length; i++)
        {
            models[i].SetActive(false);
        }
        text.fontStyle = FontStyles.Normal;
    }
}
