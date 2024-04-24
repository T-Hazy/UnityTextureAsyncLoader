using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Burst;
using UnityEngine;

namespace MFramework.AsyncTextureLoader
{
    public class TextureInfo
    {
        /// <summary>
        /// 根据纹理路径创建的FileInfo
        /// </summary>
        public FileInfo Info { get; }

        /// <summary>
        /// 纹理路径
        /// </summary>
        public string TexturePath => Info.FullName;

        /// <summary>
        /// 纹理名称
        /// </summary>
        public string TextureName => Info.Name;

        /// <summary>
        /// 纹理扩展名
        /// </summary>
        public string TextureExtension => Info.Extension;

        /// <summary>
        /// 纹理所在目录
        /// </summary>
        public string TextureDirectory => Path.GetDirectoryName(Info.FullName);

        /// <summary>
        /// 纹理加载是否成功
        /// </summary>
        public bool Success { get; private set; } = false;

        /// <summary>
        /// 纹理是否为空
        /// </summary>
        public bool TextureIsEmpty => Texture.width <= 1 || Texture.height <= 1 || Texture == null;

        /// <summary>
        /// 纹理宽度
        /// </summary>
        public int TextureWidth => Texture == null ? 0 : Texture.width;

        /// <summary>
        /// 纹理高度
        /// </summary>
        public int TextureHeight => Texture == null ? 0 : Texture.height;

        /// <summary>
        /// 纹理对象
        /// </summary>
        public Texture2D Texture { get; private set; }

        private TextureQuality textureQuality = TextureQuality.Default;

        /// <summary>
        /// 纹理质量
        /// </summary>
        public TextureQuality TextureQuality
        {
            get => textureQuality;
            set => SetTextureQuality(value);
        }

        /// <summary>
        /// 纹理数据是否为空
        /// </summary>
        public bool TextureDataIsEmpty => TextureData.Length == 0 || TextureData == null;

        /// <summary>
        /// 纹理数据
        /// </summary>
        public byte[] TextureData { get; private set; }

        /// <summary>
        /// 私有化无参构造函数，防止外部创建TextureInfo对象
        /// </summary>
        private TextureInfo() {
        }

        public TextureInfo(string texturePath, Texture2D texture, byte[] textureData, TextureQuality quality) {
            Texture = texture;
            defaultFilterMode = texture.filterMode;
            defaultanisoLevel = texture.anisoLevel;
            defaultminimumMipmapLevel = texture.minimumMipmapLevel;
            defaultTextureWrapMode = texture.wrapMode;
            if (quality != TextureQuality.Default) SetTextureQuality(quality);
            TextureData = textureData;
            Info = new FileInfo(texturePath);
            Success = !TextureIsEmpty && !TextureDataIsEmpty;
        }

        /// <summary>
        /// 设置纹理质量
        /// </summary>
        public void SetTextureQuality(TextureQuality quality) {
            switch (quality) {
                case TextureQuality.Default:
                    SetDefaultTextureQuality();
                    break;
                case TextureQuality.LowQuality:
                    SetLowTextureQuality();
                    break;
                case TextureQuality.MediumQuality:
                    SetMediumTextureQuality();
                    break;
                case TextureQuality.HighQuality:
                    SetHighTextureQuality();
                    break;
                case TextureQuality.UltraQuality:
                    SetUltraTextureQuality();
                    break;
                default:
                    Debug.LogWarning("Unsupported texture quality: " + quality);
                    break;
            }
        }

        //在首次创建TextureInfo时会在构造函数中缓存默认纹理质量设置
        private FilterMode defaultFilterMode;
        private int defaultanisoLevel;
        private int defaultminimumMipmapLevel;
        private TextureWrapMode defaultTextureWrapMode;

        /// <summary>
        /// 设置为默认纹理质量
        /// </summary>
        private void SetDefaultTextureQuality() {
            textureQuality = TextureQuality.Default;
            Texture.filterMode = defaultFilterMode;
            Texture.anisoLevel = defaultanisoLevel;
            Texture.minimumMipmapLevel = defaultminimumMipmapLevel;
            Texture.wrapMode = defaultTextureWrapMode;
        }

        /// <summary>
        /// 设置为Low纹理质量
        /// </summary>
        private void SetLowTextureQuality() {
            textureQuality = TextureQuality.LowQuality;
            Texture.filterMode = FilterMode.Point;
            Texture.anisoLevel = 1;
            Texture.Compress(false);
            Texture.minimumMipmapLevel = 0;
            Texture.wrapMode = TextureWrapMode.Repeat;
        }

        /// <summary>
        /// 设置为Medium纹理质量
        /// </summary>
        private void SetMediumTextureQuality() {
            textureQuality = TextureQuality.MediumQuality;
            Texture.filterMode = FilterMode.Bilinear;
            Texture.anisoLevel = 6;
            Texture.Compress(true);
            Texture.minimumMipmapLevel = (int)(Texture.mipmapCount * 0.5f);
            Texture.wrapMode = TextureWrapMode.Clamp;
        }

        /// <summary>
        /// 设置为High纹理质量
        /// </summary>
        private void SetHighTextureQuality() {
            textureQuality = TextureQuality.HighQuality;
            Texture.filterMode = FilterMode.Trilinear;
            Texture.anisoLevel = 12;
            Texture.minimumMipmapLevel = (int)(Texture.mipmapCount * 0.8f);
            Texture.wrapMode = TextureWrapMode.MirrorOnce;
        }

        /// <summary>
        /// 设置为Ultra纹理质量
        /// </summary>
        private void SetUltraTextureQuality() {
            textureQuality = TextureQuality.UltraQuality;
            Texture.filterMode = FilterMode.Trilinear;
            Texture.anisoLevel = 16;
            Texture.minimumMipmapLevel = Texture.mipmapCount;
            Texture.wrapMode = TextureWrapMode.Mirror;
        }
    }
}