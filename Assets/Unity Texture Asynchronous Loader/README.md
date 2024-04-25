# Unity Texture Asynchronous Loader

Unity中，在运行时加载大图像（大于2K）时，[`ImageConversion.LoadImage`](https://docs.unity3d.com/ScriptReference/ImageConversion.LoadImage.html)
和`Texture2D.LoadImage` 速度很慢。 它们在加载图像时会阻塞 Unity 主线程，持续时间为一百毫秒甚至几秒。
如果是批量进行加载，那将是噩梦般的体验。
对于那些想要在运行时以编程方式加载这些图像的游戏的应用程序来说，这是一个大问题。

该包旨在将图像加载、图像解码和 mipmap 生成卸载到其他线程。 它可以创建更流畅的游戏体验，并减少加载大图像时 Unity 主线程上的延迟峰值。

该包是在开源项目 [Unity Asynchronous Image Loader](https://github.com/Looooong/UnityAsyncImageLoader)
的基础上进行封装的，将常用的功能集成到一个静态脚本中。同时封装了一个拥有和加载图片相关的常用字段的`TextureInfo`
类，以便在图片加载完成后获取相关信息。

## 前置依赖(Dependencies)

+ Unity Burst (Unity内置包)
+ Unity Mathematics (Unity内置包) —— Unity Burst 的前置包，安装Unity Burst时会自动导入。
+ [UnityAsyncImageLoader-0.1.2](https://github.com/Looooong/UnityAsyncImageLoader) (已经包含在项目中)

## 核心脚本文件介绍

| 脚本文件                              | 功能描述                                                                                                                                                                             |
|-----------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| TextureAsyncLoader.Basics.cs      | **纹理异步加载器(基础)：** 纹理异步加载器的基础功能部分，只有`异步加载TextureInfo`和使加载后的`纹理可读`两个方法。                                                                                                             |
| TextureAsyncLoader.Texture2D.cs   | **纹理异步加载器(Texture2D)：** 纹理异步加载器的`Texture2D`部分，提供`Texture2D`类型相关的异步加载、异步回调、批量加载、批量回调、批量逐一回调、协程、批量协程等方法的各种重载。                                                                      |    
| TextureAsyncLoader.TextureInfo.cs | **纹理异步加载器(TextureInfo)：** 纹理异步加载器的`TextureInfo`部分，提供`TextureInfo`类相关的异步加载、异步回调、批量加载、批量回调、批量逐一回调、协程、批量协程等方法的各种重载。                                                                 |    
| TextureInfo.cs       | **纹理信息(TextureInfo)：** 普通类，其中包含了纹理加载后的许多额外信息。例如纹理本身、FileInfo、路径、名称、尺寸、所在目录、扩展名、是否加载成功、二进制字节数据、纹理质量等。                                                                             |
| TextureQuality.cs       | **纹理质量(Quality)：** 枚举类，用于描述纹理的加载质量，包括`Default`、`Low`、`Medium`、`High`、`Ultra`。可使用`TextureInfo`类的`SetTextureQuality()`方法或者`TextureQuality`字段来设置纹理质量。在加载时可以在参数中指定纹理质量，默认为`Default`。 |

## 核心代码(Core Code)

```csharp
    /// <summary>
    /// 异步加载Texture2D
    /// </summary>
    private static async Task<Texture2D> LoadTexture(string texturePath) {
        var textureData = await File.ReadAllBytesAsync(texturePath);
        var texture = await AsyncImageLoader.CreateFromImageAsync(textureData);
        MakeTextureReadable(texture);
        return texture;
    }
    
    /// <summary>
    /// 异步加载Texture2D并创建TextureInfo对象
    /// </summary>
    private static async Task<TextureInfo> LoadTexture(string texturePath, TextureQuality textureQuality) {
        var textureData = await File.ReadAllBytesAsync(texturePath);
        var texture = await AsyncImageLoader.CreateFromImageAsync(textureData);
        MakeTextureReadable(texture);
        return new TextureInfo(texturePath, texture, textureData, textureQuality);
    }
```

## 使用方法(Usage)

使用异步加载`Texture2D`和异步加载`TextureInfo`的所有方法、回调、重载均相同，区别是`TextureInfo`对象可以提供更多额外的信息。

**所以建议是直接使用异步加载`TextureInfo`**， 如果您介意每次都要创建一个`TextureInfo`
对象，担心可能的性能影响，可以仅使用异步加载`Texture2D`。

### 示例字段

```csharp
        public string TexturePath;
        public List<string> TexturePaths;
        public RawImage Image;
        public Button LoadButton;
```

### 1. 异步加载Texture2D

**1.1：** 使用按钮订阅`Texture2D`异步加载事件

```csharp
 LoadButton.onClick.AddListener(async () =>
 {
    var texture = await TextureAsyncLoader.LoadTextureAsync(TexturePath);
    //使用Texture2D
    Image.texture = texture;
    Image.rectTransform.sizeDelta = new Vector2(texture.width, texture.height);
  });
```

**1.2：** 使用按钮订阅`List<Texture2D>`异步加载事件

```csharp
 LoadButton.onClick.AddListener(async () =>
 {
    List<Texture2D> texture2Ds = await TextureAsyncLoader.LoadTextureAsyncBatch(TexturePaths);
    //使用List<Texture2D>
    foreach (var texture2D in texture2Ds) {
        //使用Texture2D
        Image.texture = texture2D;
    }
 });
```

**1.3：** 使用按钮订阅异步加载`Texture2D`协程

```cs
  LoadButton.onClick.AddListener(() =>
  {
     StartCoroutine(TextureAsyncLoader.LoadTextureAsyncCoroutine(TexturePath, texture2D =>
     {
        //使用Texture2D
        Image.texture = texture2D;
     }));
  });
```

**1.4：** 使用按钮订阅批量异步加载`Texture2D`协程：对于每次加载的`Texture2D`执行一次回调

```csharp
    LoadButton.onClick.AddListener(() =>
    {
        StartCoroutine(TextureAsyncLoader.LoadTextureAsyncBatchCoroutine(TexturePaths, OnLoadTextureEnd));
    });
    
    private void OnLoadTextureEnd(Texture2D texture) {
        //使用Texture2D
        Image.texture = texture;
    }
```

**1.5：** 使用按钮订阅批量异步加载`Texture2D`协程：在所有`Texture2D`加载完成后执行一次回调

```csharp
    LoadButton.onClick.AddListener(() =>
    {
        StartCoroutine(TextureAsyncLoader.LoadTextureAsyncBatchCoroutine(TexturePaths, OnLoadTexturesEnd));
    });
    
    private void OnLoadTexturesEnd(List<Texture2D> textureList) {
        //使用textureList
        foreach (var texture2D in textureList) {
            //Texture2D
        }
    }
```

### 2. 异步加载TextureInfo

异步加载`TextureInfo`的所有方式以及对应方式的重载方法与异步加载`Texture2D`的方式相同，区别是`TextureInfo`类中包含了许多额外的信息。

**所以建议是直接使用异步加载`TextureInfo`。**

```csharp
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
```

## 注意事项

+ 所有方法均没有错误处理机制，请确保在使用异步加载之前检查目标路径是否正确。
+ 异步加载的`Texture2D`
  对象的纹理像素数会保持与原始文件分辨率相同，这意味着长宽不会像手动导入Unity编辑器哪样保持宽高为4的n次方幂个像素数，这将使得诸如`Texture2D.Compress(bool)`
  等对原始纹理数据做操作的编程API可能会出错。如果想避免这种情况，可能需要手动对原始数据做二次处理。
+ 使用`TextureInfo`的`SetTextureQuality()`方法和`TextureQuality`
  字段设置纹理质量时，具体的质量等级详细设置并没有科学或者严谨的配置标准，如需修改不同的配置详情，请根据实际情况自行在`TextureInfo`
  类中更改设置方法，或者使用`Texture2D`类来自行详细设置每一项。 **现有的配置如下：**

```csharp
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
            if (TextureWidth % 4 != 0 || TextureHeight % 4 != 0) {
                Texture.Compress(false);
            }
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
            if (TextureWidth % 4 != 0 || TextureHeight % 4 != 0) {
                Texture.Compress(false);
            }
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
```

## 加载后

### 纹理格式

如果图像有alpha通道，格式将是`RGB32`，否则，它将是`RGB24`。

### 产生的数据

mipmap是使用带有2x2内核的框过滤生成的。当在编辑器中使用纹理导入时，最终结果将与Unity的对应结果不同。

## 故障排除

### 在加载超大图片时仍然会有延迟

在`TextureAsyncLoader`方法完成执行后，图像数据仍在传输到GPU。因此,任何对象如材质或UI，想要使用纹理之后将不得不等待纹理完成
上传，从而阻塞Unity主线程。

没有简单的方法来检测纹理是否完成了数据上传。变通方法有:

+ 等待一秒钟或更长时间再使用纹理。
+ **(未经过测试)** 使用[' AsyncGPUReadback '](https://docs.unity3d.com/ScriptReference/Rendering.AsyncGPUReadback.html)
  请求纹理中的单个像素。它将等待纹理在下载之前完成上传
  单独的像素。然后请求回调可以用来通知Unity主线程关于纹理上传
  完成。
