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
using System.IO;
using UnityEngine;
using UnityEditor;

namespace CEC.EditorExtensions
{
	public static class RenderTextureSave
	{
        enum Encoding { PNG, JPG }

        [MenuItem("CONTEXT/RenderTexture/Save as PNG...")]
		public static void DoRenderTextureSavePNG( MenuCommand menuCommand )
		{
            RenderTexture renderTex = menuCommand.context as RenderTexture;
			Save( renderTex, Encoding.PNG );
		}

        [MenuItem("CONTEXT/RenderTexture/Save as JPG...")]
		public static void DoRenderTextureSaveJPG( MenuCommand menuCommand )
		{
            RenderTexture renderTex = menuCommand.context as RenderTexture;
			Save( renderTex, Encoding.JPG );
		}


        static void Save( RenderTexture renderTex, Encoding encoding )
        {
			string path = EditorUtility.SaveFilePanel( "Save Texture", "Assets/", renderTex.name, encoding.ToString().ToLower() );
			if( string.IsNullOrEmpty( path ) ) return;

			RenderTexture prevRenderTex = RenderTexture.active; 
			RenderTexture.active = renderTex;
			Texture2D tex = new Texture2D( renderTex.width, renderTex.height );
			tex.ReadPixels( new Rect( 0, 0, tex.width, tex.height ), 0, 0 );
			tex.Apply();
			RenderTexture.active = prevRenderTex;

			byte[] bytes;
            switch( encoding ) {
                case Encoding.PNG:
                    bytes = tex.EncodeToPNG();
                    break;
                default:
                    bytes = tex.EncodeToJPG();
                    break;

            }

			File.WriteAllBytes( path, bytes );
        }
	}
}