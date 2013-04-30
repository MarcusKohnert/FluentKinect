namespace FluentKinect
{
	using System;
	using System.Linq;
	using Microsoft.Kinect;

	public sealed class KinectConnector
	{
		private IDisposable subscription;
		private static KinectSensor kinectSensor;

		static KinectConnector()
		{
			var kinect = KinectSensor.KinectSensors.FirstOrDefault(s => s.Status == KinectStatus.Connected);
			if (kinect == null) throw new InvalidOperationException("No Kinect connected..."); // TODO: Hook to StatusChanged event
			
			kinectSensor = kinect;
		}

		protected KinectConnector()
		{
 
		}

		public static KinectSensor GetKinect()
		{
			return kinectSensor;
		}
	}
}