using UnityEngine;
using UnityEngine.UI;

namespace FloatingUI
{
	/// <summary>
	/// Represents a menu item.
	/// </summary>
	public class MenuItem : MonoBehaviour 
	{
		#region CONSTANTS
		private const string HIGHLIGHTED_BOOL = "Highlighted";
		private const string SELECTED_TRIGGER = "Selected";
		#endregion


		#region PUBLIC PROPERTIES
		/// An data structure that holds all the necessary info to display for this item when it is selected.
		public MenuItemInfo info { get { return m_Info; } }
		#endregion


		#region PRIVATE VARIABLES
		/// The animator for this menu entry.
		[SerializeField] private Animator m_Animator;
		/// The text displayed for this menu entry.
		[SerializeField] private Text m_Text;

		[Space(10)]
		[SerializeField] private MenuItemInfo m_Info;
		#endregion


		#region PUBLIC API
		/// <summary>
		/// Sets this menu item with the info in the provided template.
		/// </summary>
		/// <param name="info">Info.</param>
		public void SetInfo(MenuItemInfo info)
		{
			m_Info.SetMenuItem(info);
			m_Text.text = m_Info.m_ListEntryName; 
		}

		public void SetHighlighted(bool highlighted)
		{
			m_Animator.SetBool(HIGHLIGHTED_BOOL, highlighted);
		}

		public void TriggerSelected()
		{
			m_Animator.SetTrigger(SELECTED_TRIGGER);
		}
		#endregion
	}
}