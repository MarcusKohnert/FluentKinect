namespace FluentKinect
{
	using System;
	using Microsoft.Kinect;
	using Microsoft.Kinect.Toolkit.Interaction;

	public static class KinectSensorExtensions
	{
		/// <summary>
		/// Enables the Color Image Stream for this Kinect Sensor, with the specified format.
		/// </summary>
		/// <param name="kinect">The Kinect Sensor</param>
		/// <param name="format">The ColorImageFormat, defaults to 640x480x30</param>
		/// <returns>The Kinect Sensor</returns>
		public static KinectSensor EnableColorStream(this KinectSensor kinect, ColorImageFormat format = ColorImageFormat.RgbResolution640x480Fps30)
		{
			return CheckAndReturn(kinect, _ => _.ColorStream.Enable(format));
		}

		/// <summary>
		/// Enables the Depth Image Stream for this Kinect Sensor, with the specified format.
		/// </summary>
		/// <param name="kinect">The Kinect Sensor</param>
		/// <param name="format">The DepthImageFormat, defaults to 640x480x30</param>
		/// <returns>The Kinect Sensor</returns>
		public static KinectSensor EnableDepthStream(this KinectSensor kinect, DepthImageFormat format = DepthImageFormat.Resolution640x480Fps30)
		{
			return CheckAndReturn(kinect, _ => _.DepthStream.Enable(format));
		}

		/// <summary>
		/// Enables the Skeleton Stream for this Kinect Sensor, with the specified TransformSmoothParameters.
		/// </summary>
		/// <param name="kinect">The Kinect Sensor</param>
		/// <param name="parameters">The TransformSmoothParamerts, optional parameter</param>
		/// <returns>The Kinect Sensor</returns>
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

		/// <summary>
		/// Creates a new instance of InteractionStream.
		/// </summary>
		/// <param name="kinect">The kinect sensor.</param>
		/// <param name="interactionClient">The interaction client instance</param>
		/// <returns>A new instance of InteractionStream.</returns>
		public static InteractionStream EnableInteractionStream(this KinectSensor kinect, IInteractionClient interactionClient)
		{
			if(kinect == null) throw new ArgumentNullException("kinect");
			if(interactionClient == null) throw new ArgumentNullException("interactionClient");

			if (!kinect.DepthStream.IsEnabled) throw new InvalidOperationException("The depth stream is not enabled, but mandatory.");
			if (!kinect.SkeletonStream.IsEnabled) throw new InvalidOperationException("The skeleton stream is not enabled, but mandatory.");

			return new InteractionStream(kinect, interactionClient);
		}

		/// <summary>
		/// Sets the skeletonstream's tracking mode to Seated.
		/// </summary>
		/// <param name="kinect">The kinect sensor.</param>
		/// <returns>The kinect sensor with TrackingMode equals seated.</returns>
		public static KinectSensor Seated(this KinectSensor kinect)
		{
			return CheckAndReturn(kinect, _ => _.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated);
		}

		/// <summary>
		/// Calls Start on the kinect sensor and returns the sensor.
		/// </summary>
		/// <param name="kinect">The kinect sensor.</param>
		/// <returns>The started kinect sensor.</returns>
		public static KinectSensor Start_(this KinectSensor kinect)
		{
			return CheckAndReturn(kinect, _ => _.Start());
		}

		/// <summary>
		/// Sets the kinect sensors DepthStream Range property to Near.
		/// </summary>
		/// <param name="kinect">The kinect sensor.</param>
		/// <returns>The kinect sensor with Range equals Near.</returns>
		public static KinectSensor NearMode(this KinectSensor kinect)
		{
			return CheckAndReturn(kinect, _ => _.DepthStream.Range = DepthRange.Near);
		}

		/// <summary>
		/// Enables the color stream, depth stream and skeleton stream of the kinect.
		/// If seated is true the SkeletonStream's TrackingMode is set to Seated.
		/// Starts the streaming from the sensor.
		/// </summary>
		/// <param name="kinect">The kinect sensor.</param>
		/// <param name="seated">Specifies whether the TrackingMode shall be seated.</param>
		/// <returns>The configured kinect sensor.</returns>
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

				if (seated) _.Seated();

				_.Start();
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