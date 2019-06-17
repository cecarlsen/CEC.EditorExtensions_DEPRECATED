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
	[CustomEditor(typeof(DistanceToMeasureGizmo))]
    public class DistanceToMeasureGizmoInspector : Editor   
	{
		DistanceToMeasureGizmo component;
		SerializedProperty _always;

		void OnEnable()
		{
			component = target as DistanceToMeasureGizmo;
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
			DrawLabel( component.transform, component.target );
		}


		// Selected objects are not forwarded to this method.
		[DrawGizmo(GizmoType.NotInSelectionHierarchy)]
		static void RenderCustomGizmo( Transform objectTransform, GizmoType gizmoType )
		{
			DistanceToMeasureGizmo component = objectTransform.GetComponent<DistanceToMeasureGizmo>();
			if( !component ) return;
			if( !component.always ) return;

			DrawLabel( component.transform, component.target );
		}


		static void DrawLabel( Transform a, Transform b )
		{
			if( !a || !b ) return;
			Vector3 towardsB = b.position - a.position;
			Handles.Label( a.position + towardsB * 0.5f, towardsB.magnitude.ToString("F2") + "m" );
		}
	}
}