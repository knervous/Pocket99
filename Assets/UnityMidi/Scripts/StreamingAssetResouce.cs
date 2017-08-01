using UnityEngine;
using System.IO;
using AudioSynthesis;
using System.Collections;

namespace UnityMidi
{
    [System.Serializable]
    public class StreamingAssetResouce : IResource
    {
        [SerializeField] string streamingAssetPath;

        public bool ReadAllowed()
        {
            return true;
        }

        public bool WriteAllowed()
        {
            return false;
        }

        public bool DeleteAllowed()
        {
            return false;
        }

        public string GetName()
        {
            return Path.GetFileName(streamingAssetPath);
        }

        public Stream OpenResourceForRead()
        {
#if UNITY_EDITOR
            return File.OpenRead(Path.Combine(Application.streamingAssetsPath, streamingAssetPath));

#elif UNITY_ANDROID
            string filePath = "jar:file://" + Application.dataPath + "!/assets/" + streamingAssetPath;
            WWW data = new WWW(filePath);
            while (!data.isDone) { }
            Stream stream = new MemoryStream(data.bytes);
            return stream;
#else

#endif
        }

        public Stream OpenResourceForWrite()
        {
            throw new System.NotImplementedException();
        }

        public void DeleteResource()
        {
            throw new System.NotImplementedException();
        }
    }
}
