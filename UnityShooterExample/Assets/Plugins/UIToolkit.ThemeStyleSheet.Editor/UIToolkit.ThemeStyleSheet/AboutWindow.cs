#nullable enable
namespace UIToolkit.ThemeStyleSheet {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public class AboutWindow : EditorWindow {

        [MenuItem( "Tools/UIToolkit Theme Style Sheet/About UIToolkit Theme Style Sheet" )]
        public new static void Show() {
            var window = GetWindow<AboutWindow>( true, "About UIToolkit Theme Style Sheet", true );
            window.minSize = window.maxSize = new Vector2( 800, 600 );
        }

        public void OnGUI() {
            using (new GUILayout.VerticalScope( EditorStyles.helpBox )) {
                {
                    EditorGUILayout.LabelField( "Overview", EditorStyles.boldLabel );
                    EditorGUILayout.LabelField( "The UIToolkit theme style sheet." );
                }
                EditorGUILayout.Separator();
                {
                    EditorGUILayout.LabelField( "Links", EditorStyles.boldLabel );
                    if (EditorGUILayout.LinkButton( "denis535.github.io" )) Application.OpenURL( "https://denis535.github.io" );
                    if (EditorGUILayout.LinkButton( "github.com (Unity Shooter Example)" )) Application.OpenURL( "https://github.com/Denis535/UnityShooterExample" );
                    if (EditorGUILayout.LinkButton( "github.com (Unity Framework)" )) Application.OpenURL( "https://github.com/Denis535/UnityFramework" );
                    EditorGUILayout.Space( 2f );
                    if (EditorGUILayout.LinkButton( "nuget.org" )) Application.OpenURL( "https://www.nuget.org/profiles/Denis535" );
                    if (EditorGUILayout.LinkButton( "openupm.com" )) Application.OpenURL( "https://openupm.com/packages/?sort=downloads&q=denis535" );
                    EditorGUILayout.Space( 2f );
                    if (EditorGUILayout.LinkButton( "fab.com" )) Application.OpenURL( "https://www.fab.com/sellers/Denis535" );
                    if (EditorGUILayout.LinkButton( "assetstore.unity.com" )) Application.OpenURL( "https://assetstore.unity.com/publishers/90787" );
                    EditorGUILayout.Space( 2f );
                    if (EditorGUILayout.LinkButton( "youtube.com" )) Application.OpenURL( "https://www.youtube.com/channel/UCLFdZl0pFkCkHpDWmodBUFg" );
                    if (EditorGUILayout.LinkButton( "udemy.com" )) Application.OpenURL( "https://www.udemy.com/user/denis-84102" );
                }
                EditorGUILayout.Separator();
                {
                    EditorGUILayout.LabelField( "If you want to support me", EditorStyles.boldLabel );
                    EditorGUILayout.LabelField( "If you want to support me, please rate my packages, subscribe to my YouTube channel and like my videos." );
                }
            }
        }

    }
}
