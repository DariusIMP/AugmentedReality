using UnityEngine;

namespace Viscosidad_Scripts
{
	public class Sphere : MonoBehaviour
	{
		private bool countStarted;
		private Timer timer;

		public GameObject cronometro;
		public Animator animator;
//	
		// Use this for initialization
		void Start ()
		{
			timer = cronometro.GetComponentInChildren<Timer>();
		}

		private void OnTriggerEnter(Collider other)
		{
            Debug.Log("Entro a " + other.gameObject.name);
			countStarted = !countStarted;
			if (countStarted)
			{
                Debug.Log("Count started");
				timer.ClickPlay();
			}
			else
			{
                Debug.Log("Not count started");
				timer.ClickStop();
			}
		}

		public void DisponerEsfera()
		{
			animator.Play("idle");
            countStarted = false;
		}
	
		public void SoltarEsfera()
		{
			animator.Play("EsferaCayendo");	
		}
	}
}
