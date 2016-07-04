using UnityEngine;
using System.Collections;
using DG.Tweening;
using VR = UnityEngine.VR;


namespace Lib.Base {
	public class HeadLib {

		protected string preBoardName;

		public void SetPreBoardName(string aName) {
			preBoardName = aName;
		}

		public string GetPreBoardName() {
			return preBoardName;
		}

		public bool CompareName(string aName) {
			return (preBoardName == aName);
		}
	}

}