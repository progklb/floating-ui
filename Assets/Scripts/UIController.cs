using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloatingUI
{
	/// <summary>
	/// Stores the menu items to instantiate
	/// </summary>
	public class UIController : MonoBehaviour 
	{
		#region CONSTANTS
		/// The camera animator bool name for toggling details view on and off.
		private const string CAMERA_DETAILS_BOOL = "Details";
		#endregion


		#region TYPES
		/// <summary>
		/// The different view states that the UI can be in.
		/// </summary>
		private enum View
		{
			List,
			Details
		}
		#endregion


		#region PUBLIC PROPERTIES
		/// The currently selected item in the list view.
		public MenuItem currentItem { get { return m_List.menuItems[m_CurrentIndex]; } }
		#endregion


		#region PUBLIC VARIABLES
		/// A delay to wait between each successive spawn of a menu item.
		public float m_ListPopulateDelay = 0.1f;
		#endregion


		#region PRIVATE VARIABLES
		/// The controller for the menu list.
		[SerializeField] private UIList m_List;
		/// The controller for the details view.
		[SerializeField] private UIDetails m_Details;
		/// The camera animator.
		[SerializeField] private Animator m_CameraAnim;

		/// The index of the currently selected item in the list view.
		private int m_CurrentIndex;
		/// The current view state of the UI.
		private View m_View;
		#endregion


		#region UNITY EVENTS
		void OnEnable()
		{
			m_View = View.List;
			StartCoroutine(PopulateListAsync());
		}

		void OnDisable()
		{
			m_List.ClearItems();
		}

		void Update()
		{
			switch (m_View)
			{
				case View.List:
					ProcessListInput();
					break;
				case View.Details:
					ProcessDetailsInput();
					break;
			}
		}
		#endregion


		#region LIST VIEW
		void ProcessListInput()
		{
			int lastIdx = -1;

			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				lastIdx = m_CurrentIndex;
				m_CurrentIndex = Repeat(++m_CurrentIndex, m_List.menuItems.Count - 1);
			}
			else if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				lastIdx = m_CurrentIndex;
				m_CurrentIndex = Repeat(--m_CurrentIndex, m_List.menuItems.Count - 1);
			}

			// Was input recieved? Update list highlight.
			if (lastIdx != -1)
			{
				m_List.menuItems[lastIdx].SetHighlighted(false);
				currentItem.SetHighlighted(true);
			}

			// If an item is selected, assign the details panels and initiate the view transition.
			if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
			{
				currentItem.TriggerSelected();
				m_Details.ShowDetails(currentItem);

				m_CameraAnim.SetBool(CAMERA_DETAILS_BOOL, true);

				m_View = View.Details;
			}
		}

		void ProcessDetailsInput()
		{
			// If we are going back, intiate the transition back to list view.
			if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Escape))
			{
				m_CameraAnim.SetBool(CAMERA_DETAILS_BOOL, false);

				m_View = View.List;
			}

			// Listen for input that allows scrolling of the text
			if (Input.GetKey(KeyCode.DownArrow))
			{
				m_Details.Scroll(1f);
			}
			else if (Input.GetKey(KeyCode.UpArrow))
			{
				m_Details.Scroll(-1f);
			}
		}
		#endregion


		#region HELPERS
		int Repeat(int value, int length)
		{
			if (value > length)
			{
				value = 0;
			}
			else if (value < 0)
			{
				value = length;
			}

			return value;
		}

		IEnumerator PopulateListAsync()
		{
			yield return new WaitUntil(()=> m_List.initialised);

			// Load all menu item prefabs
			var menuItems = Resources.LoadAll<MenuItemInfo>("MenuItems");
			m_List.ClearItems();

			foreach (var item in menuItems)
			{
				m_List.AddItem(item);
				yield return new WaitForSeconds(m_ListPopulateDelay);
			}

			m_CurrentIndex = 0;
			currentItem.SetHighlighted(true);
		}
		#endregion
	}
}