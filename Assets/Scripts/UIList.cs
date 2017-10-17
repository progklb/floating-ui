using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;

namespace FloatingUI
{
	/// <summary>
	/// Provides the ability to add and remove entries from a list in 3D space.
	/// </summary>
	public class UIList : MonoBehaviour 
	{
		#region PUBLIC PROPERTIES
		public bool initialised { get; private set; }
		public List<MenuItem> menuItems { get; private set; }
		#endregion


		#region PUBLIC VARIABLES
		/// A 3D padding value that is used to offset each successive item.
		public Vector3 m_Padding;
		#endregion


		#region PRIVATE VARIABLES
		/// A prefab of the entry that will be used to populate this list.
		[SerializeField]
		private GameObject m_MenuItemPrefab;
		#endregion


		#region UNITY EVENTS
		void Awake()
		{
			menuItems = new List<MenuItem>();

			Cache();
			Realign();

			initialised = true;
		}
		#endregion


		#region PUBLIC API
		/// <summary>
		/// Adds a new entry to the list. This will automatically position the item at the correct location.
		/// </summary>
		/// <param name="item">Template of the menu item to spawn.</param>
		public void AddItem(MenuItemInfo info)
		{
			// Spawn pos is this transforms position, and then offset this according to the padding and number of entries. Use rotation of parent.
			var entry = Instantiate(m_MenuItemPrefab, transform, false);
			entry.transform.localPosition = entry.transform.localPosition + (m_Padding * menuItems.Count);

			var item = entry.GetComponent<MenuItem>();
			item.SetInfo(info);

			menuItems.Add(item);
		}

		/// <summary>
		/// Removes the item from the list that matches the provided template item. (Note that this need to be a direct reference!)
		/// </summary>
		/// <param name="item">Item template to remove.</param>
		public void RemoveItem(MenuItemInfo info)
		{
			for (int i = 0; i < transform.childCount; ++i)
			{
				var child = transform.GetChild(i).GetComponent<MenuItem>();
				if (child.info == info)
				{
					Destroy(child.gameObject);
					menuItems.RemoveAt(i);
					break;
				}
			}
		}

		/// <summary>
		/// Clears the list of all its entries.
		/// </summary>
		public void ClearItems()
		{
			for (int i = transform.childCount -1; i >= 0; --i)
			{
				Destroy(transform.GetChild(i).gameObject);
			}

			menuItems.Clear();
		}

		/// <summary>
		/// Caches all <see cref="MenuItem"/> entries in this controllers immediate children.
		/// </summary>
		public void Cache()
		{
			menuItems.Clear();

			for (int i = 0; i < transform.childCount; ++i)
			{
				var item = transform.GetChild(i).GetComponent<MenuItem>();
				menuItems.Add(item);
			}
		}

		/// <summary>
		/// Repositions all menu item entries, considering the current padding value.
		/// </summary>
		public void Realign()
		{
			for (int i = 0; i < transform.childCount; ++i)
			{
				var item = transform.GetChild(i);
				item.position = transform.position + (m_Padding * i);
			}
		}
		#endregion
	}
}