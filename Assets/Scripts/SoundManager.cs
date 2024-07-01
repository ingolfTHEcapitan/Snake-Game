using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instanсe = null;
	public AudioSource efxSource1;
	public AudioSource efxSource2;

	public AudioClip gameOverSound;
	public AudioClip eatFoodSound;
	public AudioClip moveSound;


	void Awake()
	{
		//Проверьяем, существует ли уже экземпляр SoundManager
		if (instanсe == null)
			// Если нет делаем текщий экземпляр основным
			instanсe = this;
		else if (instanсe != this) // Если существует
			Destroy(gameObject); // Удаляем, реализирует принцип Синглтон, точто что экземпляр класса может быть только один
		DontDestroyOnLoad(gameObject);
		
		EventManager.snakeDied.AddListener(() => PlaySound(efxSource1, gameOverSound));
		EventManager.foodIsЕaten.AddListener(() => PlaySound(efxSource1, eatFoodSound));
		EventManager.snakeIsMoving.AddListener(() => PlaySound(efxSource2, moveSound));
		
	}

	public void PlaySound(AudioSource source, AudioClip clip)
	{
		float randomPitch = Random.Range(0.95f, 1.05f);
		source.pitch = randomPitch;
		source.clip = clip;
		source.Play();
		
	}
}
