using UnityEngine;
using UnityEngine.UI;

namespace FloatingUI
{
	/// <summary>
	/// Sets up the details panels of the UI.
	/// </summary>
	public class UIDetails : MonoBehaviour 
	{
		#region VARIABLES
		/// The parent object underwhich to instantiate details objects when showing the details of a menu item.
		[SerializeField] private Transform m_DetailsObjectParent;

		/// The header of the details panel text.
		[SerializeField] private Text m_DetailsHeaderText;
		/// The body of the details panel text.
		[SerializeField] private Text m_DetailsBodyText;

		/// The size of each scroll step when input it provided.
		[Space(10)]
		[SerializeField] private float m_ScrollStepSize = 0.01f;
		/// The maximum allowable scroll offset.
		[SerializeField] private float m_LowerScrollBound = 2f;

		/// The starting offset of the details body text.
		private Vector2 m_BaseScrollOffset;
		/// Additional offset creating by scrolling.
		public float m_TargetAdditionalOffset;
		#endregion


		#region UNITY EVENTS
		void Start()
		{
			// Save the initial offset
			m_BaseScrollOffset = m_DetailsBodyText.rectTransform.offsetMax;
		}

		void Update()
		{
			// Lerp toward our target offset
			m_DetailsBodyText.rectTransform.offsetMax = m_BaseScrollOffset + new Vector2(0f, m_TargetAdditionalOffset);
		}
		#endregion


		#region PUBLIC API
		public void ShowDetails(MenuItem item)
		{
			ClearDetails();

			if (item.info.m_DetailsObject != null)
			{
				Instantiate(item.info.m_DetailsObject, m_DetailsObjectParent, false);
			}

			m_DetailsHeaderText.text = item.info.m_DetailsHeader;
			m_DetailsBodyText.text = item.info.m_DetailsBody;

			m_TargetAdditionalOffset = 0f;
		}

		public void ClearDetails()
		{
			// Clear existing objects
			for (int i = m_DetailsObjectParent.transform.childCount -1; i >= 0; --i)
			{
				Destroy(m_DetailsObjectParent.transform.GetChild(i).gameObject);
			}

			m_DetailsHeaderText.text = string.Empty;
			m_DetailsBodyText.text = string.Empty;
		}

		/// <summary>
		/// Adjusts the target scroll position. Negative values scroll downwards and positive values scroll upwards.
		/// This clamps scrolling to the top of the text body and a lower bound specified by <see cref="m_LowerScrollBound"/>
		/// </summary>
		/// <param name="dir">Direction in which to scroll.</param>
		public void Scroll(float dir)
		{
			m_TargetAdditionalOffset += Mathf.Sign(dir) * m_ScrollStepSize;
			m_TargetAdditionalOffset = Mathf.Clamp(m_TargetAdditionalOffset, 0f, m_LowerScrollBound);
		}
		#endregion
	}
}