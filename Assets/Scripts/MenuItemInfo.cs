using UnityEngine;

namespace FloatingUI
{
	/// <summary>
	/// Contains all the information for a menu entry and details panel view.
	/// </summary>
	public class MenuItemInfo : MonoBehaviour 
	{
		#region PUBLIC VARIABLES
		/// The name that will be displayed in the menu items list.
		public string m_ListEntryName;

		/// An item to instantiate in the details view.
		public GameObject m_DetailsObject;

		/// The name displayed as the head of this item in the details view.
		public string m_DetailsHeader;
		/// A description of the item.
		[TextArea(3, 100)]
		public string m_DetailsBody;
		#endregion


		#region PUBLIC API
		/// <summary>
		/// Sets all the values of this instance by deep copying the provided <see cref="MenuItemInfo"/> object.
		/// </summary>
		/// <param name="item">Item.</param>
		public void SetMenuItem(MenuItemInfo item)
		{
			m_ListEntryName = item.m_ListEntryName;

			m_DetailsObject = item.m_DetailsObject;

			m_DetailsHeader = item.m_DetailsHeader;
			m_DetailsBody = item.m_DetailsBody;
		}
		#endregion


		#region OVERRIDES
		public override bool Equals(System.Object obj)
		{
			if (obj == null)
			{
				return false;
			}

			MenuItemInfo info = obj as MenuItemInfo;
			if (info == null)
			{
				return false;
			}
		
			return m_ListEntryName == info.m_ListEntryName &&
				m_DetailsBody == info.m_DetailsBody &&
				m_DetailsHeader == info.m_DetailsHeader;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public static bool operator ==(MenuItemInfo a, MenuItemInfo b)
		{
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}

			if ((object)a == null || (object)b == null)
			{
				return false;
			}

			return a.m_ListEntryName == b.m_ListEntryName &&
				a.m_DetailsBody == b.m_DetailsBody &&
				a.m_DetailsHeader == b.m_DetailsHeader;
		}

		public static bool operator !=(MenuItemInfo a, MenuItemInfo b)
		{
			return !(a == b);
		}
		#endregion
	}
}