# TextureMaskMerger for Unity

TextureMaskMerger is a Unity Editor extension that allows you to easily combine a base texture with a mask texture, creating a new texture with transparency. This tool is particularly useful when working with shaders that require a single texture with built-in alpha channel.

TextureMaskMergerは、ベーステクスチャとマスクテクスチャを簡単に結合して透明度を持つ新しいテクスチャを作成できるUnityエディタ拡張機能です。このツールは、組み込みのアルファチャンネルを持つ単一のテクスチャを必要とするシェーダーを使用する際に特に便利です。

[日本語のREADMEはこちら](README_jp.md)

## Features

- Merge a base texture with a mask texture
- Option to choose the output resolution (based on either the base or mask texture)
- Simple and intuitive Unity Editor interface
- Supports different texture resolutions
- Automatically handles texture compression settings for processing

## Installation

1. Download the `TextureMaskMerger.unitypackage` file from the latest release.
2. Open your Unity project.
3. Double-click the downloaded `.unitypackage` file, or drag and drop it into your Unity project window.
4. In the Import Unity Package window, ensure all items are selected and click 'Import'.

## Usage

1. In Unity, go to `Window > Kurogane > Texture Mask Merger` in the top menu.
2. In the Texture Mask Merger window:
   - Assign your base texture (the main texture you want to use).
   - Assign your mask texture (grayscale image where white areas will be opaque and black areas transparent).
   - Choose the resolution option:
     - Base: Output will match the base texture's resolution.
     - Mask: Output will match the mask texture's resolution.
3. Click "Merge Textures".
4. The merged texture will be saved in the same folder as the base texture, with "_Merged" appended to the filename.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Acknowledgements

- Thanks to all contributors and users of this tool.
- Inspired by the need for efficient texture and mask merging in Unity projects.

