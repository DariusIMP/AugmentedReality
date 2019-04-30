using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace Film.CampoRotanteFilm
{
    public abstract class CuadroCampoRotante : Cuadro
    {
        protected CampoRotanteFilm CampoRotanteFilm;
        protected GameObject Jaula;
        protected GameObject IndicadorJaula;
        protected GameObject IndicadorBobina;
        protected GameObject IndicadorTransformador;
        protected GameObject IndicadorCableadoAFuente;
        protected GameObject FlechaCampoMagnetico;
        protected FadingEffects FadingEffects;
        
        protected Text SectionTitle;
        
        public override void Setup()
        {
            
        }

        protected override void Start()
        {
            base.Start();
            
            CampoRotanteFilm = gameObject.GetComponent<CampoRotanteFilm>();
            Jaula = CampoRotanteFilm.Jaula;
            IndicadorJaula = CampoRotanteFilm.IndicadorJaula;
            IndicadorBobina = CampoRotanteFilm.IndicadorBobina;
            IndicadorTransformador = CampoRotanteFilm.IndicadorTransformador;
            FlechaCampoMagnetico = CampoRotanteFilm.FlechaCampoMagnetico;
            IndicadorCableadoAFuente = CampoRotanteFilm.IndicadorCableadoAFuente;
            FadingEffects = CampoRotanteFilm.GetComponent<FadingEffects>();
            SectionTitle = CampoRotanteFilm.SectionTitle;

        }
    }
}