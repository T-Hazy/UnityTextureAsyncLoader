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
        /// 异步加载纹理
        /// </summary>
        public static async Task<Texture2D> LoadTextureAsync(string texturePath) {
            var result = await CreateTextureAsync(texturePath);
            return result;
        }

        /// <summary>
        /// 异步加载纹理
        /// </summary>
        public static async Task<Texture2D> LoadTextureAsync(FileInfo fileInfo) {
            return await LoadTextureAsync(fileInfo.FullName);
        }

        /// <summary>
        /// 异步加载纹理
        /// </summary>
        public static async void LoadTextureAsync(string texturePath, Action<Texture2D> onLoaded) {
            var result = await CreateTextureAsync(texturePath);
            onLoaded?.Invoke(result);
        }

        /// <summary>
        /// 异步加载纹理
        /// </summary>
        public static async void LoadTextureAsync(FileInfo fileInfo, Action<Texture2D> onLoaded) {
            var result = await CreateTextureAsync(fileInfo.FullName);
            onLoaded?.Invoke(result);
        }

        /// <summary>
        /// 批量异步加载纹理
        /// </summary>
        public static async Task<List<Texture2D>> LoadTextureAsyncBatch(List<string> texturePaths) {
            List<Texture2D> textures = new List<Texture2D>();
            foreach (var texturePath in texturePaths) {
                var result = await CreateTextureAsync(texturePath);
                textures.Add(result);
            }

            return textures;
        }

        /// <summary>
        /// 批量异步加载纹理
        /// </summary>
        public static async Task<List<Texture2D>> LoadTextureAsyncBatch(List<FileInfo> texturePaths) {
            List<Texture2D> textures = new List<Texture2D>();
            foreach (var texturePath in texturePaths) {
                var result = await CreateTextureAsync(texturePath.FullName);
                textures.Add(result);
            }

            return textures;
        }

        /// <summary>
        /// 批量异步加载纹理
        /// </summary>
        public static async Task<List<Texture2D>> LoadTextureAsyncBatch(string[] texturePaths) {
            List<Texture2D> textures = new List<Texture2D>();
            foreach (var texturePath in texturePaths) {
                var result = await CreateTextureAsync(texturePath);
                textures.Add(result);
            }

            return textures;
        }

        /// <summary>
        /// 批量异步加载纹理
        /// </summary>
        public static async Task<List<Texture2D>> LoadTextureAsyncBatch(FileInfo[] texturePaths) {
            List<Texture2D> textures = new List<Texture2D>();
            foreach (var texturePath in texturePaths) {
                var result = await CreateTextureAsync(texturePath.FullName);
                textures.Add(result);
            }

            return textures;
        }

        // use call-back
        /// <summary>
        /// 批量异步加载纹理(加载完成后执行回调)
        /// </summary>
        public static async void LoadTextureAsyncBatch(List<string> texturePaths, Action<List<Texture2D>> onLoaded) {
            List<Texture2D> textures = new List<Texture2D>();
            foreach (var texturePath in texturePaths) {
                var result = await CreateTextureAsync(texturePath);
                textures.Add(result);
            }

            onLoaded?.Invoke(textures);
        }

        /// <summary>
        /// 批量异步加载纹理(逐一执行回调)
        /// </summary>
        public static async void LoadTextureAsyncBatch(List<string> texturePaths, Action<Texture2D> onSingleLoaded) {
            foreach (var texturePath in texturePaths) {
                var result = await CreateTextureAsync(texturePath);
                onSingleLoaded?.Invoke(result);
            }
        }

        /// <summary>
        /// 批量异步加载纹理(加载完成后执行回调)
        /// </summary>
        public static async void LoadTextureAsyncBatch(List<FileInfo> texturePaths, Action<List<Texture2D>> onLoaded) {
            List<Texture2D> textures = new List<Texture2D>();
            foreach (var texturePath in texturePaths) {
                var result = await CreateTextureAsync(texturePath.FullName);
                textures.Add(result);
            }

            onLoaded?.Invoke(textures);
        }

        /// <summary>
        /// 批量异步加载纹理(逐一执行回调)
        /// </summary>
        public static async void LoadTextureAsyncBatch(List<FileInfo> texturePaths, Action<Texture2D> onSingleLoaded) {
            foreach (var texturePath in texturePaths) {
                var result = await CreateTextureAsync(texturePath.FullName);
                if (result) onSingleLoaded?.Invoke(result);
            }
        }

        /// <summary>
        /// 批量异步加载纹理(加载完成后执行回调)
        /// </summary>
        public static async void LoadTextureAsyncBatch(string[] texturePaths, Action<List<Texture2D>> onLoaded) {
            List<Texture2D> textures = new List<Texture2D>();
            foreach (var texturePath in texturePaths) {
                var result = await CreateTextureAsync(texturePath);
                textures.Add(result);
            }

            onLoaded?.Invoke(textures);
        }

        /// <summary>
        /// 批量异步加载纹理(逐一执行回调)
        /// </summary>
        public static async void LoadTextureAsyncBatch(string[] texturePaths, Action<Texture2D> onSingleLoaded) {
            foreach (var texturePath in texturePaths) {
                var result = await CreateTextureAsync(texturePath);
                onSingleLoaded?.Invoke(result);
            }
        }

        /// <summary>
        /// 批量异步加载纹理(加载完成后执行回调)
        /// </summary>
        public static async void LoadTextureAsyncBatch(FileInfo[] texturePaths, Action<List<Texture2D>> onLoaded) {
            List<Texture2D> textures = new List<Texture2D>();
            foreach (var texturePath in texturePaths) {
                var result = await CreateTextureAsync(texturePath.FullName);
                textures.Add(result);
            }

            onLoaded?.Invoke(textures);
        }

        /// <summary>
        /// 批量异步加载纹理(逐一执行回调)
        /// </summary>
        public static async void LoadTextureAsyncBatch(FileInfo[] texturePaths, Action<Texture2D> onSingleLoaded) {
            foreach (var texturePath in texturePaths) {
                var result = await CreateTextureAsync(texturePath.FullName);
                onSingleLoaded?.Invoke(result);
            }
        }

        // use coroutine
        /// <summary>
        /// 批量异步加载纹理
        /// </summary>
        public static IEnumerator LoadTextureAsyncCoroutine(string texturePath, Action<Texture2D> onLoaded) {
            var task = CreateTextureAsync(texturePath);
            yield return new WaitUntil(() => task.IsCompleted);
            if (task.IsCompletedSuccessfully) onLoaded?.Invoke(task.Result);
        }

        /// <summary>
        /// 批量异步加载纹理
        /// </summary>
        public static IEnumerator LoadTextureAsyncCoroutine(FileInfo fileInfo, Action<Texture2D> onLoaded) {
            yield return LoadTextureAsyncCoroutine(fileInfo.FullName, onLoaded);
        }

        /// <summary>
        /// 批量异步加载纹理(加载完成后执行回调)
        /// </summary>
        public static IEnumerator LoadTextureAsyncBatchCoroutine(List<string> texturePaths,
            Action<List<Texture2D>> onLoaded) {
            var task = LoadTextureAsyncBatch(texturePaths);
            yield return new WaitUntil(() => task.IsCompleted);
            if (task.IsCompletedSuccessfully) onLoaded?.Invoke(task.Result);
        }

        /// <summary>
        /// 批量异步加载纹理(逐一执行回调)
        /// </summary>
        public static IEnumerator LoadTextureAsyncBatchCoroutine(List<string> texturePaths,
            Action<Texture2D> onSingleLoaded) {
            foreach (var texturePath in texturePaths) {
                yield return LoadTextureAsyncCoroutine(texturePath, onSingleLoaded);
            }
        }

        /// <summary>
        /// 批量异步加载纹理(加载完成后执行回调)
        /// </summary>
        public static IEnumerator LoadTextureAsyncBatchCoroutine(List<FileInfo> texturePaths,
            Action<List<Texture2D>> onLoaded) {
            var task = LoadTextureAsyncBatch(texturePaths);
            yield return new WaitUntil(() => task.IsCompleted);
            if (task.IsCompletedSuccessfully) onLoaded?.Invoke(task.Result);
        }

        /// <summary>
        /// 批量异步加载纹理(逐一执行回调)
        /// </summary>
        public static IEnumerator LoadTextureAsyncBatchCoroutine(List<FileInfo> texturePaths,
            Action<Texture2D> onSingleLoaded) {
            foreach (var texturePath in texturePaths) {
                yield return LoadTextureAsyncCoroutine(texturePath, onSingleLoaded);
            }
        }

        /// <summary>
        /// 批量异步加载纹理(加载完成后执行回调)
        /// </summary>
        public static IEnumerator LoadTextureAsyncBatchCoroutine(string[] texturePaths,
            Action<List<Texture2D>> onLoaded) {
            var task = LoadTextureAsyncBatch(texturePaths);
            yield return new WaitUntil(() => task.IsCompleted);
            if (task.IsCompletedSuccessfully) onLoaded?.Invoke(task.Result);
        }

        /// <summary>
        /// 批量异步加载纹理(逐一执行回调)
        /// </summary>
        public static IEnumerator LoadTextureAsyncBatchCoroutine(FileInfo[] texturePaths,
            Action<Texture2D> onSingleLoaded) {
            foreach (var texturePath in texturePaths) {
                yield return LoadTextureAsyncCoroutine(texturePath, onSingleLoaded);
            }
        }
    }
}