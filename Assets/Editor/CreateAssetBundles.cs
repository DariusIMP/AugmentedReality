using UnityEditor;

public class CreateAssetBundles
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {        
        BuildPipeline.BuildAssetBundles("Assets/StreamingAssets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.Android);
    }
}