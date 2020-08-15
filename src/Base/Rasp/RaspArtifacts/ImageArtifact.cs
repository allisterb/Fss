using System;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Fss.Rasp
{
    public class ImageArtifact : Artifact
    {
        #region Constructors
        public ImageArtifact(Bitmap image)
        {
            Image = image;
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            BitmapData bmpdata =
                image.LockBits(rect, ImageLockMode.ReadWrite,
                image.PixelFormat);
            var length = bmpdata.Stride * bmpdata.Height;
            var buffer = new byte[length];
            Marshal.Copy(bmpdata.Scan0, buffer, 0, length);
            image.UnlockBits(bmpdata);
            Data = buffer;
        }

        public ImageArtifact(Bitmap image, FileArtifact artifact) : this(image)
        {
            FileArtifact = artifact;
            Name = FileArtifact.Name;
            Source = artifact;
            CurrentProcess = artifact.CurrentProcess;
            CurrentWindowTitle = artifact.CurrentWindowTitle;

        }
        #endregion

        #region Properties
        public byte[] Data { get; }

        public Bitmap Image { get; }

        public FileArtifact FileArtifact { get; }

        public TextArtifact TextArtifact { get; set; }

        public bool HasData => Data != null && Data.Length != 0;

        public bool HasBitmap => Image != null;

        public bool HasFile => FileArtifact != null;

        public Dictionary<ImageObjectKinds, List<Rectangle>> DetectedObjects { get; }
            = new Dictionary<ImageObjectKinds, List<Rectangle>>();

        public List<ArtifactCategory> Categories { get; } = new List<ArtifactCategory>();

        public bool IsAdultContent { get; set; }

        public double AdultContentScore { get; set; }

        public bool IsRacy { get; set; }

        public double RacyContentScore { get; set; }

        public List<string> OCRText { get; set; }

        public bool HasOCRText => OCRText != null;

        public List<string> Captions { get; set; }

        public List<string> Tags {get; set; }
        
        #endregion

        #region Methods
        public bool HasDetectedObjects(ImageObjectKinds kind) => this.DetectedObjects.ContainsKey(kind)
            && this.DetectedObjects[kind].Count > 0;

        public void AddDetectedObject(ImageObjectKinds kind, Rectangle r)
        {
            if(!DetectedObjects.ContainsKey(kind))
                    {
                DetectedObjects.Add(ImageObjectKinds.FaceCandidate, new List<Rectangle>() { r });
            }
            else
            {
                DetectedObjects[kind].Add(r);
            }
        }
        #endregion
    }
}
