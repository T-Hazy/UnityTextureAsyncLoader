using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace MFramework.AsyncTextureLoader
{
    public static partial class TextureAsyncLoader
    {
        /// <summary>
        /// 异步加载Texture2D
        /// </summary>
        private static async Task<Texture2D> CreateTextureAsync(string texturePath) {
            var textureData = await File.ReadAllBytesAsync(texturePath);
            var texture = await AsyncImageLoader.CreateFromImageAsync(textureData);
            return MakeTextureReadable(texture);
        }

        /// <summary>
        /// 异步加载Texture2D并创建TextureInfo对象
        /// </summary>
        private static async Task<TextureInfo>
            CreateTextureInfoAsync(string texturePath, TextureQuality textureQuality) {
            var textureData = await File.ReadAllBytesAsync(texturePath);
            var texture = await AsyncImageLoader.CreateFromImageAsync(textureData);
            return new TextureInfo(texturePath, MakeTextureReadable(texture), textureData, textureQuality);
        }

        //TODO 待封装相同的同步方法
        private static Texture2D CreateTexture(string texturePath) {
            var textureData = File.ReadAllBytes(texturePath);
            var texture = AsyncImageLoader.CreateFromImage(textureData);
            return MakeTextureReadable(texture);
        }

        //TODO 待封装相同的同步方法
        private static TextureInfo CreateTextureInfo(string texturePath, TextureQuality textureQuality) {
            var textureData = File.ReadAllBytes(texturePath);
            var texture = AsyncImageLoader.CreateFromImage(textureData);
            return new TextureInfo(texturePath, MakeTextureReadable(texture), textureData, textureQuality);
        }

        /// <summary>
        /// 使纹理可读
        /// </summary>
        /// <param name="texture"></param>
        /// <returns></returns>
        private static Texture2D MakeTextureReadable(Texture2D texture) {
            // Check if the texture is already readable
            if (!texture.isReadable) {
                // Create a temporary RenderTexture to read the texture data into
                RenderTexture renderTexture =
                    RenderTexture.GetTemporary(texture.width, texture.height, 0, RenderTextureFormat.Default);
                Graphics.Blit(texture, renderTexture);

                // Create a new Texture2D and read the data from the temporary RenderTexture
                Texture2D newTexture = new Texture2D(texture.width, texture.height);
                RenderTexture.active = renderTexture;
                newTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
                newTexture.Apply();
                // Clean up the temporary resources
                RenderTexture.active = null;
                RenderTexture.ReleaseTemporary(renderTexture);
                // Assign the new readable texture back to the original texture variable
                return newTexture;
            }

            return texture;
        }
    }
}