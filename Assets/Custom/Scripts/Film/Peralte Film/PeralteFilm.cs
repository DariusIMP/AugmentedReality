using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

namespace Film.Peralte_Film
{
    public class PeralteFilm : global::Custom.Scripts.Film.Film
    {        
        protected PeralteAnimationController PeralteAnimationController;
        
        public GameObject Holograma;
        public GameObject Diagrama2D;
        public GameObject Peso;
        public GameObject Normal;
        
        public GameObject FormulaSRoz1;
        public GameObject FormulaSRoz2;
        public GameObject FormulaSRoz3;
        public GameObject FormulaSRoz4;
        public GameObject FormulaSRoz5;
        
        public GameObject FormulaVMax0;
        public GameObject FormulaVMax1;
        public GameObject FormulaVMax2;
        public GameObject FormulaVMax3;
        public GameObject FormulaVMax4;
        public GameObject FormulaVMax5;
        
        public GameObject FormulaVMin0;
        public GameObject FormulaVMin1;
        public GameObject FormulaVMin2;
        public GameObject FormulaVMin3;
        public GameObject FormulaVMin4;
        public GameObject FormulaVMin5;

        public GameObject RozVminY;
        public GameObject RozVmin;
        public GameObject RozVminX;
        public GameObject RozVmaxY;
        public GameObject RozVmaxX;
        public GameObject RozVmax;
        
        public GameObject NormalY;
        public GameObject NormalX;
        public GameObject Auto_Holograma;
        public GameObject PeralteManager;
        public GameObject Pista;
        public GameObject PlanoCartesiano;
        public GameObject FormulasSRoz;
        public GameObject FormulasVMax;
        public GameObject FormulasVMin;
        
        
        public Text SectionTitle;
        
        protected override void Start()
        {
            Debug.Log(name + ": Start");
            base.Start();
            PeralteAnimationController = gameObject.AddComponent<PeralteAnimationController>();
        }

        
    }
}