using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : AgentController
{
    [SerializeField] GameObject[] beanCosmetics;
    [SerializeField] GameObject[] cowboyCosmetics;
    [SerializeField] GameObject[] milkdiverCosmetics;
    [SerializeField] GameObject[] skyrimmerCosmetics;

    private void Start()
    {
        GameObject[] cosmetics = beanCosmetics;
        switch (PlayerData.Instance.selectedPlayerModel)
        {
            case PlayerData.PlayerModel.Bean:
                cosmetics = beanCosmetics;
                break;
            case PlayerData.PlayerModel.Cowboy:
                cosmetics = cowboyCosmetics;
                break;
            case PlayerData.PlayerModel.Milkdiver:
                cosmetics = milkdiverCosmetics;
                break;
            case PlayerData.PlayerModel.Skyrimmer:
                cosmetics = skyrimmerCosmetics;
                break;
        }
        for (int i = 0; i < cosmetics.Length; i++)
        {
            cosmetics[i].SetActive(true);
        }
    }

    private void Update()
    {
        if (!PauseInput)
        {
            StartAttack = Input.GetMouseButtonDown(0);
            DuringAttack = Input.GetMouseButton(0);
            EndAttack = Input.GetMouseButtonUp(0);
            Reload = Input.GetKeyDown(KeyCode.R);
            SwitchWeapon = Mathf.Abs(Input.mouseScrollDelta.y) > .5f;
            Aim = Input.GetMouseButton(1);
        }
    }
}
