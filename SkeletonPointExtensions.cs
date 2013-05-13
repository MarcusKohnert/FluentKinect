namespace FluentKinect
{
	using Microsoft.Kinect;

	public static class SkeletonPointExtensions
	{
		public static ColorImagePoint MapToColor(this SkeletonPoint skeletonPoint, CoordinateMapper mapper, ColorImageFormat format = ColorImageFormat.RgbResolution640x480Fps30)
		{
			return mapper.MapSkeletonPointToColorPoint(skeletonPoint, format);
		}
	}
}