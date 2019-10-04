using eWolfFences.Interfaces;

namespace eWolfFences
{
    public class PannelFeature : FeatureBase, IFeature
    {
        /// <summary>
        /// The feature type options for this feature
        /// </summary>
        public enum PannelFeatureStyles
        {
            Gap,
            Gate,
        }

        /// <summary>
        /// The feature style type
        /// </summary>
        public PannelFeatureStyles Style = PannelFeatureStyles.Gate;
        
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
