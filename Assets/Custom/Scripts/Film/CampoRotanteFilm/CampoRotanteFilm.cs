using UnityEngine;
using UnityEngine.UI;

namespace Film.CampoRotanteFilm
{
    public class CampoRotanteFilm : Custom.Scripts.Film.Film
    {
        public GameObject Jaula;
        public GameObject IndicadorJaula;
        public GameObject IndicadorBobina;
        public GameObject IndicadorTransformador;
        public GameObject IndicadorCableadoAFuente;
        public GameObject FlechaCampoMagnetico;
        
        public Text SectionTitle;
        
        protected override void Start()
        {
            Debug.Log(name + ": Start");
            base.Start();
        }
    }
}