using UnityEngine;
using UnityEngine.UI;

namespace Dialogues
{
	public class DialogueManager : MonoBehaviour
	{
		private AudioSource _audioSource;
		private SubtitleDisplayer _subtitleDisplayer;

		public Text Text;
		public Text Text2;
		[Range(0, 1)]
		public float FadeTime;

		public TextAsset Subtitle;
		public AudioClip AudioClip;

		public static DialogueManager Instance { get; private set; }

		private void Start()
		{
			_audioSource = gameObject.AddComponent<AudioSource>();
			_subtitleDisplayer = gameObject.AddComponent<SubtitleDisplayer>();
		}

		public void Play()
		{
			// Configuración de Audio.
			_audioSource.Stop();
			_audioSource.clip = AudioClip;

			// Configuración de subtítulos.
			_subtitleDisplayer.Subtitle = Subtitle;
			_subtitleDisplayer.Text = Text;
			_subtitleDisplayer.Text2 = Text2;
			_subtitleDisplayer.FadeTime = FadeTime;

			// Dispara el audio
			_audioSource.Play();

			// Dispara los subtítulos y elimina los anteriores si los hubiera.
			RestartSubtitles();
		}

		public void Stop() {
			_audioSource.Stop ();
			// Para borrar los subtítulos se los reemplaza con un nuevo set vacío.
			_subtitleDisplayer.Subtitle = new TextAsset("");
			RestartSubtitles ();
		}

		public void TurnAudioOff()
		{
			_audioSource.volume = 0f;
		}

		public void TurnAudioOn()
		{
			_audioSource.volume = 1f;
		}
		
		// Detiene los subtítulos en ejecución y pone en ejecución los nuevos.
		private void RestartSubtitles() {
			StopAllCoroutines();
			StartCoroutine(_subtitleDisplayer.Begin());
		}

	}
}
