using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloatingUI
{
	/// <summary>
	/// Assigns a random texture at start up.
	/// </summary>
	public class RandomMaterialAssigner : MonoBehaviour 
	{
		#region VARIABLES
		public MeshRenderer m_Renderer;
		public Material[] m_Materials;
		#endregion


		#region UNITY EVENTS
		void Start () 
		{
			m_Renderer.material = m_Materials[Random.Range(0, m_Materials.Length)];
		}
		#endregion
	}
}
