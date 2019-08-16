using UnityEngine;

namespace Viscosidad_Scripts
{
	public class Sphere : MonoBehaviour
	{
		private bool countStarted;
		private Timer virtualTimer, realTimer;

		public GameObject CronometroVirtual, CronometroReal;
		public Animator animator;
//	
		// Use this for initialization
		void Start ()
		{
			virtualTimer = CronometroVirtual.GetComponentInChildren<Timer>();
            realTimer = CronometroReal.GetComponentInChildren<Timer>();
		}

		private void OnTriggerEnter(Collider other)
		{
            Debug.Log("Entro a " + other.gameObject.name);
			countStarted = !countStarted;
			if (countStarted)
			{
                Debug.Log("Count started");
				virtualTimer.ClickPlay();
			}
			else
			{
                Debug.Log("Not count started");
				virtualTimer.ClickStop();
			}
		}

		public void DisponerEsfera()
		{
			animator.Play("idle");
            countStarted = false;
            GetComponent<FluxController>().dispose();
		}
	
		public void SoltarEsfera()
		{
			animator.Play("EsferaCayendo");	
		}

        public void TocoFondo()
        {
            realTimer.ClickStop();
        }
	}
}
