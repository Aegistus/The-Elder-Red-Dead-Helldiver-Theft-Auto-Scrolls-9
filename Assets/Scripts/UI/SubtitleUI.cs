using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SubtitleUI : MonoBehaviour
{
    [SerializeField] bool spaceToContinue = true;
    [SerializeField] TMP_Text characterName;
    [SerializeField] TMP_Text text;
    [SerializeField] CinematicDialogue dialogue;
    [SerializeField] string nextSceneName;

    int frameIndex = -1;

    private void Start()
    {
        NextFrame();
    }

    private void Update()
    {
        if (spaceToContinue && Input.GetKeyDown(KeyCode.Space))
        {
            NextFrame();
        }
    }

    public void NextFrame()
    {
        frameIndex++;
        if (frameIndex >= dialogue.frames.Length)
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            if (dialogue.frames[frameIndex].characterName == "" && dialogue.frames[frameIndex].text == "")
            {
                characterName.text = "";
                text.text = "";
            }
            else
            {
                characterName.text = dialogue.frames[frameIndex].characterName + ":";
                text.text = dialogue.frames[frameIndex].text;
            }
        }
    }
}
