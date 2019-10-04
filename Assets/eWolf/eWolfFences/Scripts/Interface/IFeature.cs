namespace eWolfFences.Interfaces
{
    public interface IFeature
    {
        /// <summary>
        /// Gets the position of the gate as a percentage from the start to the end
        /// </summary>
        float Percentage
        {
            get; set;
        }

        /// <summary>
        /// Draw the UI in the editor
        /// </summary>
        void DrawUI();

        /// <summary>
        /// Get the width of the feature
        /// </summary>
        float FeatureWidth
        {
            get;
        }

        /// <summary>
        /// Get the current style type
        /// </summary>
        string StyleType
        {
            get;
        }
    }
}
