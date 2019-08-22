using System.Collections;
using Campo_Rotante_Scripts;
using UnityEditor;
using UnityEngine;

namespace Film.CampoRotanteFilm
{
    public class Cuadro2 : CuadroCampoRotante
    {
        public override void Play()
        {
            Debug.Log("<color=blue> CampoRotanteCuadroSegundo.play() </color>");
            base.Play();
            if (DialogueManager != null)
            {
                DialogueManager.Play();
            }
            ConfigureScene();

            StartCoroutine(ArrancaARotarLaJaula(12));
        }

        public override void ConfigureScene()
        {
            Jaula.SetActive(true);
            Jaula.GetComponent<Spin>().setSpeed(0f);
            IndicadorJaula.SetActive(true);
            IndicadorBobina.SetActive(true);
            IndicadorTransformador.SetActive(true);
            IndicadorCableadoAFuente.SetActive(true);
            FlechaCampoMagnetico.SetActive(true);
        }

        private IEnumerator ArrancaARotarLaJaula(float secs)
        {
            yield return new WaitForSecondsRealtime(secs);
            float speed = 1;
            while (speed < 150)
            {
                Jaula.GetComponent<Spin>().setSpeed(speed);
                speed = speed * 1.05f;
                yield return null;
            }
        }
    }
}