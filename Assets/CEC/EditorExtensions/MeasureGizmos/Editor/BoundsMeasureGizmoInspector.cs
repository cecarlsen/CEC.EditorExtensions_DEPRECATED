//
// Copyright (C) 2017 Carl Emil Carlsen
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CEC.EditorExtensions
{
	[CustomEditor(typeof(BoundsMeasureGizmo))]
	public class BoundsMeasureGizmoInspector : Editor
	{
		BoundsMeasureGizmo component;
		SerializedProperty _always;

		void OnEnable()
		{
			component = target as BoundsMeasureGizmo;
			_always = serializedObject.FindProperty("_always");
		}


		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			EditorGUILayout.PropertyField( _always );

			serializedObject.ApplyModifiedProperties();
		}


		void OnSceneGUI()
		{
			DrawLabels( component.ComputeBounds() );
		}


		// Selected objects are not forwarded to this method.
		[DrawGizmo(GizmoType.NotInSelectionHierarchy)]
		static void RenderCustomGizmo( Transform objectTransform, GizmoType gizmoType )
		{
			BoundsMeasureGizmo component = objectTransform.GetComponent<BoundsMeasureGizmo>();
			if( !component ) return;
			if( !component.always ) return;

			DrawLabels( component.ComputeBounds() );
		}


		static void DrawLabels( Bounds bounds )
		{
			if( bounds.size == Vector3.zero ) return;

			Handles.Label( bounds.min + ( Vector3.right * bounds.extents.x ), bounds.size.x.ToString("F2") + "m" );
			Handles.Label( bounds.min + ( Vector3.up * bounds.extents.y ), bounds.size.y.ToString("F2") + "m" );
			Handles.Label( bounds.min + ( Vector3.forward * bounds.extents.z ), bounds.size.z.ToString("F2") + "m" );
		}
	}
}