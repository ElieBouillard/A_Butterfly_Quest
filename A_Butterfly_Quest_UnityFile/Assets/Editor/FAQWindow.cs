using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FAQWindow : EditorWindow
{
    private static Texture2D bg_tex;
    int tipIndex;

    [MenuItem("FAQ/Common Questions")]
    public static void ShowWindow()
    {

        // Create a new window of this type or focus an existing one
        var window = GetWindow<FAQWindow>("FAQ", true);
        // Setup and show window
        window.minSize = new Vector2(500, 500);
        window.maxSize = new Vector2(500, 500);
        window.ShowUtility();
        //window.Show();

        bg_tex = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        bg_tex.SetPixel(0, 0, new Color(0.10f, 0.12f, 0.15f));
        bg_tex.Apply();

    }

    private void OnGUI()
    {
        //BG texture
        //GUI.DrawTexture(new Rect(0, 0, maxSize.x, maxSize.y), bg_tex, ScaleMode.StretchToFill);
        //scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(250), GUILayout.Height(250));
        GUILayout.Space(10);

        
        if(GUILayout.Button("Workflow Sourcetree"))
        {
            tipIndex = 1;
        }
        if(tipIndex == 1)
        {
            GUILayout.Label("Explication des termes :", EditorStyles.boldLabel);
            GUILayout.Label("Commit = Faire une copie locale des fichiers modifiés\n" +
                "Pull = Récupérer la version la plus récente du projet\n" +
                "Push = Envoyer en ligne les modifications effectuées");
            GUILayout.Label("Envoyer ses fichiers :", EditorStyles.boldLabel);
            GUILayout.Label("Commit > Pull > Push");
            GUILayout.Label("Annuler une ou plusieurs modifications :", EditorStyles.boldLabel);
            GUILayout.Label("Dans la fenêtre pour commit, clic droit sur un élément >  Discard");
        }

        if (GUILayout.Button("Modifier le feeling du player"))
        {
            tipIndex = 2;
        }
        if (tipIndex == 2)
        {
            GUILayout.Label("Cliquer sur le GameObject \"Player\" dans la hierarchie :\n" +
                "Dans l'inspecteur, Chercher le script Character2D\n" +
                "Modifier les courbes pour faire varier le saut et les déplacements\n" +
                "(Pour créer un point sur la courbe, il faut double-cliquer dessus)\n" +
                "(Pour raccourcir la durée du saut, faire Ctrl + A et rétrecir l'ensemble de la courbe)");
        }

        if (GUILayout.Button("Organisation des scènes"))
        {
            tipIndex = 3;
        }
        if (tipIndex == 3)
        {
            GUILayout.Label("-L'ensemble des scènes sont dans le dossier Scenes\n" +
                "-UNE SEULE personne à la fois peut travailler sur une scène précise sans quoi vous\naurez des problèmes pour mettre en commun vos modifications sur la même scène\n" +
                "-Si vous avez un doute, dupliquez la scène en question, et travailler sur un duplicata.\nIl faudra ensuite mettre en commun les modifications de la scène originale \net du duplicata");
        }

        if (GUILayout.Button("Setup de test du LD"))
        {
            tipIndex = 4;
        }
        if (tipIndex == 4)
        {
            GUILayout.Label("-Les murs grimpables : Layer = Wall / Tag = Wall \n" +
                "-Le sol : Layer = Ground / Tag = Ground\n" +
                "-L'acide : Layer = Ground / Tag = Acide \n"+
                "-Les cubes de télékinésie : Layer = Ground / Tag = Flair\n" +
                "-La souris : Layer = Player / Tag = Player : "
                );
        }

        //GUILayout.EndScrollView();

    }
}

