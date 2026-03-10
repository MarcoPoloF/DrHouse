using System.Runtime.Serialization;

namespace DocNoc.Xam.Models
{
    /// <summary>
    /// Model for SocialProfile
    /// </summary>
    public class Profile
    {
        #region Fields

        private string imagePath;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the image path.
        /// </summary>
        public string ImagePath
        {
            get { return App.BaseImageUrl + this.imagePath; }
            set { this.imagePath = value; }
        }

        #endregion
    }
}