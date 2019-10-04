using eWolfFences.Interfaces;

namespace eWolfFences
{
    public class HedgeFeature : FeatureBase, IFeature
    {
        /// <summary>
        /// The feature type options for this feature
        /// </summary>
        public enum HedgeFeatureStyles
        {
            Gap,
            Arch
        }

        /// <summary>
        /// The feature style type
        /// </summary>
        public HedgeFeatureStyles Style = HedgeFeatureStyles.Gap;

        /// <summary>
        /// The width of the Featuer
        /// </summary>
        public float Width = 4;

        /// <summary>
        /// Get the width of the feature
        /// </summary>
        public float FeatureWidth
        {
            get
            {
                return Width;
            }
        }

        /// <summary>
        /// Get the current style type
        /// </summary>
        public string StyleType
        {
            get
            {
                return Style.ToString();
            }
        }
    }
}
