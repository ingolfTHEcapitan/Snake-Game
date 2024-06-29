using UnityEngine;

public class Menu : MonoBehaviour
{
    private GameObject startMenu;

    void Awake()
    {
        EventManager.snakeDied.AddListener(() => startMenu.SetActive(true));
        startMenu = GameObject.Find("StartMenu");
    }
    
    void Update()
    {
        if (AreKeysPressed(KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D))
        {
            HideStartMenu();
        }
    }

    private void HideStartMenu()
    {
        startMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    // Метод для проверки нажатия любых из переданных клавиш
    private bool AreKeysPressed(params KeyCode[] keys)
    {
        foreach (KeyCode key in keys)
            if (Input.GetKey(key)) return true;
        return false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
