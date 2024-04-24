using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace MFramework.AsyncTextureLoader
{
    public static partial class TextureAsyncLoader
    {
        /// <summary>
        /// 异步加载TextureInfo
        /// </summary>
        public static async Task<TextureInfo> LoadTextureInfoAsync(string texturePath,
            TextureQuality textureQuality = TextureQuality.Default) {
            return await CreateTextureInfoAsync(texturePath, textureQuality);
        }

        /// <summary>
        /// 异步加载TextureInfo
        /// </summary>
        public static async Task<TextureInfo> LoadTextureInfoAsync(FileInfo fileInfo,
            TextureQuality textureQuality = TextureQuality.Default) {
            return await CreateTextureInfoAsync(fileInfo.FullName, textureQuality);
        }

        /// <summary>
        /// 异步加载TextureInfo
        /// </summary>
        public static async void LoadTextureInfoAsync(string texturePath, Action<TextureInfo> onLoaded,
            TextureQuality textureQuality = TextureQuality.Default) {
            var result = await CreateTextureInfoAsync(texturePath, textureQuality);
            onLoaded?.Invoke(result);
        }

        /// <summary>
        /// 异步加载TextureInfo
        /// </summary>
        public static async void LoadTextureInfoAsync(FileInfo fileInfo, Action<TextureInfo> onLoaded,
            TextureQuality textureQuality = TextureQuality.Default) {
            var result = await CreateTextureInfoAsync(fileInfo.FullName, textureQuality);
            onLoaded?.Invoke(result);
        }

        /// <summary>
        /// 批量异步加载TextureInfo
        /// </summary>
        public static async Task<List<TextureInfo>> LoadTextureInfoAsyncBatch(List<string> texturePaths,
            TextureQuality textureQuality = TextureQuality.Default) {
            List<TextureInfo> textureInfos = new List<TextureInfo>();
            foreach (var texturePath in texturePaths) {
                var result = await CreateTextureInfoAsync(texturePath, textureQuality);
                textureInfos.Add(result);
            }

            return textureInfos;
        }

        /// <summary>
        /// 批量异步加载TextureInfo
        /// </summary>
        public static async Task<List<TextureInfo>> LoadTextureInfoAsyncBatch(List<FileInfo> texturePaths,
            TextureQuality textureQuality = TextureQuality.Default) {
            List<TextureInfo> textureInfos = new List<TextureInfo>();
            foreach (var texturePath in texturePaths) {
                var result = await CreateTextureInfoAsync(texturePath.FullName, textureQuality);
                textureInfos.Add(result);
            }

            return textureInfos;
        }

        /// <summary>
        /// 批量异步加载TextureInfo
        /// </summary>
        public static async Task<List<TextureInfo>> LoadTextureInfoAsyncBatch(string[] texturePaths,
            TextureQuality textureQuality = TextureQuality.Default) {
            List<TextureInfo> textureInfos = new List<TextureInfo>();
            foreach (var texturePath in texturePaths) {
                var result = await CreateTextureInfoAsync(texturePath, textureQuality);
                textureInfos.Add(result);
            }

            return textureInfos;
        }

        /// <summary>
        /// 批量异步加载TextureInfo
        /// </summary>
        public static async Task<List<TextureInfo>> LoadTextureInfoAsyncBatch(FileInfo[] texturePaths,
            TextureQuality textureQuality = TextureQuality.Default) {
            List<TextureInfo> textureInfos = new List<TextureInfo>();
            foreach (var texturePath in texturePaths) {
                var result = await CreateTextureInfoAsync(texturePath.FullName, textureQuality);
                textureInfos.Add(result);
            }

            return textureInfos;
        }

        /************************* use call-back****************************/


        /// <summary>
        /// 异步加载TextureInfo(加载完成后执行回调)
        /// </summary>
        public static async void LoadTextureInfoAsyncBatch(List<string> texturePaths,
            Action<List<TextureInfo>> onLoaded,
            TextureQuality textureQuality = TextureQuality.Default) {
            List<TextureInfo> textures = new List<TextureInfo>();
            foreach (var texturePath in texturePaths) {
                var result = await CreateTextureInfoAsync(texturePath, textureQuality);
                textures.Add(result);
            }

            onLoaded?.Invoke(textures);
        }

        /// <summary>
        /// 批量异步加载纹理(逐一执行回调)
        /// </summary>
        public static async void LoadTextureInfoAsyncBatch(List<string> texturePaths,
            Action<TextureInfo> onSingleLoaded,
            TextureQuality textureQuality = TextureQuality.Default) {
            foreach (var texturePath in texturePaths) {
                var result = await CreateTextureInfoAsync(texturePath, textureQuality);
                onSingleLoaded?.Invoke(result);
            }
        }

        /// <summary>
        /// 异步加载TextureInfo(加载完成后执行回调)
        /// </summary>
        public static async void LoadTextureInfoAsyncBatch(List<FileInfo> texturePaths,
            Action<List<TextureInfo>> onLoaded,
            TextureQuality textureQuality = TextureQuality.Default) {
            List<TextureInfo> textures = new List<TextureInfo>();
            foreach (var texturePath in texturePaths) {
                var result = await CreateTextureInfoAsync(texturePath.FullName, textureQuality);
                textures.Add(result);
            }

            onLoaded?.Invoke(textures);
        }

        /// <summary>
        /// 批量异步加载TextureInfo(逐一执行回调)
        /// </summary>
        public static async void LoadTextureInfoAsyncBatch(List<FileInfo> texturePaths,
            Action<TextureInfo> onSingleLoaded,
            TextureQuality textureQuality = TextureQuality.Default) {
            foreach (var texturePath in texturePaths) {
                var result = await CreateTextureInfoAsync(texturePath.FullName, textureQuality);
                if (result.Success) onSingleLoaded?.Invoke(result);
            }
        }

        /// <summary>
        /// 异步加载TextureInfo(加载完成后执行回调)
        /// </summary>
        public static async void LoadTextureInfoAsyncBatch(string[] texturePaths, Action<List<TextureInfo>> onLoaded,
            TextureQuality textureQuality = TextureQuality.Default) {
            List<TextureInfo> textures = new List<TextureInfo>();
            foreach (var texturePath in texturePaths) {
                var result = await CreateTextureInfoAsync(texturePath, textureQuality);
                textures.Add(result);
            }

            onLoaded?.Invoke(textures);
        }

        /// <summary>
        /// 批量异步加载TextureInfo(逐一执行回调)
        /// </summary>
        public static async void LoadTextureInfoAsyncBatch(string[] texturePaths, Action<TextureInfo> onSingleLoaded,
            TextureQuality textureQuality = TextureQuality.Default) {
            foreach (var texturePath in texturePaths) {
                var result = await CreateTextureInfoAsync(texturePath, textureQuality);
                if (result.Success) onSingleLoaded?.Invoke(result);
            }
        }

        /// <summary>
        /// 异步加载TextureInfo(加载完成后执行回调)
        /// </summary>
        public static async void
            LoadTextureInfoAsyncBatch(FileInfo[] texturePaths, Action<List<TextureInfo>> onLoaded,
                TextureQuality textureQuality = TextureQuality.Default) {
            List<TextureInfo> textures = new List<TextureInfo>();
            foreach (var texturePath in texturePaths) {
                var result = await CreateTextureInfoAsync(texturePath.FullName, textureQuality);
                textures.Add(result);
            }

            onLoaded?.Invoke(textures);
        }

        /// <summary>
        /// 批量异步加载TextureInfo(逐一执行回调)
        /// </summary>
        public static async void
            LoadTextureInfoAsyncBatch(FileInfo[] texturePaths, Action<TextureInfo> onSingleLoaded,
                TextureQuality textureQuality = TextureQuality.Default) {
            foreach (var texturePath in texturePaths) {
                var result = await CreateTextureInfoAsync(texturePath.FullName, textureQuality);
                if (result.Success) onSingleLoaded?.Invoke(result);
            }
        }

        // use coroutine
        /// <summary>
        /// 异步加载TextureInfo
        /// </summary>
        public static IEnumerator LoadTextureInfoAsyncCoroutine(string texturePath, Action<TextureInfo> onLoaded,
            TextureQuality textureQuality = TextureQuality.Default) {
            var task = CreateTextureInfoAsync(texturePath, textureQuality);
            yield return new WaitUntil(() => task.IsCompleted);
            if (task.IsCompletedSuccessfully) onLoaded?.Invoke(task.Result);
        }

        /// <summary>
        /// 异步加载TextureInfo
        /// </summary>
        public static IEnumerator LoadTextureInfoAsyncCoroutine(FileInfo fileInfo, Action<TextureInfo> onLoaded,
            TextureQuality textureQuality = TextureQuality.Default) {
            yield return LoadTextureInfoAsyncCoroutine(fileInfo.FullName, onLoaded, textureQuality);
        }

        /// <summary>
        /// 异步加载TextureInfo(加载完成后执行回调)
        /// </summary>
        public static IEnumerator LoadTextureAsyncBatchCoroutine(List<string> texturePaths,
            Action<List<TextureInfo>> onLoaded,
            TextureQuality textureQuality = TextureQuality.Default) {
            var task = LoadTextureInfoAsyncBatch(texturePaths, textureQuality);
            yield return new WaitUntil(() => task.IsCompleted);
            if (task.IsCompletedSuccessfully) onLoaded?.Invoke(task.Result);
        }

        /// <summary>
        /// 批量异步加载纹理(逐一执行回调)
        /// </summary>
        public static IEnumerator LoadTextureAsyncBatchCoroutine(List<string> texturePaths,
            Action<TextureInfo> onSingleLoaded,
            TextureQuality textureQuality = TextureQuality.Default) {
            foreach (var texturePath in texturePaths) {
                yield return LoadTextureInfoAsyncCoroutine(texturePath, onSingleLoaded, textureQuality);
            }
        }

        /// <summary>
        /// 异步加载TextureInfo(加载完成后执行回调)
        /// </summary>
        public static IEnumerator LoadTextureInfoAsyncBatchCoroutine(List<FileInfo> texturePaths,
            Action<List<TextureInfo>> onLoaded,
            TextureQuality textureQuality = TextureQuality.Default) {
            var task = LoadTextureInfoAsyncBatch(texturePaths, textureQuality);
            yield return new WaitUntil(() => task.IsCompleted);
            if (task.IsCompletedSuccessfully) onLoaded?.Invoke(task.Result);
        }

        /// <summary>
        /// 批量异步加载TextureInfo(逐一执行回调)
        /// </summary>
        public static IEnumerator LoadTextureInfoAsyncBatchCoroutine(List<FileInfo> texturePaths,
            Action<TextureInfo> onSingleLoaded,
            TextureQuality textureQuality = TextureQuality.Default) {
            foreach (var texturePath in texturePaths) {
                yield return LoadTextureInfoAsyncCoroutine(texturePath, onSingleLoaded, textureQuality);
            }
        }

        /// <summary>
        /// 异步加载TextureInfo(加载完成后执行回调)
        /// </summary>
        public static IEnumerator LoadTextureAsyncBatchCoroutine(string[] texturePaths,
            Action<List<TextureInfo>> onLoaded,
            TextureQuality textureQuality = TextureQuality.Default) {
            var task = LoadTextureInfoAsyncBatch(texturePaths, textureQuality);
            yield return new WaitUntil(() => task.IsCompleted);
            if (task.IsCompletedSuccessfully) onLoaded?.Invoke(task.Result);
        }

        /// <summary>
        /// 批量异步加载TextureInfo(逐一执行回调)
        /// </summary>
        public static IEnumerator LoadTextureAsyncBatchCoroutine(FileInfo[] texturePaths,
            Action<TextureInfo> onSingleLoaded,
            TextureQuality textureQuality = TextureQuality.Default) {
            foreach (var texturePath in texturePaths) {
                yield return LoadTextureInfoAsyncCoroutine(texturePath, onSingleLoaded, textureQuality);
            }
        }
    }
}