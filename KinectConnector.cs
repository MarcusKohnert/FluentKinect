namespace FluentKinect
{
	using System;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;
	using Microsoft.Kinect;

	public sealed class KinectConnector
	{
		private static KinectSensor kinectSensor;
		private static CoordinateMapper coordinateMapper;

		/// <summary>
		/// Starts a new Task and listens to KinectSensors StatusChanged event.
		/// </summary>
		/// <returns>Eventually returns a kinect sensor when one is connected.</returns>
		public static Task<KinectSensor> GetKinect()
		{
			return Task.Factory.StartNew<KinectSensor>(() =>
			{
				if (kinectSensor != null) return kinectSensor;

				var kinect = KinectSensor.KinectSensors.FirstOrDefault(_ => _.Status == KinectStatus.Connected);
				if (kinect != null)
				{
					kinectSensor = kinect;
					return kinectSensor;
				}

				using (var signal = new ManualResetEventSlim())
				{
					KinectSensor.KinectSensors.StatusChanged += (s, e) =>
					{
						if (e.Status == KinectStatus.Connected)
						{
							kinectSensor = e.Sensor;
							coordinateMapper = new CoordinateMapper(kinectSensor);
							signal.Set();
						}
					};

					signal.Wait();
				}

				return kinectSensor;
			});
		}

		public static CoordinateMapper GetCoordinateMapper()
		{
			return coordinateMapper;
		}
	}
}