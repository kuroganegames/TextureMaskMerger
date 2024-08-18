using UnityEngine;
using UnityEditor;
using System.IO;
using System.Threading.Tasks;

public class TextureMaskMerger : EditorWindow
{
    private Texture2D baseTexture;
    private Texture2D maskTexture;
    private string outputPath;
    private bool isProcessing = false;
    private ResolutionOption resolutionOption = ResolutionOption.Base;

    private enum ResolutionOption
    {
        Base,
        Mask
    }

    private static readonly Vector2 MinWindowSize = new Vector2(300, 250);
    private static readonly Vector2 MaxWindowSize = new Vector2(500, 350);

    [MenuItem("Window/Kurogane/Texture Mask Merger")]
    public static void ShowWindow()
    {
        TextureMaskMerger window = GetWindow<TextureMaskMerger>("Texture Combiner");
        window.minSize = MinWindowSize;
        window.maxSize = MaxWindowSize;
    }

    private void OnEnable()
    {
        minSize = MinWindowSize;
        maxSize = MaxWindowSize;
    }

    private void OnGUI()
    {
        GUILayout.Label("Texture Combiner", EditorStyles.boldLabel);

        baseTexture = (Texture2D)EditorGUILayout.ObjectField("Base Texture", baseTexture, typeof(Texture2D), false);
        maskTexture = (Texture2D)EditorGUILayout.ObjectField("Mask Texture", maskTexture, typeof(Texture2D), false);

        resolutionOption = (ResolutionOption)EditorGUILayout.EnumPopup("Resolution Option", resolutionOption);

        EditorGUI.BeginDisabledGroup(isProcessing);
        if (GUILayout.Button("Combine Textures"))
        {
            if (baseTexture != null && maskTexture != null)
            {
                CombineTexturesAsync();
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "Please assign both base and mask textures.", "OK");
            }
        }
        EditorGUI.EndDisabledGroup();

        if (isProcessing)
        {
            EditorGUILayout.HelpBox("Processing... Please wait.", MessageType.Info);
        }
    }

    private async void CombineTexturesAsync()
    {
        isProcessing = true;
        Repaint();

        PrepareTextures();

        Color[] baseColors = GetTexturePixels(baseTexture);
        Color[] maskColors = GetTexturePixels(maskTexture);

        int width, height;
        if (resolutionOption == ResolutionOption.Base)
        {
            width = baseTexture.width;
            height = baseTexture.height;
            maskColors = ResizeTextureColors(maskColors, maskTexture.width, maskTexture.height, width, height);
        }
        else
        {
            width = maskTexture.width;
            height = maskTexture.height;
            baseColors = ResizeTextureColors(baseColors, baseTexture.width, baseTexture.height, width, height);
        }

        Color[] combinedColors = await Task.Run(() => CombineTexturesTask(baseColors, maskColors, width, height));

        SaveCombinedTexture(combinedColors, width, height);

        isProcessing = false;
        Repaint();
    }

    private Color[] ResizeTextureColors(Color[] colors, int srcWidth, int srcHeight, int dstWidth, int dstHeight)
    {
        Texture2D tempTex = new Texture2D(srcWidth, srcHeight);
        tempTex.SetPixels(colors);
        tempTex.Apply();

        RenderTexture rt = RenderTexture.GetTemporary(dstWidth, dstHeight, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
        Graphics.Blit(tempTex, rt);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = rt;
        Texture2D resizedTex = new Texture2D(dstWidth, dstHeight);
        resizedTex.ReadPixels(new Rect(0, 0, dstWidth, dstHeight), 0, 0);
        resizedTex.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(rt);

        return resizedTex.GetPixels();
    }

    // PrepareTextures, GetTexturePixels, CombineTexturesTask, SaveCombinedTexture メソッドは前回と同じ

    private void PrepareTextures()
    {
        PrepareTexture(baseTexture);
        PrepareTexture(maskTexture);
    }

    private void PrepareTexture(Texture2D texture)
    {
        string path = AssetDatabase.GetAssetPath(texture);
        TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
        
        if (importer != null)
        {
            bool needsReimport = false;

            if (!importer.isReadable)
            {
                importer.isReadable = true;
                needsReimport = true;
            }

            if (importer.textureCompression != TextureImporterCompression.Uncompressed)
            {
                importer.textureCompression = TextureImporterCompression.Uncompressed;
                needsReimport = true;
            }

            if (needsReimport)
            {
                importer.SaveAndReimport();
            }
        }
    }

    private Color[] GetTexturePixels(Texture2D texture)
    {
        if (!texture.isReadable)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
        }
        return texture.GetPixels();
    }

    private Color[] CombineTexturesTask(Color[] baseColors, Color[] maskColors, int width, int height)
    {
        Color[] combinedColors = new Color[baseColors.Length];

        for (int i = 0; i < baseColors.Length; i++)
        {
            Color baseColor = baseColors[i];
            Color maskColor = maskColors[i];
            float alpha = maskColor.grayscale;
            combinedColors[i] = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
        }

        return combinedColors;
    }

    private void SaveCombinedTexture(Color[] combinedColors, int width, int height)
    {
        Texture2D combinedTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        combinedTexture.SetPixels(combinedColors);
        combinedTexture.Apply();

        byte[] pngData = combinedTexture.EncodeToPNG();
        string basePath = AssetDatabase.GetAssetPath(baseTexture);
        string directoryPath = Path.GetDirectoryName(basePath);
        string fileName = Path.GetFileNameWithoutExtension(basePath) + "_Combined.png";
        outputPath = Path.Combine(directoryPath, fileName);
        File.WriteAllBytes(outputPath, pngData);
        AssetDatabase.Refresh();

        TextureImporter newImporter = AssetImporter.GetAtPath(outputPath) as TextureImporter;
        if (newImporter != null)
        {
            newImporter.textureType = TextureImporterType.Default;
            newImporter.alphaIsTransparency = true;
            newImporter.SaveAndReimport();
        }

        EditorUtility.DisplayDialog("Success", "Combined texture saved at: " + outputPath, "OK");
    }
}