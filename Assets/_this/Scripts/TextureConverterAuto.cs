using System;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class TextureConverterAuto
{
    private static string folderPath = "";
    private static HashSet<string> processedTextures = new HashSet<string>();

    static TextureConverterAuto()
    {
        // Load the previously saved folder path
        folderPath = EditorPrefs.GetString("TextureConverterFolderPath", "");
        
        // Only start monitoring if the folder path is set
        if (!string.IsNullOrEmpty(folderPath))
        {
            EditorApplication.update += OnEditorUpdate;
        }
    }

    // Menu item to set the sprite folder
    [MenuItem("Tools/Select Sprite Folder")]
    public static void SetSpriteFolder()
    {
        string selectedPath = EditorUtility.OpenFolderPanel("Select Folder with Textures", "Assets", "");

        // Check if a folder was selected
        if (!string.IsNullOrEmpty(selectedPath))
        {
            // Convert absolute path to relative path
            if (selectedPath.StartsWith(Application.dataPath))
            {
                folderPath = "Assets" + selectedPath.Substring(Application.dataPath.Length);
                EditorPrefs.SetString("TextureConverterFolderPath", folderPath); // Save the path
                Debug.Log("Sprite folder set to: " + folderPath);
            }
            else
            {
                Debug.LogWarning("Selected folder is not within the Assets directory.");
            }
        }
    }

    // Update method to continuously check for new textures
    static void OnEditorUpdate()
    {
        if (!string.IsNullOrEmpty(folderPath))
        {
            ConvertTexturesInFolder(folderPath);
        }
    }

    // Method to convert textures in the specified folder
    static void ConvertTexturesInFolder(string folderPath)
    {
        string[] textureFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);

        foreach (string texturePath in textureFiles)
        {
            if (IsValidTexture(texturePath) && !processedTextures.Contains(texturePath))
            {
                string assetPath = texturePath.Replace(Application.dataPath, "Assets");
                TextureImporter textureImporter = (TextureImporter)AssetImporter.GetAtPath(assetPath);

                if (textureImporter != null && NeedsConversion(textureImporter))
                {
                    textureImporter.textureType = TextureImporterType.Sprite;
                    textureImporter.filterMode = FilterMode.Point;
                    textureImporter.maxTextureSize = 1024;
                    textureImporter.textureCompression = TextureImporterCompression.Uncompressed;

                    textureImporter.SaveAndReimport();
                    Debug.Log("Converted: " + assetPath);

                    // Mark the texture as processed
                    processedTextures.Add(texturePath);
                    AssetDatabase.Refresh();
                }
            }
        }
    }

    // Helper method to validate if the file is a texture
    static bool IsValidTexture(string path)
    {
        return path.EndsWith(".png") || path.EndsWith(".jpg") || path.EndsWith(".tga");
    }

    // Helper method to determine if the texture needs conversion
    static bool NeedsConversion(TextureImporter textureImporter)
    {
        return textureImporter.textureType != TextureImporterType.Sprite ||
               textureImporter.filterMode != FilterMode.Point ||
               textureImporter.maxTextureSize != 1024 ||
               textureImporter.textureCompression != TextureImporterCompression.Uncompressed;
    }
}
