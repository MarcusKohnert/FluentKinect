namespace FluentKinect
{
	using System;
	using System.Linq;
	using Microsoft.Kinect;

	public sealed class KinectConnector
	{
		private static KinectSensor kinectSensor;
		private static CoordinateMapper coordinateMapper;

		static KinectConnector()
		{
			var kinect = KinectSensor.KinectSensors.FirstOrDefault(s => s.Status == KinectStatus.Connected);
			if (kinect == null) throw new InvalidOperationException("No Kinect connected..."); // TODO: Hook to StatusChanged event
			
			kinectSensor = kinect;
			coordinateMapper = new CoordinateMapper(kinectSensor);
		}

		public static KinectSensor GetKinect()
		{
			return kinectSensor;
		}

		public static CoordinateMapper GetCoordinateMapper()
		{
			return coordinateMapper;
		}
	}
}