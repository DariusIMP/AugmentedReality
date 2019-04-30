using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PeralteScripts {
		

	public class PeralteManager : MonoBehaviour {
		public float masa;
		public float velocidad;
		public float mu;

		// Prefab de diagrama de cuerpo libre.
		public GameObject FBDObject;

		public GameObject mcuViewPrefab;
		private GameObject mcu;
		private MCUView mcuView;

		public GameObject sinRozamientoPrefab;
		private GameObject sinRozamientoObject;
		private PeralteSinRozamientoView sinRozamientoView;

		public GameObject vMaxPrefab;
		private GameObject vMaxObject;
		private PeralteConRozamientoVMaxView vMaxView;

		public GameObject vMinPrefab;
		private GameObject vMinObject;
		private PeralteConRozamientoVMinView vMinView;

		public PeralteView currentView { get; private set; }
		public GameObject currentObject { get; private set; }

		public void Start() {
			mcu = instantiateView (mcuViewPrefab);
			mcuView = mcu.GetComponent<MCUView> ();
			mcuView.velocidad = velocidad;

			sinRozamientoObject = instantiateFBDView<PeralteSinRozamientoView>(sinRozamientoPrefab);
			sinRozamientoView = sinRozamientoObject.GetComponent<PeralteSinRozamientoView> ();

			vMaxObject = instantiateFBDView<PeralteConRozamientoVMaxView>(vMaxPrefab);
			vMaxView = vMaxObject.GetComponent<PeralteConRozamientoVMaxView> ();

			vMinObject = instantiateFBDView<PeralteConRozamientoVMinView>(vMinPrefab);
			vMinView = vMinObject.GetComponent<PeralteConRozamientoVMinView> ();

			currentView = mcuView;
			currentObject = mcu;
		}

		protected GameObject instantiateView(GameObject viewObject) {
			GameObject v = Instantiate(viewObject, new Vector3 (0f, 0f, 0f), Quaternion.identity);
			v.transform.SetParent(this.transform, false);
			return v;
		}

		private GameObject instantiateFBDView<T>(GameObject prefab) where T : FBDView {
			GameObject v = instantiateView (prefab);
			FBDView view = v.GetComponent<T> ();
			view.masa = masa;
			view.velocidad = velocidad;
			view.mu = mu;
			view.FBDObject = FBDObject;
			return v;
		}

		public void switchView(view p) {
			currentObject.SetActive (false);
			currentView.setActive (false);
			switch (p) {
			case view.mcu:
				currentObject = mcu;
				currentView = mcuView;
				break;
			case view.sinRozamiento:
				currentObject = sinRozamientoObject;
				currentView = sinRozamientoView;
				break;
			case view.conRozamientoVMin:
				currentObject = vMinObject;
				currentView = vMinView;
				break;
			case view.conRozamientoVMax:
				currentObject = vMaxObject;
				currentView = vMaxView;
				break;
			default:
				Debug.LogError ("Error: tipo de vista no reconocido por switch");
				break;
			}
			currentObject.SetActive (true);
			currentView.setActive (true);
		}

		public void switchFBD(Fbd f) {
			((FBDView)currentView).SwitchFBD (f);
		}

		public void hideFBD() {
			((FBDView)currentView).setActive (false);
		}

		public enum view
		{
			mcu,
			sinRozamiento,
			conRozamientoVMax,
			conRozamientoVMin
		}

	}


}