using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Film.CampoRotanteFilm
{
    public class Cuadro1 : CuadroCampoRotante
    {
        public override void Setup() {
            StartCoroutine(AparicionDelTitulo());
        }
        public override void Play()
        {
            Debug.Log("<color=blue> CampoRotanteCuadroInicial.play() </color>");
            base.Play();
            if (DialogueManager != null)
            {
               DialogueManager.Play();
            }
            ConfigureScene();

            StartCoroutine(MostrarIndicador(IndicadorBobina, 2.5f));
            StartCoroutine(MostrarIndicador(IndicadorJaula, 6.5f));
            StartCoroutine(MostrarIndicador(IndicadorTransformador, 11f));
            StartCoroutine(MostrarIndicador(IndicadorCableadoAFuente, 12f));
        }

        public override void ConfigureScene()
        {
            Jaula.SetActive(true);
            IndicadorJaula.SetActive(false);
            IndicadorBobina.SetActive(false);
            FlechaCampoMagnetico.SetActive(false);
            IndicadorTransformador.SetActive(false);
            IndicadorCableadoAFuente.SetActive(false);        }

        private IEnumerator AparicionDelTitulo()
        {
            SectionTitle.text = "Experiencia de campo rotante";
            yield return StartCoroutine(FadingEffects.ShowAndHideTextFading(0.5f, 1f, SectionTitle));
        }

        private IEnumerator MostrarIndicador(GameObject indicador, float secs)
        {
            yield return new WaitForSecondsRealtime(secs);
            indicador.SetActive(true);
        }
    }
}