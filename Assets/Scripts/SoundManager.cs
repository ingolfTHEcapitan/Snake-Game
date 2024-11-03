using UnityEngine;

public class SoundManager : MonoBehaviour
{
	[Header("Sources")]
	[SerializeField] private AudioSource _efxSource1;
	[SerializeField] private AudioSource _efxSource2;
	
	[Header("Clips")]
	[SerializeField] private AudioClip _gameOverSound;
	[SerializeField] private AudioClip _eatFoodSound;
	[SerializeField] private AudioClip _moveSound;

	void Awake()
	{
		GameEvents.SnakeDied.AddListener(() => PlaySound(_efxSource1, _gameOverSound));
		GameEvents.FoodÅaten.AddListener(() => PlaySound(_efxSource1, _eatFoodSound));
		GameEvents.SnakeMoved.AddListener(() => PlaySound(_efxSource2, _moveSound));
	}

	public void PlaySound(AudioSource source, AudioClip clip)
	{
		float randomPitch = Random.Range(0.95f, 1.05f);
		source.pitch = randomPitch;
		source.clip = clip;
		source.Play();	
	}
}
