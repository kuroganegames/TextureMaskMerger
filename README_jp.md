# TextureMaskMerger for Unity

TextureMaskMergerは、ベーステクスチャとマスクテクスチャを簡単に結合して透明度を持つ新しいテクスチャを作成できるUnityエディタ拡張機能です。このツールは、組み込みのアルファチャンネルを持つ単一のテクスチャを必要とするシェーダーを使用する際に特に便利です。

TextureMaskMerger is a Unity Editor extension that allows you to easily combine a base texture with a mask texture, creating a new texture with transparency. This tool is particularly useful when working with shaders that require a single texture with built-in alpha channel.

[English README is here](README.md)

## 機能

- ベーステクスチャとマスクテクスチャの結合
- 出力解像度の選択オプション（ベーステクスチャまたはマスクテクスチャのいずれかに基づく）
- シンプルで直感的なUnityエディタインターフェース
- 異なるテクスチャ解像度のサポート
- 処理のためのテクスチャ圧縮設定の自動処理

## インストール方法

1. 最新のリリースから `TextureMaskMerger.unitypackage` ファイルをダウンロードします。
2. Unityプロジェクトを開きます。
3. ダウンロードした `.unitypackage` ファイルをダブルクリックするか、Unityプロジェクトウィンドウにドラッグ＆ドロップします。
4. Unity Package Importウィンドウで、すべての項目が選択されていることを確認し、'Import'をクリックします。

## 使用方法

1. Unityで、トップメニューの `Window > Kurogane > Texture Mask Merger` を選択します。
2. Texture Mask Mergerウィンドウで：
   - ベーステクスチャ（使用したいメインテクスチャ）を割り当てます。
   - マスクテクスチャ（白い領域が不透明で黒い領域が透明になるグレースケール画像）を割り当てます。
   - 解像度オプションを選択します：
     - Base: 出力はベーステクスチャの解像度に合わせます。
     - Mask: 出力はマスクテクスチャの解像度に合わせます。
3. "Merge Textures"をクリックします。
4. 結合されたテクスチャは、ベーステクスチャと同じフォルダに保存され、ファイル名の末尾に"_Merged"が追加されます。

## 動作要件

- Unity 2019.4以降（それ以前のバージョンでも動作する可能性がありますが、テストされていません）

## ライセンス

このプロジェクトはMITライセンスの下で公開されています - 詳細は[LICENSE](LICENSE)ファイルをご覧ください。

## コントリビューション

コントリビューションを歓迎します！プルリクエストを気軽に送ってください。

## 謝辞

- このツールのすべての貢献者とユーザーに感謝します。
- Unityプロジェクトにおける効率的なテクスチャとマスクの結合の必要性に触発されて作成されました。

