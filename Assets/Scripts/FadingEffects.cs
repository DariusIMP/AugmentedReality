using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

public class FadingEffects : MonoBehaviour
{
	
	public IEnumerator ShowImageFading(float fadingTime, Image imagen)
	{
		imagen.color = new Color(imagen.color.r, 
			imagen.color.g, imagen.color.b, 0);
		while (imagen.color.a < 1.0f)
		{
			imagen.color = new Color(imagen.color.r, 
				imagen.color.g, imagen.color.b, 
				imagen.color.a + (Time.deltaTime / fadingTime));
			yield return null;
		}
	}
 
	public IEnumerator HideImageFading(float fadingTime, Image imagen)
	{
		imagen.color = new Color(imagen.color.r, 
			imagen.color.g, imagen.color.b, 1);
		while (imagen.color.a > 0.0f)
		{
			imagen.color = new Color(imagen.color.r, 
				imagen.color.g, imagen.color.b, 
				imagen.color.a - (Time.deltaTime / fadingTime));
			yield return null;
		}
	}
	
	public IEnumerator ShowAndHideTextFading(float fadingTime, float holdingTime, Text text)
	{
		text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
		while (text.color.a < 1.0f)
		{
			text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / fadingTime));
			yield return null;
		}

		yield return new WaitForSecondsRealtime(holdingTime);
	
		yield return StartCoroutine(HideTextFading(fadingTime, text));
	}
	
	public IEnumerator ShowTextFading(float fadingTime, Text text)
	{
		text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
		while (text.color.a < 1.0f)
		{
			text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / fadingTime));
			yield return null;
		}
	}
	
	public IEnumerator HideTextFading(float fadingTime, Text text)
	{
		text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
		while (text.color.a > 0.0f)
		{
			text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / fadingTime));
			yield return null;
		}
	}

	
}