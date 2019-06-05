using UnityEngine;

namespace Viscosidad_Scripts
{
	public class Sphere : MonoBehaviour
	{
		private bool countStarted;
		private Timer timer;

		public GameObject cronometro;
		public Animator animator;
		public Animation animation;
//	
		// Use this for initialization
		void Start ()
		{
			timer = cronometro.GetComponentInChildren<Timer>();
		}

		private void OnTriggerEnter(Collider other)
		{
			countStarted = !countStarted;
			if (countStarted)
			{
				timer.ClickPlay();
			}
			else
			{
				timer.ClickStop();
			}
		}

		public void DisponerEsfera()
		{
			animator.Play("idle");
		}
	
		public void SoltarEsfera()
		{
			animator.Play("EsferaCayendo");	
		}
	}
}
