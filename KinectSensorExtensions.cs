namespace FluentKinect
{
	using System;
	using Microsoft.Kinect;

	public static class KinectSensorExtensions
	{
		public static KinectSensor EnableColorStream(this KinectSensor kinect, ColorImageFormat format = ColorImageFormat.RgbResolution640x480Fps30)
		{
			return CheckAndReturn(kinect, _ => _.ColorStream.Enable(format));
		}

		public static KinectSensor EnableDepthStream(this KinectSensor kinect, DepthImageFormat format = DepthImageFormat.Resolution640x480Fps30)
		{
			return CheckAndReturn(kinect, _ => _.DepthStream.Enable(format));
		}

		public static KinectSensor EnableSkeletonStream(this KinectSensor kinect, TransformSmoothParameters parameters = new TransformSmoothParameters())
		{
			return CheckAndReturn(kinect, _ =>
			{
				if (parameters != null)
					_.SkeletonStream.Enable(parameters);
				else
					_.SkeletonStream.Enable();
			});
		}

		public static KinectSensor Seated(this KinectSensor kinect)
		{
			return CheckAndReturn(kinect, _ => _.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated);
		}

		public static KinectSensor NearMode(this KinectSensor kinect)
		{
			return CheckAndReturn(kinect, _ => _.DepthStream.Range = DepthRange.Near);
		}

		public static KinectSensor KickStart(this KinectSensor kinect, Boolean seated = false)
		{
			return CheckAndReturn(kinect, _ =>
			{
				_ = _.EnableColorStream()
							   .EnableDepthStream()
							   .EnableSkeletonStream(new TransformSmoothParameters
							   {
								   Correction = 0.8f,
								   Smoothing = 0.8f
							   });

				if (seated) _.Seated().Start();
			});
		}

		private static KinectSensor CheckAndReturn(KinectSensor kinect, Action<KinectSensor> action)
		{
			if (kinect == null) throw new ArgumentNullException("kinect");

			action(kinect);

			return kinect;
		}
	}
}