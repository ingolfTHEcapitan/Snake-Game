using UnityEngine;

public class SoundManager : MonoBehaviour
{
	private static SoundManager s_instan�e = null;
	[SerializeField] private AudioSource _efxSource1;
	[SerializeField] private AudioSource _efxSource2;

	[SerializeField] private AudioClip _gameOverSound;
	[SerializeField] private AudioClip _eatFoodSound;
	[SerializeField] private AudioClip _moveSound;

	void Awake()
	{
		// ���������� �������� ��������
		if (s_instan�e == null)
			s_instan�e = this;
		else if (s_instan�e != this) 
			Destroy(gameObject);
			
		DontDestroyOnLoad(gameObject);
		
		// �������� �� �������
		EventManager.SnakeDiedEvent.AddListener(() => PlaySound(_efxSource1, _gameOverSound));
		EventManager.Food�atenEvent.AddListener(() => PlaySound(_efxSource1, _eatFoodSound));
		EventManager.SnakeMovedEvent.AddListener(() => PlaySound(_efxSource2, _moveSound));
		
	}

	public void PlaySound(AudioSource source, AudioClip clip)
	{
		float randomPitch = Random.Range(0.95f, 1.05f);
		source.pitch = randomPitch;
		source.clip = clip;
		source.Play();	
	}
}
