using UnityEngine;

public class Menu : MonoBehaviour
{
	private GameObject _startMenu;

	void Awake()
	{
		_startMenu = GameObject.Find("StartMenu");
		EventManager.SnakeDiedEvent.AddListener(() => _startMenu.SetActive(true));
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
		_startMenu.SetActive(false);
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
	
	// Функция переключения полноэкранного режима
    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
