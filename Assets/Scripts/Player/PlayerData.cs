using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    public enum PlayerModel
    {
        Bean, Cowboy, Milkdiver, Skyrimmer
    }

    public PlayerModel selectedPlayerModel = PlayerModel.Skyrimmer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
