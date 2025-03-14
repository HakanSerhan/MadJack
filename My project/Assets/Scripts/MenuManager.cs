using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    [SerializeField] private Button[] buttons;

    private void Start()
    {
        ButtonsController();
    }

    private void ButtonsController()
    {
        for (var i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = PlayerPrefs.GetInt("checkPoint" + i) > 0;
        }
    }
    public void PlayButton(int index)
    {
        foreach (var t in panels)
        {
            t.SetActive(false);
        }

        panels[index].SetActive(true);
    }
    public void LevelController(int level)
    {
        switch (level)
        {
            case 1:
                SceneManager.LoadScene("level_1");
                break;
            case 2:
                SceneManager.LoadScene("level_2");
                break;
            case 3:
                SceneManager.LoadScene("level_3");
                break;

        }
    }
    public void CheckPointController(int index)
    {
        PlayerPrefs.SetInt("clickPoint", index);
    }

    public GameObject levelPanel;

    public void Exit()
    {
        levelPanel.SetActive(false);
    }

    public void Menu()
    {
        levelPanel.SetActive(true);
        ButtonsController();
    }
}
