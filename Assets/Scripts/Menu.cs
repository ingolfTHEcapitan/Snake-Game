using UnityEngine;

namespace SnakeGame
{
	public class Menu : MonoBehaviour
	{
		private GameObject _startMenu;
	
		private readonly KeyCode[] _keys = 
		{
			KeyCode.W, 
			KeyCode.S, 
			KeyCode.D, 
			KeyCode.UpArrow, 
			KeyCode.DownArrow, 
			KeyCode.RightArrow
		};

		private void Awake()
		{
			_startMenu = GameObject.Find("StartMenu");
			GameEvents.SnakeDied.AddListener(() => StartMenuIsAcrive(true, 0.0f));
		}
	
		private void Start() 
		{
			StartMenuIsAcrive(true, 0.0f);
		}


		private void Update()
		{
			if (AreKeysPressed(_keys)) 
			{
				StartMenuIsAcrive(false, 1.0f);
			}
		}

		private void StartMenuIsAcrive(bool isActive, float timeScale)
		{
			_startMenu.SetActive(isActive);
			Time.timeScale = timeScale;
		}

		private bool AreKeysPressed(params KeyCode[] keys)
		{
			//TODO: Использовать LINQ
			foreach (var key in keys)
				if (Input.GetKey(key)) 
					return true;
				
			return false;
		}

		public void Quit() => Application.Quit();
	}
}
