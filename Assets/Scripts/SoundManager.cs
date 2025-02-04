using UnityEngine;

namespace SnakeGame
{
	public class SoundManager : MonoBehaviour
	{
		[Header("Sources")]
		[SerializeField] private AudioSource _efxSource1;
		[SerializeField] private AudioSource _efxSource2;
	
		[Header("Clips")]
		[SerializeField] private AudioClip _gameOverSound;
		[SerializeField] private AudioClip _eatFoodSound;
		[SerializeField] private AudioClip _moveSound;

		private void Awake()
		{
			EventBus.SnakeDied.AddListener(() => PlaySound(_efxSource1, _gameOverSound));
			EventBus.FoodEaten.AddListener(() => PlaySound(_efxSource1, _eatFoodSound));
			EventBus.SnakeMoved.AddListener(() => PlaySound(_efxSource2, _moveSound));
		}

		private void PlaySound(AudioSource source, AudioClip clip)
		{
			float randomPitch = Random.Range(0.95f, 1.05f);
			source.pitch = randomPitch;
			source.clip = clip;
			source.PlayOneShot(clip);	
		}
	}
}
