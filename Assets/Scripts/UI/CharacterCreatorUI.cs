using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreatorUI : MonoBehaviour
{
    [SerializeField] CharacterOptionUI[] options;

    int currentOption = 0;

    private void Start()
    {
        options[0].Select();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            options[currentOption].Unselect();
            currentOption = (currentOption + 1) % options.Length;
            options[currentOption].Select();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            options[currentOption].Unselect();
            currentOption = currentOption == 0 ? options.Length - 1 : currentOption - 1;
            options[currentOption].Select();
        }
    }

    public void Continue()
    {
        PlayerData.Instance.selectedPlayerModel = options[currentOption].modelType;
        FindAnyObjectByType<SceneChanger>().ChangeScene();
    }
}
