using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MFramework.AsyncTextureLoader
{
    public class TextureAsynchronousLoaderExample : MonoBehaviour
    {
        public TextureQuality Quality;
        public string TexturePath;
        public List<string> TexturePaths;
        public RawImage Image;
        public Button LoadButton;
        public Text text;

        private void Start() {
            LoadButton.onClick.AddListener(async () =>
            {
                var textureInfo = await TextureAsyncLoader.LoadTextureInfoAsync(TexturePath);
                //使用TextureInfo
                Debug.Log($"Texture路径：{textureInfo.TexturePath}");
                Debug.Log($"Texture所在目录：{textureInfo.TextureDirectory}");
                Debug.Log($"Texture加载成功：{textureInfo.Success}");
                Debug.Log($"Texture扩展名：{textureInfo.TextureExtension}");
                Debug.Log($"Texture名称：{textureInfo.TextureName}");
                Debug.Log($"Texture是否为空：{textureInfo.TextureIsEmpty}");
                Debug.Log($"Texture尺寸：{textureInfo.TextureWidth}：{textureInfo.TextureHeight}");
                Debug.Log($"Texture质量：{textureInfo.TextureQuality}");
                Debug.Log($"TextureData是否为空：{textureInfo.TextureDataIsEmpty}");
                //使用Texture
                Image.texture = textureInfo.Texture;
                Image.rectTransform.sizeDelta = new Vector2(textureInfo.TextureWidth, textureInfo.TextureHeight);
                //使用TextureData
                textureInfo.TextureData.CopyTo(new byte[textureInfo.TextureData.Length], 0);
                //使用FileInfo
                FileInfo info = textureInfo.Info;
                //设置纹理质量
                textureInfo.SetTextureQuality(TextureQuality.HighQuality);
                textureInfo.TextureQuality = TextureQuality.Default;
            });

            // 使用按钮订阅Texture2D异步加载事件
            // LoadButton.onClick.AddListener(async () =>
            // {
            //     var texture = await TextureAsyncLoader.LoadTextureAsync(TexturePath);
            //     //使用Texture2D
            //     Image.texture = texture;
            //     Image.rectTransform.sizeDelta = new Vector2(texture.width, texture.height);
            // });
            //
            // //使用按钮订阅List<Texture2D>异步加载事件
            // LoadButton.onClick.AddListener(async () =>
            // {
            //     List<Texture2D> texture2Ds = await TextureAsyncLoader.LoadTextureAsyncBatch(TexturePaths);
            //     foreach (var texture2D in texture2Ds) {
            //         //使用Texture2D
            //         Image.texture = texture2D;
            //     }
            // });
            // //使用按钮订阅异步加载Texture2D协程
            // LoadButton.onClick.AddListener(() =>
            // {
            //     StartCoroutine(TextureAsyncLoader.LoadTextureAsyncCoroutine(TexturePath, texture2D =>
            //     {
            //         //使用Texture2D
            //         Image.texture = texture2D;
            //     }));
            // });
            // //使用按钮订阅批量异步加载Texture2D协程：对于每次加载的Texture2D执行一次回调
            // LoadButton.onClick.AddListener(() =>
            // {
            //     StartCoroutine(TextureAsyncLoader.LoadTextureAsyncBatchCoroutine(TexturePaths, OnLoadTextureEnd));
            // });
            //
            // //使用按钮订阅批量异步加载Texture2D协程：在所有Texture2D加载完成后执行一次回调
            // LoadButton.onClick.AddListener(() =>
            // {
            //     StartCoroutine(TextureAsyncLoader.LoadTextureAsyncBatchCoroutine(TexturePaths, OnLoadTexturesEnd));
            // });
        }

        private void OnLoadTextureEnd(Texture2D texture) {
            //使用Texture2D
            Image.texture = texture;
        }

        private void OnLoadTexturesEnd(List<Texture2D> textureList) {
            //使用textureList
            foreach (var texture2D in textureList) {
                //Texture2D
            }
        }

        private float time = 0;

        private void Update() {
            time += Time.deltaTime;
            text.text = time.ToString();
        }
    }
}