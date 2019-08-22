using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Film.Peralte_Film
{
    public abstract class CuadroPeralte : Cuadro
    {
        protected PeralteAnimationController PeralteAnimationController;

        protected PeralteFilm PeralteFilm;
        
        //Holograma
        protected GameObject Holograma;
        protected GameObject Diagrama2D;
        protected GameObject Peso;
        protected GameObject Normal;
        
        protected GameObject FormulaSRoz1;
        protected GameObject FormulaSRoz2;
        protected GameObject FormulaSRoz3;
        protected GameObject FormulaSRoz4;
        protected GameObject FormulaSRoz5;
        
        protected GameObject FormulaVMax0;
        protected GameObject FormulaVMax1;
        protected GameObject FormulaVMax2;
        protected GameObject FormulaVMax3;
        protected GameObject FormulaVMax4;
        protected GameObject FormulaVMax5;
        
        protected GameObject FormulaVMin0;
        protected GameObject FormulaVMin1;
        protected GameObject FormulaVMin2;
        protected GameObject FormulaVMin3;
        protected GameObject FormulaVMin4;
        protected GameObject FormulaVMin5;
        
        protected GameObject RozVminY;
        protected GameObject RozVmin;
        protected GameObject RozVminX;
        protected GameObject RozVmaxY;
        protected GameObject RozVmaxX;
        protected GameObject RozVmax;
        
        protected GameObject NormalY;
        protected GameObject NormalX;
        protected GameObject PlanoCartesiano;
        protected GameObject Auto_Holograma;
        protected GameObject Pista;
        
        
        //protected PeralteManager PeralteManager;
        protected Text SectionTitle;
        protected FadingEffects FadingEffects;

        protected enum TexturasPista
        {
            Ruta,
            Hielo
        }
   
        protected override void Start()
        {
            base.Start();
            PeralteAnimationController = gameObject.GetComponent<PeralteAnimationController>();

            PeralteFilm = gameObject.GetComponent<PeralteFilm>();

            Holograma = PeralteFilm.Holograma;
            Diagrama2D = PeralteFilm.Diagrama2D;
            Peso = PeralteFilm.Peso;
            Normal = PeralteFilm.Normal;
           
            FormulaSRoz1 = PeralteFilm.FormulaSRoz1;
            FormulaSRoz2 = PeralteFilm.FormulaSRoz2;
            FormulaSRoz3 = PeralteFilm.FormulaSRoz3;
            FormulaSRoz4 = PeralteFilm.FormulaSRoz4;
            FormulaSRoz5 = PeralteFilm.FormulaSRoz5;
            
            FormulaVMax0 = PeralteFilm.FormulaVMax0;
            FormulaVMax1 = PeralteFilm.FormulaVMax1;
            FormulaVMax2 = PeralteFilm.FormulaVMax2;
            FormulaVMax3 = PeralteFilm.FormulaVMax3;
            FormulaVMax4 = PeralteFilm.FormulaVMax4;
            FormulaVMax5 = PeralteFilm.FormulaVMax5;
            
            FormulaVMin0 = PeralteFilm.FormulaVMin0;
            FormulaVMin1 = PeralteFilm.FormulaVMin1;
            FormulaVMin2 = PeralteFilm.FormulaVMin2;
            FormulaVMin3 = PeralteFilm.FormulaVMin3;
            FormulaVMin4 = PeralteFilm.FormulaVMin4;
            FormulaVMin5 = PeralteFilm.FormulaVMin5;
            
            RozVminY = PeralteFilm.RozVminY;
            RozVmin  = PeralteFilm.RozVmin ;
            RozVminX = PeralteFilm.RozVminX;
            RozVmaxY = PeralteFilm.RozVmaxY;
            RozVmaxX = PeralteFilm.RozVmaxX;
            RozVmax  = PeralteFilm.RozVmax ;
            
            NormalX = PeralteFilm.NormalX;
            NormalY = PeralteFilm.NormalY;
            PlanoCartesiano = PeralteFilm.PlanoCartesiano;
            Auto_Holograma = PeralteFilm.Auto_Holograma;
            Pista = PeralteFilm.Pista;
            //PeralteManager = PeralteFilm.PeralteManager.GetComponent<PeralteManager>();
            SectionTitle = PeralteFilm.SectionTitle;
            FadingEffects = Diagrama2D.GetComponent<FadingEffects>();
    
        }

        public abstract override void ConfigureScene();
        
		protected void CambiarTexturaPistaAHielo()
		{
			Pista.GetComponent<ChangeMaterial>().ChangeToMaterial((uint)TexturasPista.Hielo);
		}

		protected void CambiarTexturaPistaARuta()
		{
			Pista.GetComponent<ChangeMaterial>().ChangeToMaterial((uint)TexturasPista.Ruta);
		}

    }
}