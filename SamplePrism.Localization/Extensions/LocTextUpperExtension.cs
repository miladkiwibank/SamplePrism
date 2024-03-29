using System;
using System.Windows.Markup;
using SamplePrism.Localization.BaseExtensions;
using SamplePrism.Localization.Engine;

namespace SamplePrism.Localization.Extensions
{
    /// <summary>
    /// <c>BaseLocalizeExtension</c> for string objects.
    /// This strings will be converted to upper case.
    /// </summary>
    [MarkupExtensionReturnType(typeof(string))]
    public class LocTextUpperExtension : LocTextExtension
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocTextUpperExtension"/> class.
        /// </summary>
        public LocTextUpperExtension() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocTextUpperExtension"/> class.
        /// </summary>
        /// <param name="key">The resource identifier.</param>
        public LocTextUpperExtension(string key) : base(key) { }

        /// <summary>
        /// Provides the Value for the first Binding as <see cref="System.String"/>
        /// </summary>
        /// <param name="serviceProvider">
        /// The <see cref="System.Windows.Markup.IProvideValueTarget"/> provided from the <see cref="MarkupExtension"/>
        /// </param>
        /// <returns>The founded item from the .resx directory or null if not founded</returns>
        /// <exception cref="System.InvalidOperationException">
        /// thrown if <paramref name="serviceProvider"/> is not type of <see cref="System.Windows.Markup.IProvideValueTarget"/>
        /// </exception>
        /// <exception cref="System.NotSupportedException">
        /// thrown if the founded object is not type of <see cref="System.String"/>
        /// </exception>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            object obj = base.ProvideValue(serviceProvider);

            if (obj == null)
            {
                return null;
            }

            if (this.IsTypeOf(obj.GetType(), typeof(BaseLocalizeExtension<>)))
            {
                return obj;
            }

            if (obj.GetType().Equals(typeof(string)))
            {
                // dont call GetLocalizedText at this point, otherwise you will get prefix and suffix twice appended
                return obj;
            }

            throw new NotSupportedException(
                string.Format(
                    "ResourceKey '{0}' returns '{1}' which is not type of System.String",
                    this.Key,
                    obj.GetType().FullName));
        }

        /// <summary>
        /// This method formats the localized text.
        /// If the passed target text is null, string.empty will be returned.
        /// </summary>
        /// <param name="target">The text to format.</param>
        /// <returns>
        /// Returns the formated text or string.empty, if the target text was null.
        /// </returns>
        protected override string FormatText(string target)
        {
            return target == null ? string.Empty : target.ToUpper(this.GetForcedCultureOrDefault()).Replace("?", "SS");
        }

        /// <summary>
        /// see <c>BaseLocalizeExtension</c>
        /// </summary>
        protected override void HandleNewValue()
        {
            object obj = LocalizeDictionary.Instance.GetLocalizedObject<object>(this.Assembly, this.Dict, this.Key, this.GetForcedCultureOrDefault());
            this.SetNewValue(this.FormatOutput(obj));
        }
    }
}