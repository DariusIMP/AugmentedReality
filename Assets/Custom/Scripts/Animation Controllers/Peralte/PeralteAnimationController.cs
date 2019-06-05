using UnityEngine;


public class PeralteAnimationController : MonoBehaviour
{
	public static PeralteAnimationController Instance { get; private set; }

	private Animator _animator;

	public void Ajustar_Velocidad(float velocidad)
	{
		_animator.speed = velocidad;
	}	   
	
	void Start()
	{
		_animator = GetComponent<Animator>();
	}

}

