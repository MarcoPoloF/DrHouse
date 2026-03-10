using Xamarin.Forms.Internals;

namespace DocNoc.Xam.ViewModels.Acceso
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class LoginViewModel : DocNocViewModel
    {
        #region Fields

        private string email;

        private string emailPlaceholder;

        private bool isInvalidEmail;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the email ID from user in the login page.
        /// </summary>
        public string Email
        {
            get
            {
                return this.email;
            }

            set
            {
                if (this.email == value)
                {
                    return;
                }

                SetProperty(ref email, value);
            }
        }

        /// <summary>
        /// Gets or sets the Password field placeholder.
        /// </summary>
        public string EmailPlaceholder
        {
            get { return this.emailPlaceholder; }
            set { SetProperty(ref emailPlaceholder, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the entered email is valid or invalid.
        /// </summary>
        public bool IsInvalidEmail
        {
            get
            {
                return this.isInvalidEmail;
            }

            set
            {
                if (this.isInvalidEmail == value)
                {
                    return;
                }

                SetProperty(ref isInvalidEmail, value);
            }
        }

        #endregion
    }
}
